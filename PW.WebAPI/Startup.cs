using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security.OAuth;
using PW.WebAPI.Infrastructure;

[assembly: OwinStartup(typeof(PW.WebAPI.Startup))]

namespace PW.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //use CORS for WebAPI and Token
            app.UseCors(CorsOptions.AllowAll);

            //!!! Authentication step should be prior to the configuration of SignalR
            //!!! otherwise NullReferenceException in Hub on Context.User.Identity.Name
            ConfigureAuth(app);
            
            //http://stackoverflow.com/questions/26657296/signalr-authentication-with-webapi-bearer-token
            app.Map("/signalr", map =>
            {
                //map.UseCors(CorsOptions.AllowAll);
                map.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
                {
                    Provider = new QueryStringOAuthBearerProvider()
                });
                var hubConfiguration = new HubConfiguration
                {
                    Resolver = GlobalHost.DependencyResolver
                };
                map.RunSignalR(hubConfiguration);
            });


        }
    }
}
