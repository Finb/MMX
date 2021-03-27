using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MoveAnimationBehaviour : PlayableBehaviour
{
    public bool useCurrentPosition = true;
    public Vector2 startPosition;
    public Vector2 endPosition;

    public bool isFirstFrame = true;
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        isFirstFrame = true;
    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        isFirstFrame = true;
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var transform = playerData as Transform;
        if (transform == null)
        {
            return;
        }
        if (isFirstFrame){
            isFirstFrame = false;
            if (useCurrentPosition) {
                startPosition = transform.position;
            }
        }

        var progress = (float)(playable.GetTime() / playable.GetDuration());
        Debug.Log(startPosition);
        transform.position = Vector2.Lerp(startPosition,endPosition,progress);
    }
}
