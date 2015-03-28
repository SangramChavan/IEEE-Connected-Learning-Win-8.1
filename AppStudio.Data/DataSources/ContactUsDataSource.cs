using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class ContactUsDataSource : DataSourceBase<MenuSchema>
    {
        private const string _file = "/Assets/Data/ContactUsDataSource.json";

        protected override string CacheKey
        {
            get { return "ContactUsDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<MenuSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticDataProvider(_file);
                return await serviceDataProvider.Load<MenuSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("ContactUsDataSource.LoadData", ex.ToString());
                return new MenuSchema[0];
            }
        }

    }
}
