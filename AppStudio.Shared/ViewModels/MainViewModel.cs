using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private AboutUsViewModel _aboutUsModel;
       private CatalogViewModel _catalogModel;
       private ToolsViewModel _toolsModel;
       private RssViewModel _rssModel;
       private IEEEFacebookViewModel _iEEEFacebookModel;
       private ContactUsViewModel _contactUsModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = AboutUsModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public AboutUsViewModel AboutUsModel
        {
            get { return _aboutUsModel ?? (_aboutUsModel = new AboutUsViewModel()); }
        }
 
        public CatalogViewModel CatalogModel
        {
            get { return _catalogModel ?? (_catalogModel = new CatalogViewModel()); }
        }
 
        public ToolsViewModel ToolsModel
        {
            get { return _toolsModel ?? (_toolsModel = new ToolsViewModel()); }
        }
 
        public RssViewModel RssModel
        {
            get { return _rssModel ?? (_rssModel = new RssViewModel()); }
        }
 
        public IEEEFacebookViewModel IEEEFacebookModel
        {
            get { return _iEEEFacebookModel ?? (_iEEEFacebookModel = new IEEEFacebookViewModel()); }
        }
 
        public ContactUsViewModel ContactUsModel
        {
            get { return _contactUsModel ?? (_contactUsModel = new ContactUsViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            AboutUsModel.ViewType = viewType;
            CatalogModel.ViewType = viewType;
            ToolsModel.ViewType = viewType;
            RssModel.ViewType = viewType;
            IEEEFacebookModel.ViewType = viewType;
            ContactUsModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                AboutUsModel.LoadItemsAsync(forceRefresh),
                CatalogModel.LoadItemsAsync(forceRefresh),
                ToolsModel.LoadItemsAsync(forceRefresh),
                RssModel.LoadItemsAsync(forceRefresh),
                IEEEFacebookModel.LoadItemsAsync(forceRefresh),
                ContactUsModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
