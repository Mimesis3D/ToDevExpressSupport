using System.Windows.Controls;
using Core.Module;
using Core.Module.Extensions;
using Core.Module.Interfaces;
using ModuleA.RibbonTabs;

namespace ModuleA.Views
{
    [DependantView(typeof(ViewATab), RegionNames.RibbonTabRegion)]
    [DependantView(typeof(ViewATab), RegionNames.RibbonTabRegion)]
    [DependantView(typeof(ViewBTab), RegionNames.RibbonTabRegion)]
    public partial class ViewA : UserControl, ISupportDataContext
    {
        public ViewA()
        {
            InitializeComponent();
        }
    }
}
