using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMX.Input;


[System.Serializable]
public class RoleInfoAction
{
    public RoleInfoActionType actionType;

    [Header("事件名称")]
    public string actionName;

    [TextArea]
    [Header("对话文本")]
    [RoleInfoActionTypeAttribute(RoleInfoActionType.dialogue)]
    public string dialogueText;


    [RoleInfoActionTypeAttribute(RoleInfoActionType.dialogue)]
    [Header("子事件")]
    public RoleInfoAction[] childRoleInfoActions;


}


[System.Serializable]
public enum RoleInfoActionType : int
{
    none = 0,
    dialogue = 1, //弹出对话
    humanItem = 2, //人类道具店
}



[System.AttributeUsage(System.AttributeTargets.Class |
                          System.AttributeTargets.Struct | System.AttributeTargets.Field,
                          AllowMultiple = true)
   ]
public class RoleInfoActionTypeAttribute : System.Attribute
{
    RoleInfoActionType type;

    public RoleInfoActionTypeAttribute(RoleInfoActionType type)
    {
        this.type = type;
    }

    public RoleInfoActionType getRoleInfoType()
    {
        return type;
    }

}