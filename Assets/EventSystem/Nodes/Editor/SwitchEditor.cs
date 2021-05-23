using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(SwitchNode))]
public class SwitchEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();

        NodeEditorGUILayout.PortField(target.GetInputPort("input"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("nick"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("content"));
        NodeEditorGUILayout.DynamicPortList("options", typeof(Object), serializedObject, NodePort.IO.Output, Node.ConnectionType.Override);
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("canCancel"));

        serializedObject.ApplyModifiedProperties();
    }
}
