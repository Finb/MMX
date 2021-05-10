using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Condition/VariableCondition")]
[NodeWidth(240)]
public class VariableConditionNode : Node
{
    public string conditionKey;
    public ComparisonOperator comparisonOperator = ComparisonOperator.equal;
    public int conditionValue;
    [Output]
    public bool output;

    protected override void Init()
    {
        base.Init();

    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        return null; // Replace this
    }
}
