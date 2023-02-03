using System;
using Prism.Mvvm;

namespace ModuleA.ViewModels
{
    public class ViewCViewModel : BindableBase
    {
        private string? _title;
        public string? Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewCViewModel()
        {
            Title = "View C";
        }
    }
}
