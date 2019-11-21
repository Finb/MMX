using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleInfo : MonoBehaviour, InputEventInterface
{
    public AnimationClip downAnimation;
    public AnimationClip leftAnimation;
    public AnimationClip rightAnimation;
    public AnimationClip upAnimation;

    [Header("从多远可以与角色交互")]
    public float raycastDistance = 1f;
    [Header("角色昵称")]
    public string nick;
    [Header("对话文本")]
    [TextArea]
    public string dialogueText;


    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        var overrideAinmator = new AnimatorOverrideController(anim.runtimeAnimatorController);
        if (downAnimation != null) overrideAinmator["主角1_move_down"] = downAnimation;
        if (leftAnimation != null) overrideAinmator["主角1_move_left"] = leftAnimation;
        if (rightAnimation != null) overrideAinmator["主角1_move_right"] = rightAnimation;
        if (upAnimation != null) overrideAinmator["主角1_move_up"] = upAnimation;
        anim.runtimeAnimatorController = overrideAinmator;
    }

    public void willOnFocus(){

    }

    public void willLostFocus(){

    }

    // Update is called once per frame
    public void inputAction()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            var hit = Physics2D.Raycast(gameObject.transform.position, GetComponent<Movement>().lookDirection, 3, 1 << 8);
            if (hit.collider == null){
                return;
            }
            var roleInfo = hit.collider.gameObject.GetComponent<RoleInfo>();
            if (roleInfo == null)
            {
                return;
            }
            if (hit.distance > roleInfo.raycastDistance)
            {
                return;
            }
            if (roleInfo.dialogueText.Length > 0)
            {
                Dialog.shared.showText(roleInfo.dialogueText, roleInfo.nick);
            }

        }
    }
}
