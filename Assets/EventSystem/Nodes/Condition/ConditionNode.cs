using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#7b113a")]
[CreateNodeMenu("Condition/Condition")]
public class ConditionNode : Node
{

    [Input(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
    public int input;
    [Input(dynamicPortList = true, backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
    public List<bool> conditions = new List<bool>();

    [Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Multiple)]
    public int pass;
    [Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Multiple)]
    public int fail;
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

public enum ComparisonOperator
{
    greaterThan,
    greaterThanOrEqua,
    lessThan,
    lessThanOrEqual,
    notEqual,
    equal,
}
