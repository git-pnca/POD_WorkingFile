using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PodDesignPlugin
{
    public class Credentials
    {
        private readonly string clientId = "c8pjov5tm4eVpfsX3ZtnnNwDrgg0GnG00XQsAFP9L6b95T8v";
        private readonly string clientSecret = "2CRxyAdkDqrUsjPCPtqDD9nQ1rgCu7biXY8AG4z7x8V5zBL8tzqbeCdTLrSzHnHJ";
        private readonly string redirectUri = "http://localhost:8080/callback";
        private readonly string scopes = "data:read viewables:read";

        public async Task<string> Authenticate()
        {
            string authUrl = $"https://developer.api.autodesk.com/authentication/v1/authorize" +
                             $"?response_type=code&client_id={clientId}" +
                             $"&redirect_uri={HttpUtility.UrlEncode(redirectUri)}" +
                             $"&scope={HttpUtility.UrlEncode(scopes)}";

            // Open browser
            Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });

            // Wait for redirect and extract the auth code
            string authCode = await GetAuthorizationCode();

            // Exchange code for access token
            return await GetAccessToken(authCode);
        }

        private async Task<string> GetAuthorizationCode()
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add("http://localhost:8080/callback/");
                listener.Start();

                var context = await listener.GetContextAsync();
                var query = context.Request.QueryString;
                string code = query["code"];

                // Let the user know login worked
                byte[] buffer = Encoding.UTF8.GetBytes("Login successful! You may now return to the app.");
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();

                return code;
            }
        }

        private async Task<string> GetAccessToken(string authCode)
        {
            using (var client = new HttpClient())

            {

                var values = new Dictionary<string, string>
                {
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "grant_type", "authorization_code" },
                    { "code", authCode },
                    { "redirect_uri", redirectUri }
                };



                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://developer.api.autodesk.com/authentication/v1/gettoken", content);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(jsonResponse);
                return json["access_token"];
            }
        
        }
    }
}
