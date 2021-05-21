using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(SwitchSetterNode))]
public class SwitchSetterEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        EditorGUIUtility.labelWidth = 100;
        SwitchSetterNode node = target as SwitchSetterNode;

        NodeEditorGUILayout.PortField(target.GetInputPort("input"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("type"));
        if (node.type == SwitchConditionType.self)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("selfSwitchType"));
        }
        else
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("switchkey"));
        }
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("switchValue"));

    }
}