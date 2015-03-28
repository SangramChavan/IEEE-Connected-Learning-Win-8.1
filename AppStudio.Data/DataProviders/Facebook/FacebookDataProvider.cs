using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    public class FacebookDataProvider
    {
        private const string _baseUrl = @"https://graph.facebook.com/v2.2";
        private string _userID;
        private string _appID;
        private string _appSecret;

        public FacebookDataProvider(string userID, OAuthTokens tokens)
        {
            this._userID = userID;
            this._appSecret = tokens["AppSecret"];
            this._appID = tokens["AppId"];
        }

        private string PageUrl { get { return string.Format("{0}/{1}/posts?&access_token={2}|{3}", _baseUrl, _userID, _appID, _appSecret); } }

        public async Task<ObservableCollection<FacebookSchema>> LoadAsync()
        {
            try
            {
                string searchResult = await DownloadAsync(new Uri(PageUrl, UriKind.Absolute));
                var searchList = JsonConvert.DeserializeObject<FacebookGraphResponse>(searchResult);
                ObservableCollection<FacebookSchema> resultToReturn = new ObservableCollection<FacebookSchema>();
                foreach (var i in searchList.data.Where(w => !string.IsNullOrEmpty(w.message) && !string.IsNullOrEmpty(w.link)).OrderBy(o => o.created_time))
                {
                    var item = new FacebookSchema();
                    item.Author = i.from.name;
                    item.PublishDate = i.created_time;
                    item.Title = Utility.DecodeHtml(i.message);
                    item.Summary = Utility.DecodeHtml(i.message);
                    item.Content = i.message;
                    item.ImageUrl = ConvertImageUrlFromParameter(i.picture);
                    item.FeedUrl = i.link;
                    resultToReturn.Add(item);
                }
                return resultToReturn;
            }
            catch (WebException wex)
            {
                HttpWebResponse response = wex.Response as HttpWebResponse;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                        return GenerateErrorFacebook("The keys provided have been revoked or deleted.");
                }
                throw;
            }
        }

        private static ObservableCollection<FacebookSchema> GenerateErrorFacebook(string text)
        {
            return new ObservableCollection<FacebookSchema>()
            {
                new FacebookSchema()
                {
                    FeedUrl = string.Empty,
                    Title = text,
                    ImageUrl = "ms-appx:///Assets/ErrorImage.png"                    
                }
            };
        }

        private string ConvertImageUrlFromParameter(string imageUrl)
        {
            string parsedImageUrl = null;
            try
            {
                if (!string.IsNullOrEmpty(imageUrl) && imageUrl.IndexOf("url=") > 0)
                {
                    Uri imageUri = new Uri(imageUrl);
                    var imageUriQuery = imageUri.Query.Replace("?", "").Replace("&amp;", "&");

                    var imageUriQueryParameters = imageUriQuery.Split('&').Select(q => q.Split('='))
                           .Where(s => s != null && s.Length >= 2)
                           .ToDictionary(k => k[0], v => v[1]);

                    parsedImageUrl = WebUtility.UrlDecode(imageUriQueryParameters["url"]);
                }
                else if (!string.IsNullOrEmpty(imageUrl))
                {
                    parsedImageUrl = WebUtility.HtmlDecode(imageUrl);
                }
            }
            catch
            {
            }

            return parsedImageUrl;
        }

        private async Task<string> DownloadAsync(Uri url)
        {
            WebRequest request = WebRequest.CreateHttp(url);
            request.UseDefaultCredentials = true;
            request.Method = "GET";
            var response = await request.GetResponseAsync();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
