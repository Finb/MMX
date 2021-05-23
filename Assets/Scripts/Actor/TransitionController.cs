using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public System.Action transitionInCompletion;
    public System.Action transitionOutCompletion;
    public bool isReady = true;
    public void OnTransitionInAnimationComplete()
    {
        Debug.Log("屏幕黑了");
        if (transitionInCompletion != null)
        {
            transitionInCompletion();
        }

    }

    // This method should be called by a keyframe event
    public void OnTransitionOutAnimationComplete()
    {
        Debug.Log("屏幕亮了");
        isReady = true;
        if (transitionOutCompletion != null)
        {
            transitionOutCompletion();
        }
        transitionInCompletion = null;
        transitionOutCompletion = null;
    }

    public void startScreenFade()
    {
        isReady = false;
        var screenFader = GameObject.FindWithTag("ScreenFader");
        var screenFaderAnimator = screenFader.GetComponent<Animator>();
        screenFaderAnimator.SetTrigger("FadeInTrigger");
    }

    public void endScreenFade()
    {
        var screenFader = GameObject.FindWithTag("ScreenFader");
        var screenFaderAnimator = screenFader.GetComponent<Animator>();
        screenFaderAnimator.SetTrigger("FadeOutTrigger");
    }
}
