using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Condition/ItemCondition", 102)]
[NodeWidth(240)]
public class ItemConditionSetterNode : Node
{
    public string itemId;
    public ComparisonOperator comparisonOperator = ComparisonOperator.greaterThanOrEqua;
    public int count;
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