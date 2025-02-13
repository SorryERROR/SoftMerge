﻿using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace JMERGE
{
    [CustomEditor(typeof(JMergeSettingsPanel))]
    public class JMergeSettingsPanelEditor : Editor
    {
        private const string SETTINGS_ANIMATION_PROPERTY_NAME = "jMergeSettingsAnimation";
        private const string X_PANEL_POSITION_PROPERTY_NAME = "xPanelPosition";
        private const string Y_PANEL_POSITION_PROPERTY_NAME = "yPanelPosition";
        private const string ELEMENT_SPACE_PROPERTY_NAME = "elementSpace";
        private const string SETTINGS_BUTTONS_PROPERTY_NAME = "settingsButtonsInfo";

        private ReorderableList reorderableList;

        private JMergeSettingsPanel jMergeSettingsPanel;

        private SerializedProperty settingsAnimationProperty;
        private SerializedProperty xPanelPositionProperty;
        private SerializedProperty yPanelPositionProperty;
        private SerializedProperty elementSpaceProperty;
        private SerializedProperty settingsButtonsProperty;

        protected void OnEnable()
        {
            jMergeSettingsPanel = target as JMergeSettingsPanel;

            settingsAnimationProperty = serializedObject.FindProperty(SETTINGS_ANIMATION_PROPERTY_NAME);
            xPanelPositionProperty = serializedObject.FindProperty(X_PANEL_POSITION_PROPERTY_NAME);
            yPanelPositionProperty = serializedObject.FindProperty(Y_PANEL_POSITION_PROPERTY_NAME);
            elementSpaceProperty = serializedObject.FindProperty(ELEMENT_SPACE_PROPERTY_NAME);
            settingsButtonsProperty = serializedObject.FindProperty(SETTINGS_BUTTONS_PROPERTY_NAME);

            reorderableList = new ReorderableList(serializedObject, settingsButtonsProperty, true, true, true, true);

            reorderableList.drawHeaderCallback += DrawHeader;
            reorderableList.drawElementCallback += DrawElement;

            reorderableList.onAddCallback += AddItem;
            reorderableList.onRemoveCallback += RemoveItem;
            reorderableList.onReorderCallback += ReorderItems;
        }

        private void OnDisable()
        {
            reorderableList.drawHeaderCallback -= DrawHeader;
            reorderableList.drawElementCallback -= DrawElement;

            reorderableList.onAddCallback -= AddItem;
            reorderableList.onRemoveCallback -= RemoveItem;
            reorderableList.onReorderCallback -= ReorderItems;
        }

        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Buttons");
        }

        private void DrawElement(Rect rect, int index, bool active, bool focused)
        {
            JMergeSettingsPanel.SettingsButtonInfo item = jMergeSettingsPanel.SettingsButtonsInfo[index];

            EditorGUI.BeginChangeCheck();
            item.JmergeSettingsButton = (JMERGESettingsButtonBase)EditorGUI.ObjectField(new Rect(rect.x + 18, rect.y, rect.width - 18, 20), item.JmergeSettingsButton, typeof(JMERGESettingsButtonBase), true);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
                //settingsPanel.OnValidate();
            }
        }

        private void AddItem(ReorderableList list)
        {
            var index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;

            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("settingsButton").objectReferenceValue = null;
        }

        private void RemoveItem(ReorderableList list)
        {
            list.serializedProperty.serializedObject.Update();
            list.serializedProperty.DeleteArrayElementAtIndex(list.index);
            list.serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private void ReorderItems(ReorderableList list)
        {
            //settingsPanel.OnValidate();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.Space(5);

            Object tempObject = settingsAnimationProperty.objectReferenceValue;
            EditorGUILayout.PropertyField(settingsAnimationProperty);
            if(tempObject != settingsAnimationProperty.objectReferenceValue)
            {
                if(settingsAnimationProperty.objectReferenceValue != null)
                {
                    if(EditorApplication.isPlaying)
                    {
                        JMergeSettingsAnimation jMergeSettingsAnimation = (JMergeSettingsAnimation)settingsAnimationProperty.objectReferenceValue;
                        jMergeSettingsAnimation.Init(jMergeSettingsPanel);
                    }
                }
            }

            EditorGUILayout.PropertyField(xPanelPositionProperty);
            EditorGUILayout.PropertyField(yPanelPositionProperty);
            EditorGUILayout.PropertyField(elementSpaceProperty);

            GUILayout.Space(8);

            reorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
