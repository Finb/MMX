using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;
using UnityEditor;

[CustomNodeEditor(typeof(HumanItemConditionNode))]
public class HumanItemConditionEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();

        var node = target as HumanItemConditionNode;

        EditorGUIUtility.labelWidth = 60;
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
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("comparisonOperator"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("count"));
        NodeEditorGUILayout.PortField(target.GetOutputPort("output"));

        serializedObject.ApplyModifiedProperties();
    }
}
