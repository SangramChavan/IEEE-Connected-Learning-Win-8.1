using System.Collections.Generic;

namespace AppStudio.Data
{
    public static class OAuthTokensRepository
    {
        private static Dictionary<long, OAuthTokens> Tokens { get; set; }

        static OAuthTokensRepository()
        {
            Tokens = new Dictionary<long, OAuthTokens>();


            Tokens.Add(12790, new OAuthTokens
                            {
                                { "AppId", "1546714612256817" },
                                { "AppSecret", "0af1911f9655f3c7502b845ce1d5bc86" }
                            });

            Tokens.Add(12747, new OAuthTokens
                            {
                                { "ApiKey", "AIzaSyAnwkfLR7AObxJUizDl2UtPtoB8CDljhbY" }
                            });

        }

        public static OAuthTokens GetTokens(long key)
        {
            if (Tokens.ContainsKey(key))
            {
                return Tokens[key];
            }
            return null;
        }

    }
}
