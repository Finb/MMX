using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Condition/VariableCondition")]
[NodeWidth(240)]
public class VariableConditionNode : EventBaseNode
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
    public override bool trigger()
    {
        switch (comparisonOperator)
        {
            case ComparisonOperator.equal:
                return VariableManager.shared[conditionKey] == conditionValue;
            case ComparisonOperator.greaterThan:
                return VariableManager.shared[conditionKey] > conditionValue;
            case ComparisonOperator.greaterThanOrEqua:
                return VariableManager.shared[conditionKey] >= conditionValue;
            case ComparisonOperator.lessThan:
                return VariableManager.shared[conditionKey] < conditionValue;
            case ComparisonOperator.lessThanOrEqual:
                return VariableManager.shared[conditionKey] <= conditionValue;
            case ComparisonOperator.notEqual:
                return VariableManager.shared[conditionKey] != conditionValue;
        }
        return false;
    }
}
