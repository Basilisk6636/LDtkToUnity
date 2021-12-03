﻿using UnityEditor;
using UnityEngine.Internal;

#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif

namespace LDtkUnity.Editor
{
    [ExcludeFromDocs]
    [CustomEditor(typeof(LDtkLevelImporter))]
    public class LDtkLevelImporterEditor : ScriptedImporterEditor
    {
        protected override bool needsApplyRevert => false;

        public override void OnInspectorGUI()
        {
            //draw nothing
        }
    }
}