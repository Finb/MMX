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

[CreateNodeMenu("Setter/VariableSetter", 101)]
public class VariableSetterNode : Node
{
    [Input]
    public bool intput;
    public string variableKey;
    public VariableOperation variableOperation = VariableOperation.set;
    public int value;
}