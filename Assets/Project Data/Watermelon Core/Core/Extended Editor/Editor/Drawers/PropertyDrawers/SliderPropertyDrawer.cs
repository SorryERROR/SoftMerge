using UnityEngine;
using UnityEditor;

namespace JMERGE
{
    [PropertyDrawer(typeof(SliderAttribute))]
    public class SliderPropertyDrawer : PropertyDrawer
    {
        public override void DrawProperty(SerializedProperty property)
        {
            EditorDrawUtility.DrawHeader(property);

            SliderAttribute sliderAttribute = PropertyUtility.GetAttribute<SliderAttribute>(property);

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUILayout.IntSlider(property, (int)sliderAttribute.MinValue, (int)sliderAttribute.MaxValue);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                EditorGUILayout.Slider(property, sliderAttribute.MinValue, sliderAttribute.MaxValue);
            }
            else
            {
                string warning = sliderAttribute.GetType().Name + " can be used only on int or float fields";
                EditorGUILayout.HelpBox(warning, MessageType.Warning);
                Debug.LogWarning(warning, PropertyUtility.GetTargetObject(property));

                EditorDrawUtility.DrawPropertyField(property);
            }
        }
    }
}