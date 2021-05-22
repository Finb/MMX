using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using XNodeEditor;
[CustomNodeEditor(typeof(TeleportNode))]
public class TeleportEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();
        EditorGUIUtility.labelWidth = 100;
        TeleportNode node = target as TeleportNode;

        NodeEditorGUILayout.PortField(target.GetInputPort("input"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("mapName"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("roomName"));

        if (node.teleportPosition == Vector2.zero)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("teleporterName"));
        }
        if (node.teleporterName.Length <= 0)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("teleportPosition"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
