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
    public Vector2 teleportPosition = Vector2.zero;

    public AudioClip soundEffect;


    public override bool trigger()
    {
        if (soundEffect == null)
        {
            soundEffect = Resources.Load<AudioClip>("MetalMax-SFX/0x3B-Stairs");
        }
        TeleportTargetInfo teleportInfo;
        if (teleporterName?.Length > 0)
        {
            teleportInfo = new TeleportTargetInfo(mapName, roomName, teleporterName, soundEffect);
        }
        else
        {
            teleportInfo = new TeleportTargetInfo(mapName, roomName, teleportPosition, soundEffect);
        }
        TeleportManager.shared.teleport(teleportInfo);
        return true;
    }
}