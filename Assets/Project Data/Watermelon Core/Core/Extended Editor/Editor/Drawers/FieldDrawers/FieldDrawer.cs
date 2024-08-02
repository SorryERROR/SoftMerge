using System.Reflection;

namespace JMERGE
{
    public abstract class FieldDrawer
    {
        public abstract void DrawField(UnityEngine.Object target, FieldInfo field);
    }
}
