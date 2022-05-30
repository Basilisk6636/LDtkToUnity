﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace LDtkUnity.Editor
{
    internal class LDtkSectionDependencies : LDtkSectionDrawer
    {
        private readonly SerializedObject _serializedObject;
        private string[] _dependencies;
        private Object[] _dependencyAssets;
        
        protected override string GuiText => "Dependencies";
        protected override string GuiTooltip => "Dependencies that were defined since the last import.\n" +
                                                "If any of these dependencies save changes, then this asset will automatically reimport.";
        protected override Texture GuiImage => LDtkIconUtility.GetUnityIcon("UnityEditor.FindDependencies", "");
        protected override string ReferenceLink => LDtkHelpURL.SECTION_DEPENDENCIES;
        protected override bool SupportsMultipleSelection => false;
        
        public LDtkSectionDependencies(SerializedObject serializedObject) : base(serializedObject)
        {
            _serializedObject = serializedObject;
            UpdateDependencies();
        }

        public void UpdateDependencies() //originally this was updated less regularly for performance, but was difficult to find the right event for.
        {
            if (_serializedObject == null)
            {
                return;
            }
            
            SerializedProperty dependenciesProp = _serializedObject.FindProperty(LDtkJsonImporter<LDtkProjectFile>.PROP_DEPENDENCIES);
            _dependencies = new string[dependenciesProp.arraySize];
            _dependencyAssets = new Object[dependenciesProp.arraySize];

            for (int i = 0; i < dependenciesProp.arraySize; i++)
            {
                _dependencies[i] = dependenciesProp.GetArrayElementAtIndex(i).stringValue;
                _dependencyAssets[i] = AssetDatabase.LoadAssetAtPath<Object>(_dependencies[i]);
            }
        }

        public override void Draw()
        {
            if (_dependencies.IsNullOrEmpty())
            {
                return;
            }
            LDtkEditorGUIUtility.DrawDivider();
            base.Draw();
        }
        
        protected override void DrawDropdownContent()
        {
            for (int i = 0; i < _dependencies.Length; i++)
            {
                string dependency = _dependencies[i];
                Object dependencyAsset = _dependencyAssets[i];

                if (dependencyAsset == null)
                {
                    continue;
                }
                
                GUIContent content = new GUIContent(dependencyAsset.name, dependency);

                using (new LDtkGUIEnabledScope(false))
                {
                    EditorGUILayout.ObjectField(content, dependencyAsset, typeof(Object), false);
                }
            }
        }
    }
}