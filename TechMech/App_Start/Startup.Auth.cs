using System;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TechMech.Models;

namespace TechMech
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Third party login providers
            //http://www.oauthforaspnet.com/providers/microsoft/guides/aspnet-mvc5/
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "6af48c76-ab03-447c-9d7f-efa316b80e5c",
            //    clientSecret: "P5PuFQ9FrVGwiWtkrBcF5yW");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(
               appId: ConfigurationManager.AppSettings["FacebookAppId"],
               appSecret: ConfigurationManager.AppSettings["FacebookAppSecret"]);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                //http://stackoverflow.com/questions/19775321/owins-getexternallogininfoasync-always-returns-null
                //ClientId = "95011479004-92lgeivqjnrus4gl741lpilt2q90iupq.apps.googleusercontent.com",
                //ClientSecret = "fizuoG07tgV6K68WSsYA9b-1"
                ClientId = ConfigurationManager.AppSettings["GoogleClientAppId"],
                ClientSecret= ConfigurationManager.AppSettings["GoogleSecretId"]
                
            });
        }
    }
}