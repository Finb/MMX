using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class RoleAnimationClip : PlayableAsset
{
    public RoleAnimationBehaviour template = new RoleAnimationBehaviour();
    public RoleAnimationType type;

    [HideInInspector]
    public RoleInfo role;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var director = graph.GetResolver() as PlayableDirector;
        foreach (var playableAssetOutput in director.playableAsset.outputs)
        {
            if (playableAssetOutput.outputTargetType == typeof(RoleInfo))
            {
                role = (RoleInfo)director.GetGenericBinding(playableAssetOutput.sourceObject);
            }
        }
        var playable = ScriptPlayable<RoleAnimationBehaviour>.Create(graph, template);
        var clone = playable.GetBehaviour();
        clone.type = type;
        clone.role = role;

        return playable;
    }
}
