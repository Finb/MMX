using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Setter/SwitchSetter", 100)]
public class SwitchSetterNode : EventBaseNode
{
    [Input]
    public bool input;
    public SwitchConditionType type = SwitchConditionType.self;
    public SelfSwitchType selfSwitchType = SelfSwitchType.A;
    public string switchkey;
    public bool switchValue;

    public override bool trigger()
    {
        if (type == SwitchConditionType.self)
        {
            VariableManager.shared["switch.self." + (graph as EventGraph).eventId + "." + selfSwitchType.ToString()] = switchValue ? 1 : 0;
        }
        else
        {
            VariableManager.shared["switch.global." + switchkey] = switchValue ? 1 : 0;
        }
        return true;
    }
}