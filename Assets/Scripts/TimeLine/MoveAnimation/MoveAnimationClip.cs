using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class MoveAnimationClip : PlayableAsset
{
    public MoveAnimationBehaviour templete = new MoveAnimationBehaviour();
    public bool useCurrentPosition = true;
    public Vector2 startPosition;
    public Vector2 endPosition;

    public ClipCaps clipCaps => ClipCaps.Blending;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MoveAnimationBehaviour>.Create(graph, templete);
        var clone = playable.GetBehaviour();

        clone.useCurrentPosition = useCurrentPosition;
        clone.startPosition = startPosition;
        clone.endPosition = endPosition;

        return playable;

    }
}
