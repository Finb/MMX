using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public enum VariableOperation
{
    set,
    add,
    sub,
    mul,
    div,
    mod,
}

[CreateNodeMenu("Condition/VariableConditionSetter", 101)]
public class VariableConditionSetterNode : Node
{
    [Input]
    public bool intput;
    public string conditionKey;
    public VariableOperation variableOperation = VariableOperation.set;
    public int conditionValue;
}