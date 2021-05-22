using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;



[CreateNodeMenu("Setter/VariableSetter", 101)]
public class VariableSetterNode : EventBaseNode
{
    [Input]
    public bool intput;
    public string variableKey;
    public VariableOperation variableOperation = VariableOperation.set;
    public int value;

    public override bool trigger()
    {
        VariableManager.shared[variableKey] = variableOperation.operation(VariableManager.shared[variableKey], value);
        return true;
    }
}