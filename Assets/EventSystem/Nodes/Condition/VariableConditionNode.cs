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

    public override bool trigger()
    {
        return comparisonOperator.comparison(VariableManager.shared[conditionKey], conditionValue);
    }
}

[CreateNodeMenu("Condition/SpecialVariableCondition")]
[NodeWidth(240)]
public class SpecialVariableConditionNode : EventBaseNode
{
    public SpecialVariableKey conditionKey;
    public ComparisonOperator comparisonOperator = ComparisonOperator.equal;
    public int conditionValue;
    [Output]
    public bool output;

    public override bool trigger()
    {
        return comparisonOperator.comparison(VariableManager.shared[conditionKey], conditionValue);
    }
}
