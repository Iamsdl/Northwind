﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NorthwindWeb.Startup))]
namespace NorthwindWeb
{
    /// <summary>
    /// Start the application.
    /// Some more comments here.
    /// New comment
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// generates application configuration
        /// </summary>
        /// <param name="app">application data</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
