using System;

namespace JMERGE
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyFieldAttribute : DrawerAttribute
    {
    }
}
