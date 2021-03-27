using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum RoleAnimationType
{
    up,
    down,
    left,
    right
}
public class RoleAnimationBehaviour : PlayableBehaviour
{
    public RoleAnimationType type;
    public RoleInfo role;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        role.GetComponent<Movement>().toward(getDirection());
        role.GetComponent<Movement>().anim.enabled = true;
    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        role.GetComponent<Movement>().anim.enabled = false;
    }

    Vector2 getDirection()
    {
        switch (type)
        {
            case RoleAnimationType.up:
                return Vector2.up;
            case RoleAnimationType.down:
                return Vector2.down;
            case RoleAnimationType.left:
                return Vector2.left;
            case RoleAnimationType.right:
                return Vector2.right;
        }
        return Vector2.zero;
    }
}
