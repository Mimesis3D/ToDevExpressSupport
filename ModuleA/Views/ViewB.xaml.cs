using System.Windows.Controls;
using Core.Module;
using Core.Module.Extensions;
using Core.Module.Interfaces;
using ModuleA.RibbonTabs;
using Prism.Regions;

namespace ModuleA.Views
{
    [DependantView(typeof(ViewBTab), RegionNames.RibbonTabRegion)]
    [DependantView(typeof(ViewCView), RegionNames.SubRegion)]
    public partial class ViewB : UserControl, ISupportDataContext, IRegionMemberLifetime
    {
        public ViewB()
        {
            InitializeComponent();
        }

        public bool KeepAlive { get { return false; } }
    }
}
