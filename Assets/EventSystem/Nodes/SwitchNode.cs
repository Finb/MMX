using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#c06014")]
public class SwitchNode : EventBaseNode
{


    [Output(dynamicPortList = true, typeConstraint = TypeConstraint.None, connectionType = ConnectionType.Override)]
    public List<string> options = new List<string>();

    // Use this for initialization
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