using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#7b113a")]
[CreateNodeMenu("Condition/Condition")]
public class ConditionNode : EventBaseNode
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
    public override bool trigger()
    {
        var success = true;
        foreach (var item in this.DynamicInputs)
        {
            var nodes = item.GetConnections().ConvertAll<EventBaseNode>((connection) => connection.node as EventBaseNode);
            foreach (var node in nodes)
            {
                if (!node.trigger())
                {
                    success = false;
                    break;
                }
            }
        }
        if (success)
        {
            (this.GetOutputPort("pass").Connection?.node as EventBaseNode)?.trigger();
        }
        else
        {
            (this.GetOutputPort("fail").Connection?.node as EventBaseNode)?.trigger();
        }
        return true;
    }
}
