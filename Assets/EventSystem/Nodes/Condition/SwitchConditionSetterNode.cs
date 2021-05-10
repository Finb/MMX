using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Condition/SwitchConditionSetter", 100)]
public class SwitchConditionSetterNode : Node
{
    [Input]
    public bool input;
    public SwitchConditionType conditionType = SwitchConditionType.self;
    public SelfSwitchType selfSwitchType = SelfSwitchType.A;
    public string conditionKey;
    public bool conditionValue;
}