﻿using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.LdapAuth
{
    public class LdapAuthentication
    : IDisposable
    {
        private readonly LdapAuthenticationOptions _options;
        private readonly LdapConnection _connection;
        private bool _isDisposed = false;

        /// <summary>
        /// Initializes a new instance with the the given options.
        /// </summary>
        /// <param name="options"></param>
        public LdapAuthentication(LdapAuthenticationOptions options)
        {
            _options = options;
            _connection = new LdapConnection();
        }

        /// <summary>
        /// Cleans up any connections and other resources.
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _connection.Dispose();
            _isDisposed = true;
        }

        /// <summary>
        /// Gets a value that indicates if the password for the user identified by the given DN is valid.
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string distinguishedName, string password)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(LdapConnection));
            }

            if (string.IsNullOrEmpty(_options.Hostname))
            {
                throw new InvalidOperationException("The LDAP Hostname cannot be empty or null.");
            }

            _connection.Connect(_options.Hostname, _options.Port);

            try
            {
                var accountName = string.IsNullOrWhiteSpace(_options.Domain) ? distinguishedName : $"{_options.Domain}\\{distinguishedName}";
                _connection.Bind(accountName, password);
                return _connection.Bound;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _connection.Disconnect();
            }
        }
    }

}
