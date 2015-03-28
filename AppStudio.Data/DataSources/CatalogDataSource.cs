using System;
using System.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class CatalogDataSource : DataSourceBase<CatalogSchema>
    {
        private const string _appId = "2827a6fc-ad62-4bb9-bf06-2906265aef33";
        private const string _dataSourceName = "8c2dff3a-0c50-4ba8-a9d3-afad33db8f09";
        private Guid _storeId;
        private string _deviceType;
        private bool _isBackgroundAgent;

        public CatalogDataSource(Guid storeId, string deviceType, bool isBackgroundAgent = false)
        {
            _storeId = storeId;
            _deviceType = deviceType;
            _isBackgroundAgent = isBackgroundAgent;
        }

        protected override string CacheKey
        {
            get { return "CatalogDataSource"; }
        }

        public override bool HasStaticData
        {
            get { return false; }
        }

        public async override Task<IEnumerable<CatalogSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new ServiceDataProvider(_appId, _dataSourceName, _storeId, _deviceType, _isBackgroundAgent);
                return await serviceDataProvider.Load<CatalogSchema>();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("CatalogDataSource.LoadData", ex.ToString());
                return new CatalogSchema[0];
            }
        }
    }
}
