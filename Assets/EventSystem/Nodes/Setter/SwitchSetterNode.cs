using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Setter/SwitchConditionSetter", 100)]
public class SwitchSetterNode : Node
{
    [Input]
    public bool input;
    public SwitchConditionType type = SwitchConditionType.self;
    public SelfSwitchType selfSwitchType = SelfSwitchType.A;
    public string switchkey;
    public bool switchValue;
}