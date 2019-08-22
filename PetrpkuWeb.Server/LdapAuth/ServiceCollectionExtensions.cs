﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.LdapAuth
{
    /// <summary>
    /// Provides extension methods for configuring the DI container
    /// </summary>`
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures <see cref="LdapAuthenticationOptions"/> for the given service collection.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="collection"></param>
        /// <param name="setupAction"></param>
        public static void AddLdapAuthentication<TUser>(this IServiceCollection collection, Action<LdapAuthenticationOptions> setupAction = null)
            where TUser : class
        {
            if (setupAction != null)
            {
                collection.Configure(setupAction);
            }
        }
    }
}
