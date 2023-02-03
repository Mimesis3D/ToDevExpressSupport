using System;

namespace Core.Module.Extensions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependantViewAttribute : Attribute
    {
        public Type Type { get; private set; }
        public string TargetRegionName { get; private set; }
        public DependantViewAttribute(Type viewType, string targetRegionName)
        {
            Type = viewType;
            TargetRegionName = targetRegionName;
        }
    }
}
