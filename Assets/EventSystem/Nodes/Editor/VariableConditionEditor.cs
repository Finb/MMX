using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(VariableConditionNode))]
public class VariableConditionEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        EditorGUIUtility.labelWidth = 120;
        base.OnBodyGUI();
    }
}