using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
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

            var searchFilter = String.Format(_config.SearchOneFilter, username);
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
                if (user is { })
                {
                    _connection.Bind(user.Dn, password);
                    if (_connection.Bound)
                    {
                        var accountNameAttr = user.GetAttribute(SAMAccountNameAttribute);
                        if (accountNameAttr is null)
                        {
                            throw new Exception("Your account is missing the account name.");
                        }

                        var displayNameAttr = user.GetAttribute(DisplayNameAttribute);
                        if (displayNameAttr is null)
                        {
                            throw new Exception("Your account is missing the display name.");
                        }

                        var emailAttr = user.GetAttribute(MailAttribute);
                        if (emailAttr is null)
                        {
                            throw new Exception("Your account is missing an email.");
                        }

                        var memberAttr = user.GetAttribute(MemberOfAttribute);
                        if (memberAttr is null)
                        {
                            throw new Exception("Your account is missing roles.");
                        }

                        return new LdapUser
                        {
                            DisplayName = displayNameAttr.StringValue,
                            UserName = accountNameAttr.StringValue,
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

            var searchFilter = String.Format(_config.SearchOneFilter, username);
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
                if (user is { })
                {
                    if (_connection.Bound)
                    {
                        var accountNameAttr = user.GetAttribute(SAMAccountNameAttribute);
                        if (accountNameAttr is null)
                        {
                            throw new Exception("Your account is missing the account name.");
                        }

                        var displayNameAttr = user.GetAttribute(DisplayNameAttribute);
                        if (displayNameAttr is null)
                        {
                            throw new Exception("Your account is missing the display name.");
                        }

                        var emailAttr = user.GetAttribute(MailAttribute);
                        if (emailAttr is null)
                        {
                            throw new Exception("Your account is missing an email.");
                        }

                        return new LdapUser
                        {
                            DisplayName = displayNameAttr.StringValue,
                            UserName = accountNameAttr.StringValue,
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


        public List<IAuthUser> SearchAll()
        {
            _connection.Connect(_config.Url, LdapConnection.DefaultPort);
            _connection.Bind(_config.Username, _config.Password);

            var listOfLdapUsers = new List<IAuthUser>();

            LdapSearchQueue queue = _connection.Search(
                _config.SearchBase, 
                LdapConnection.ScopeSub,
                _config.SearchAllFilter, 
                null, 
                false, 
                (LdapSearchQueue) null, 
                (LdapSearchConstraints) null);

            LdapMessage message;
            while ((message = queue.GetResponse()) != null)
            {
                if (message is LdapSearchResult)
                {
                    LdapEntry entry = ((LdapSearchResult)message).Entry;
                    LdapAttributeSet attributeSet = entry.GetAttributeSet();

                    listOfLdapUsers.Add(
                        new LdapUser
                        {
                            DisplayName = attributeSet.GetAttribute(DisplayNameAttribute)?.StringValue,
                            UserName = attributeSet.GetAttribute(SAMAccountNameAttribute)?.StringValue,
                            Email = attributeSet.GetAttribute(MailAttribute)?.StringValue
                        });
                }
            }
            
            _connection.Disconnect();

            var sortedList = listOfLdapUsers.OrderBy(d => d.DisplayName).ToList();
            return sortedList;
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
