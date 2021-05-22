using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(HumanItemSetterNode))]
public class HumanItemSetterEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        var node = target as HumanItemSetterNode;

        EditorGUIUtility.labelWidth = 60;
        NodeEditorGUILayout.PortField(target.GetInputPort("input"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("itemId"));
        if (node.itemId != null && node.itemId.Length > 0)
        {
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleRight };
            if (MMX.ItemStorage.shared.items.ContainsKey(node.itemId))
            {
                var item = MMX.ItemStorage.shared.items[node.itemId];
                EditorGUILayout.LabelField(item.name, style, GUILayout.ExpandWidth(true));
            }
            else
            {
                EditorGUILayout.LabelField("Invalid Item Id", style, GUILayout.ExpandWidth(true));
            }
        }
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("operation"), new GUIContent("Operator"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("count"));

    }
}
