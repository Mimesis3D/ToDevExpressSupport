using Core.Module.Interfaces;
using DevExpress.Xpf.Ribbon;

namespace ModuleA.RibbonTabs
{
    /// <summary>
    /// Interaction logic for ViewBTab.xaml
    /// </summary>
    public partial class ViewBTab : RibbonPage, ISupportDataContext
    {
        public ViewBTab()
        {
            InitializeComponent();
        }
    }
}
