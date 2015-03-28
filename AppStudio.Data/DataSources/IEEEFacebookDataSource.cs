using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class IEEEFacebookDataSource : DataSourceBase<FacebookSchema>
    {
        private const long OAuthKey = 12790;
        private const string UserId = "176104589110851";

        protected override string CacheKey
        {
            get { return "IEEEFacebookDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<FacebookSchema>> LoadDataAsync()
        {
            try
            {
                var facebookDataProvider = new FacebookDataProvider(UserId, OAuthTokensRepository.GetTokens(OAuthKey));
                return await facebookDataProvider.LoadAsync();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("IEEEFacebookDataSourceDataSource.LoadData", ex.ToString());
                return new FacebookSchema[0];
            }
        }
    }
}
