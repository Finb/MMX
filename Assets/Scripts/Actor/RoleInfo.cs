using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleInfo : MonoBehaviour
{
    public AnimationClip downAnimation;
    public AnimationClip leftAnimation;
    public AnimationClip rightAnimation;
    public AnimationClip upAnimation;
    
    [Header("从多远可以与角色交互")]
    public float raycastDistance = 1f;

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        var overrideAinmator = new AnimatorOverrideController(anim.runtimeAnimatorController);
        if (downAnimation != null) overrideAinmator["主角1_move_down"] =  downAnimation;
        if (leftAnimation != null)  overrideAinmator["主角1_move_left"] = leftAnimation;
        if (rightAnimation != null)  overrideAinmator["主角1_move_right"] = rightAnimation;
        if (upAnimation != null)  overrideAinmator["主角1_move_up"] = upAnimation;
        anim.runtimeAnimatorController = overrideAinmator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
