using System;
using Prism.Commands;
using Prism.Mvvm;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _title = "View A";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand UpdateCommand { get; private set; }

        public ViewAViewModel()
        {
            UpdateCommand = new DelegateCommand(UpdateTitle);
        }

        private void UpdateTitle()
        {
            Title = String.Format("Updated {0}", DateTime.Now);
        }
    }

}
