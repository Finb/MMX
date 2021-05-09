using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu(null)]
public class EventBaseNode : Node
{

    [Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.None, connectionType = ConnectionType.Override)]
    public int input;
    [Output(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.None, connectionType = ConnectionType.Multiple)]
    public int output;

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