using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace PetrpkuWeb.NovellDirectoryLdap
{
    public class LdapAuthenticationService : IAppAuthenticationService
    {
        private const string MemberOfAttribute = "memberOf";
        private const string DisplayNameAttribute = "displayName";
        private const string SAMAccountNameAttribute = "sAMAccountName";
        private const string MailAttribute = "mail";

        private readonly LdapConfig _config;
        private readonly LdapConnection _connection;

        public LdapAuthenticationService(IOptions<LdapConfig> configAccessor)
        {
            _config = configAccessor.Value;
            _connection = new LdapConnection();
        }

        public IAuthUser Login(string username, string password)
        {
            _connection.Connect(_config.Url, LdapConnection.DefaultPort);
            _connection.Bind(_config.Username, _config.Password);

            var searchFilter = String.Format(_config.SearchFilter, username);
            var result = _connection.Search(
                _config.SearchBase,
                LdapConnection.ScopeSub,
                searchFilter,
                new[] {
                    MemberOfAttribute,
                    DisplayNameAttribute,
                    SAMAccountNameAttribute,
                    MailAttribute
                },
                false
            );

            try
            {
                var user = result.Next();
                if (user != null)
                {                    
                    _connection.Bind(user.Dn, password);
                    if (_connection.Bound)
                    {
                        var accountNameAttr = user.GetAttribute(SAMAccountNameAttribute);
                        if (accountNameAttr == null)
                        {
                            throw new Exception("Your account is missing the account name.");
                        }

                        var displayNameAttr = user.GetAttribute(DisplayNameAttribute);
                        if (displayNameAttr == null)
                        {
                            throw new Exception("Your account is missing the display name.");
                        }

                        var emailAttr = user.GetAttribute(MailAttribute);
                        if (emailAttr == null)
                        {
                            throw new Exception("Your account is missing an email.");
                        }

                        var memberAttr = user.GetAttribute(MemberOfAttribute);
                        if (memberAttr == null)
                        {
                            throw new Exception("Your account is missing roles.");
                        }

                        return new LdapUser
                        {
                            DisplayName = displayNameAttr.StringValue,
                            Username = accountNameAttr.StringValue,
                            Email = emailAttr.StringValue,
                            Roles = memberAttr.StringValueArray
                                .Select(x => GetGroup(x))
                                .Where(x => x != null)
                                .Distinct()
                                .ToArray()
                        };
                    }
                }
            }
            finally
            {
                _connection.Disconnect();
            }

            return null;
        }

        public IAuthUser Search(string username)
        {
            _connection.Connect(_config.Url, LdapConnection.DefaultPort);
            _connection.Bind(_config.Username, _config.Password);

            var searchFilter = String.Format(_config.SearchFilter, username);
            var result = _connection.Search(
                _config.SearchBase,
                LdapConnection.ScopeSub,
                searchFilter,
                new[] {                    
                    DisplayNameAttribute,
                    SAMAccountNameAttribute,
                    MailAttribute
                },
                false
            );

            try
            {
                var user = result.Next();
                if (user != null)
                {    
                    if (_connection.Bound)
                    {
                        var accountNameAttr = user.GetAttribute(SAMAccountNameAttribute);
                        if (accountNameAttr == null)
                        {
                            throw new Exception("Your account is missing the account name.");
                        }

                        var displayNameAttr = user.GetAttribute(DisplayNameAttribute);
                        if (displayNameAttr == null)
                        {
                            throw new Exception("Your account is missing the display name.");
                        }

                        var emailAttr = user.GetAttribute(MailAttribute);
                        if (emailAttr == null)
                        {
                            throw new Exception("Your account is missing an email.");
                        }                        

                        return new LdapUser
                        {
                            DisplayName = displayNameAttr.StringValue,
                            Username = accountNameAttr.StringValue,
                            Email = emailAttr.StringValue                            
                        };
                    }
                }
            }
            finally
            {
                _connection.Disconnect();
            }

            return null;
        }

        private string GetGroup(string value)
        {
            Match match = Regex.Match(value, "^CN=([^,]*)");
            if (!match.Success)
            {
                return null;
            }

            return match.Groups[1].Value;
        }
    }

}
