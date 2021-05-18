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
public class VariableSetterNode : EventBaseNode
{
    [Input]
    public bool intput;
    public string variableKey;
    public VariableOperation variableOperation = VariableOperation.set;
    public int value;

    public override bool trigger()
    {
        switch (variableOperation)
        {
            case VariableOperation.set:
                VariableManager.shared[variableKey] = value;
                break;
            case VariableOperation.add:
                VariableManager.shared[variableKey] += value;
                break;
            case VariableOperation.sub:
                VariableManager.shared[variableKey] -= value;
                break;
            case VariableOperation.mul:
                VariableManager.shared[variableKey] *= value;
                break;
            case VariableOperation.div:
                VariableManager.shared[variableKey] /= value;
                break;
            case VariableOperation.mod:
                VariableManager.shared[variableKey] %= value;
                break;
        }
        return true;
    }
}