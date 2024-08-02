using System;

namespace JMERGE
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumFlagsAttribute : DrawerAttribute
    {
        public EnumFlagsAttribute()
        {

        }
    }
}
