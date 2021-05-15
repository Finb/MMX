using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Teleport")]
public class TeleportNode : EventBaseNode
{
    [Input]
    public int input;

    public string mapName;
    public string roomName;
    public string teleporterName;
    public Vector2 teleportPosition;
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