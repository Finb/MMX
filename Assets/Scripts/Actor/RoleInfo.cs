using UnityEngine;
using MMX.Input;



public class RoleInfo : MonoBehaviour
{
    public AnimationClip downAnimation;
    public AnimationClip leftAnimation;
    public AnimationClip rightAnimation;
    public AnimationClip upAnimation;

    [Header("从多远可以与角色交互")]
    public float raycastDistance = 1f;
    [Header("角色昵称")]
    public string nick;
    public RoleInfoAction action;

    private VehicleInfo _currentTakeVehicle;
    public VehicleInfo currentTakeVehicle
    {
        set
        {
            if (_currentTakeVehicle != null)
            {
                _currentTakeVehicle.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            _currentTakeVehicle = value;
            if (_currentTakeVehicle != null)
            {
                _currentTakeVehicle.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
        get
        {
            return _currentTakeVehicle;
        }
    }

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

    public void playAction(RoleInfoAction action)
    {
        if (action == null)
        {
            return;
        }
        switch (action.actionType)
        {
            case RoleInfoActionType.dialogue:
                var dialog = DialogController.create();
                dialog.showText(action.dialogueText, this.nick, () =>
                {
                    if (action.childRoleInfoActions.Length > 0)
                    {
                        var boxController = SelectButtonBoxController.Create();
                        foreach (var item in action.childRoleInfoActions)
                        {
                            boxController.addButton(item.actionName, () =>
                            {
                                dialog.hide();
                                this.playAction(item);
                            });
                        }
                        boxController.show();
                    }
                });
                break;
            case RoleInfoActionType.follow:
                MMX.GameManager.Queue.enqueue(this.gameObject);
                Debug.Log("跟随");
                break;
        }
    }

}

