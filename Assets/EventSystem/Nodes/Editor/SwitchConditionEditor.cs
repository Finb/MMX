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


[CustomNodeEditor(typeof(VariableConditionNode))]
public class VariableConditionEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        EditorGUIUtility.labelWidth = 120;
        base.OnBodyGUI();
    }
}

[CustomNodeEditor(typeof(VariableSetterNode))]
public class VariableSetterEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        EditorGUIUtility.labelWidth = 100;
        base.OnBodyGUI();
    }
}

[CustomNodeEditor(typeof(ItemConditionNode))]
public class ItemConditionSetterEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        var node = target as ItemConditionNode;

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
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("comparisonOperator"), new GUIContent("Operator"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("count"));
        NodeEditorGUILayout.PortField(target.GetOutputPort("output"));

    }
}