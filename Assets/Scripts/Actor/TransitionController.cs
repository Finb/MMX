using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public GameObject messageReceiver;
    public void OnTransitionInAnimationComplete()
    {
        Debug.Log("屏幕黑了");
        messageReceiver.GetComponent<TeleportController>().teleport();
    }

    // This method should be called by a keyframe event
    public void OnTransitionOutAnimationComplete()
    {
        messageReceiver.GetComponent<TeleportController>().teleportFinished();
        Debug.Log("屏幕亮了");
    }
}
