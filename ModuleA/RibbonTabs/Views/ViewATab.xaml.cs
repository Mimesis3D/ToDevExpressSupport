using Core.Module.Interfaces;
using DevExpress.Xpf.Ribbon;

namespace ModuleA.RibbonTabs
{
    /// <summary>
    /// Interaction logic for ViewATab.xaml
    /// </summary>
    public partial class ViewATab : RibbonPage, ISupportDataContext
    {
        public ViewATab()
        {
            InitializeComponent();
        }
    }
}
