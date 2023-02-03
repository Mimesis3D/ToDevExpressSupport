using DevExpress.Mvvm;
using Prism.Regions;

namespace RibbonAppExample.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        IRegionManager _regionManager;

        public DelegateCommand<string> NavigateCommand { get; set; }

        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        void Navigate(string navigationPath)
        {
            _regionManager.RequestNavigate("ContentRegion", navigationPath);
        }
    }
}
