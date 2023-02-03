using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Core.Module.Extensions;
using Core.Module.Interfaces;
using Prism.Regions;

namespace Core.Module.Behaviors
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        static Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new();
        
        public const string BehaviorKey = RegionBehaviorKeys.DependentViewRegionBehavior;
        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += Views_CollectionChanged;
        }
        void Views_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        List<DependentViewInfo> viewList = new();
                        if (_dependentViewCache.ContainsKey(view))
                        {
                            viewList = _dependentViewCache[view];
                        }
                        else
                        {
                            foreach (var attribute in GetCustomAttributes<DependantViewAttribute>(view.GetType()))
                            {
                                var info = CreateDependentView(attribute);

                                if (info.View is ISupportDataContext && view is ISupportDataContext)
                                {
                                    ((ISupportDataContext)info.View).DataContext = ((ISupportDataContext)view).DataContext;
                                }
                                if (info != null)
                                {
                                    viewList.Add(info);
                                }
                            }
                            if (!_dependentViewCache.ContainsKey(view))
                            {
                                _dependentViewCache.Add(view, viewList);
                            }
                        }
                        viewList.ForEach(x => Region.RegionManager.Regions[x.TargetRegionName].Add(x.View));
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
            {
                foreach (var oldView in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldView))
                    {
                        _dependentViewCache[oldView].ForEach(x => Region.RegionManager.Regions[x.TargetRegionName].Remove(x.View));
                        if (!ShouldKeepAlive(oldView))
                        {
                            _dependentViewCache.Remove(oldView);
                        }
                    }
                }
            }
        }
        
        private DependentViewInfo CreateDependentView(DependantViewAttribute attribute)
        {
            var info = new DependentViewInfo();
            info.TargetRegionName = attribute.TargetRegionName;
            if (attribute.Type != null)
            {
                info.View = Activator.CreateInstance(attribute.Type);
            }
            return info;
        }
        static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
        bool ShouldKeepAlive(object oldView)
        {
            IRegionMemberLifetime? lifetime = GetItemOrContextLifetime(oldView);
            if (lifetime != null)
            {
                return lifetime.KeepAlive;
            }

            RegionMemberLifetimeAttribute? lifetimeAttribute = GetItemOrContentLifetimeAttribute(oldView);
            if (lifetimeAttribute != null)
            {
                return lifetimeAttribute.KeepAlive;
            }
            return true;
        }

        private RegionMemberLifetimeAttribute? GetItemOrContentLifetimeAttribute(object oldView)
        {
            var lifetimeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(oldView.GetType()).FirstOrDefault();
            if (lifetimeAttribute != null)
            {
                return lifetimeAttribute;
            }
            var frameworkElement = oldView as FrameworkElement;
            if (frameworkElement != null && frameworkElement.DataContext != null)
            {
                var dataContext = frameworkElement.DataContext;
                var contextLifetimeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(dataContext.GetType()).FirstOrDefault();
                return contextLifetimeAttribute;
            }
            return null;
        }

        IRegionMemberLifetime? GetItemOrContextLifetime(object oldView)
        {
            var regionLifetime = oldView as IRegionMemberLifetime;
            if (regionLifetime != null)
            {
                return regionLifetime;
            }
            var frameworkElement = oldView as FrameworkElement;
            
            if (frameworkElement != null)
            {
                return frameworkElement.DataContext as IRegionMemberLifetime;
            }
            return null;
        }
    }
    internal class DependentViewInfo 
    {
        public object? View { get; set; }
        public string? TargetRegionName { get; set; }
    }
}
