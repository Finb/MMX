using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

[CreateNodeMenu("Condition/HumanItemCondition", 102)]
[NodeWidth(240)]
public class HumanItemConditionNode : EventBaseNode
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

    public override bool trigger()
    {

        var selectedItem = ItemPack.shared.findItem<MMX.HumanItem>(itemId);
        var selectedItemCount = selectedItem != null ? selectedItem.count : 0;

        switch (comparisonOperator)
        {
            case ComparisonOperator.equal:
                return selectedItemCount == count;
            case ComparisonOperator.greaterThan:
                return selectedItemCount > count;
            case ComparisonOperator.greaterThanOrEqua:
                return selectedItemCount >= count;
            case ComparisonOperator.lessThan:
                return selectedItemCount < count;
            case ComparisonOperator.lessThanOrEqual:
                return selectedItemCount <= count;
            case ComparisonOperator.notEqual:
                return selectedItemCount != count;
        }

        return false;
    }
}