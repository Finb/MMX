using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMX.Input;


[System.Serializable]
public class RoleInfoAction
{
    public RoleInfoActionType actionType;

    [Header("动作名称")]
    [RoleInfoActionTypeAttribute(RoleInfoActionType.dialogue)]
    public string text;

    [TextArea]
    [Header("对话文本")]
    [RoleInfoActionTypeAttribute(RoleInfoActionType.dialogue)]
    public string dialogueText;


    [RoleInfoActionTypeAttribute(RoleInfoActionType.dialogue)]
    public RoleInfoAction[] childRoleInfoActions;


}


[System.Serializable]
public enum RoleInfoActionType : int
{
    dialogue = 0,
    humanItem,
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