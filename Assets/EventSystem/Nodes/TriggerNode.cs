using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
public enum TriggerType
{
    //按键触发
    KeyTrigger,
    //碰撞
    CollideEnter,
    //Trigger
    TriggerEnter,
    //自动开始
    AutoStart,
}
public class TriggerNode : EventBaseNode
{

    public TriggerType triggerType = TriggerType.KeyTrigger;
    public int priority = 0;

    [Input(dynamicPortList = true, backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
    public List<bool> conditions = new List<bool>();

    [Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
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

    public override bool trigger()
    {
        var success = true;
        foreach (var item in this.DynamicInputs)
        {
            var nodes = item.GetConnections().ConvertAll<EventBaseNode>((connection) => connection.node as EventBaseNode);
            foreach(var node in nodes){
                if (!node.trigger()){
                    success = false;
                    break;
                }
            }
        }
        if(success){
            (this.GetOutputPort("output").Connection?.node as EventBaseNode)?.trigger();
        }

        return success;
    }
}