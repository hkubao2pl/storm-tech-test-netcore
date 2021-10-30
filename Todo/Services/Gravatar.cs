using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace Todo.Services
{
    public static class Gravatar
    {
        public static Dictionary<string, string> _cache = new Dictionary<string, string>();
        private static Timer _timer;
        static Gravatar()
        {
            _timer = new Timer(1000 * 3600);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            for (int i = _cache.Count - 1; i > -1; i--)
            {
                if (string.IsNullOrEmpty(_cache.ElementAt(i).Value))
                {
                    _cache.Remove(_cache.ElementAt(i).Key);
                }
            }
        }

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public async static Task<XElement?> GetUserProfile(string address)
        {
            HttpClient client = new()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
            XElement profile = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(address);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                profile = XElement.Parse(responseBody);
            }
            catch (HttpRequestException)
            {
                //it should be logged
            }
            return profile;
        }

        public async static ValueTask<string> GetUserName(string emailAddress)
        {
            string username = null;
            if (_cache.ContainsKey(emailAddress))
            {
                return _cache[emailAddress];
            }
            else
            {
                //string address = "https://www.gravatar.com/205e460b479e2e5b48aec07710c08d50.xml"; //<- used to test

                string gravatarhash = Gravatar.GetHash(emailAddress);
                string address = $"https://www.gravatar.com/{gravatarhash}.xml";
                XElement? profile = await GetUserProfile(address);
                if (profile != null)
                {
                    username = profile?.Element("entry")?.Element("displayName")?.Value;
                }
                _cache.Add(emailAddress, username ?? string.Empty);
                return _cache[emailAddress];
            }

        }
    }
}