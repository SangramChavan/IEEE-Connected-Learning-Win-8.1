using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class AuthorResourcesCSDataSource : DataSourceBase<HtmlSchema>
    {
        private const string HtmlSource = "/Assets/Data/AuthorResourcesCSDataSource.htm";

        protected override string CacheKey
        {
            get { return "AuthorResourcesCSDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<HtmlSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticHtmlDataProvider(HtmlSource);
                return await serviceDataProvider.Load();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("AuthorResourcesCSDataSource.LoadData", ex.ToString());
                return new HtmlSchema[0];
            }
        }
    }
}
