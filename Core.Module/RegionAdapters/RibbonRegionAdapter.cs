using System.Collections.Specialized;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Ribbon.Internal;
using Prism.Regions;

namespace Core.Module.RegionAdapters
{
    public class RibbonRegionAdapter : RegionAdapterBase<RibbonControl>
    {
        protected RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, RibbonControl regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
                {
                    foreach (var view in e.NewItems)
                    {
                        AddviewToRegion(view, regionTarget);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
                {
                    foreach (var view in e.OldItems)
                    {
                        RemoveViewFromRegion(view, regionTarget);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
        void AddviewToRegion(object view, RibbonControl regionTarget)
        {
            var ribbonTabItem = view as BaseRibbonItem;
            if (ribbonTabItem != null)
            {
                regionTarget.Items.Add(ribbonTabItem);
            }
        }
        void RemoveViewFromRegion(object view, RibbonControl regionTarget)
        {
            var ribbonTabItem = view as BaseRibbonItem;
            if (ribbonTabItem != null)
            {
                regionTarget.Items.Remove(ribbonTabItem);
            }
        }
    }

}
