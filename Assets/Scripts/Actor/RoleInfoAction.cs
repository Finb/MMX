using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMX.Input;


[System.Serializable]
public struct RoleInfoAction
{
    public RoleInfoActionType actionType;

    [TextArea]
    [Header("对话文本")]
    [RoleInfoActionTypeAttribute(RoleInfoActionType.dialogue)]
    [RoleInfoActionTypeAttribute(RoleInfoActionType.humanItem)]
    public string text;


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