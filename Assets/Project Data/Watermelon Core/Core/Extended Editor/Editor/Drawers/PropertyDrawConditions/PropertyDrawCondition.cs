using UnityEditor;

namespace JMERGE
{
    public abstract class PropertyDrawCondition
    {
        public abstract bool CanDrawProperty(SerializedProperty property);
    }
}
