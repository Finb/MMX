using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(SwitchConditionNode))]
public class SwitchConditionEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        EditorGUIUtility.labelWidth = 100;
        SwitchConditionNode node = target as SwitchConditionNode;

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("conditionType"));
        if (node.conditionType == SwitchConditionType.self)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("selfSwitchType"));
        }
        else
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("conditionKey"));
        }
        NodeEditorGUILayout.PortField(target.GetOutputPort("output"));
    }
}