using System.Reflection;

namespace JMERGE
{
    public abstract class MethodDrawer
    {
        public abstract void DrawMethod(UnityEngine.Object target, MethodInfo methodInfo);
    }
}
