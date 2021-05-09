using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#7b113a")]
[CreateNodeMenu("Condition/Condition")]
public class ConditionNode : EventBaseNode
{

    // Use this for initialization

    [Input(dynamicPortList = true, backingValue = ShowBackingValue.Never)]
    public List<bool> conditions = new List<bool>();
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

[CreateNodeMenu("Condition/SwitchCondition")]
public class SwitchConditionNode : Node
{
    public enum SwitchConditionType
    {
        self,
        global
    }
    public SwitchConditionType conditionType = SwitchConditionType.self;
    public string conditionKey;
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


public enum ComparisonOperator
{
    greaterThan,
    greaterThanOrEqua,
    lessThan,
    lessThanOrEqual,
    notEqual,
    equal,
}

[CreateNodeMenu("Condition/VariableCondition")]
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

[CreateNodeMenu("Condition/SwitchConditionSetter", 100)]
public class SwitchConditionSetterNode : Node
{
    [Input]
    public bool intput;
    public string conditionKey;
    public bool conditionValue;
}
[CreateNodeMenu("Condition/VariableConditionSetter", 101)]
public class VariableConditionSetterNode : Node
{
    [Input]
    public bool intput;
    public string conditionKey;
    public int conditionValue;
}
