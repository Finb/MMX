using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Setter/ItemSetter", 100)]
public class ItemSetterNode : Node
{
    [Input]
    public bool input;
    public string itemId;
    public VariableOperation operation = VariableOperation.add;
    public int count;

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