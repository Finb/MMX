using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public enum SwitchConditionType
{
    self,
    global
}

public enum SelfSwitchType
{
    A,
    B,
    C,
    D
}

[CreateNodeMenu("Condition/SwitchCondition")]
[NodeWidth(240)]
public class SwitchConditionNode : EventBaseNode
{
    public SwitchConditionType conditionType = SwitchConditionType.self;
    public SelfSwitchType selfSwitchType = SelfSwitchType.A;
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

    public override bool trigger()
    {
        if (conditionType == SwitchConditionType.self)
        {
            return true;
        }
        else{
            return true;
        }
    }
}