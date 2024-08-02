using System;

namespace JMERGE
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class BoxGroupAttribute : GroupAttribute
    {
        public BoxGroupAttribute(string name = "") : base(name)
        {
        }
    }
}
