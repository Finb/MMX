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

[CustomNodeEditor(typeof(SwitchConditionSetterNode))]
public class SwitchConditionSetterEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        EditorGUIUtility.labelWidth = 100;
        SwitchConditionSetterNode node = target as SwitchConditionSetterNode;

        NodeEditorGUILayout.PortField(target.GetInputPort("input"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("conditionType"));
        if (node.conditionType == SwitchConditionType.self)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("selfSwitchType"));
        }
        else
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("conditionKey"));
        }
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("conditionValue"));

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

[CustomNodeEditor(typeof(VariableConditionSetterNode))]
public class VariableConditionSetterEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        EditorGUIUtility.labelWidth = 100;
        base.OnBodyGUI();
    }
}

[CustomNodeEditor(typeof(ItemConditionSetterNode))]
public class ItemConditionSetterEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        var node = target as ItemConditionSetterNode;

        EditorGUIUtility.labelWidth = 60;
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("itemId"));
        if (node.itemId.Length > 0)
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