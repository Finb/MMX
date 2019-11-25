using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoleInfo))]//关联之前的脚本
public class RoleInfoEditor : Editor
{
    private SerializedObject obj;//序列化
    private SerializedProperty action;
    void OnEnable()
    {
        obj = new SerializedObject(target);
        action = obj.FindProperty("action");

    }
    public override void OnInspectorGUI()
    {
        obj.Update();

        SerializedProperty tProp = serializedObject.GetIterator();
        if (tProp.NextVisible(true))
        {
            do
            {
                if (tProp.name == "action")
                {
                    tProp = action.Copy();
                    layoutRowInfoAction(tProp);
                }
                else
                    EditorGUILayout.PropertyField(tProp, true);
            }
            while (tProp.NextVisible(false));
        }

        obj.ApplyModifiedProperties();//应用
    }

    private void layoutRowInfoAction(SerializedProperty tProp)
    {
        EditorGUILayout.PropertyField(tProp, false);
        if (!tProp.isExpanded)
        {
            return;
        }

        EditorGUI.indentLevel++;
        var actionTypeElement = tProp.FindPropertyRelative("actionType");
        EditorGUILayout.PropertyField(actionTypeElement);
        var actionType = (RoleInfoActionType)actionTypeElement.intValue;

        foreach (System.Reflection.FieldInfo field in typeof(RoleInfoAction).GetFields())
        {
            foreach (RoleInfoActionTypeAttribute item in field.GetCustomAttributes(typeof(RoleInfoActionTypeAttribute), false))
            {
                if (field.Name == "childRoleInfoActions")
                {

                    var childRoleInfoActions = tProp.FindPropertyRelative(field.Name);
                    EditorGUILayout.PropertyField(childRoleInfoActions);
                    if (childRoleInfoActions.isExpanded)
                    {
                        childRoleInfoActions.arraySize = EditorGUILayout.DelayedIntField("Size", childRoleInfoActions.arraySize);
                        EditorGUI.indentLevel++;
                        for (int i = 0, size = childRoleInfoActions.arraySize; i < size; i++)
                        {

                            var element = childRoleInfoActions.GetArrayElementAtIndex(i);
                            layoutRowInfoAction(element);
                        }
                        EditorGUI.indentLevel--;
                    }
                    
                }
                else if (item.getRoleInfoType() == actionType)
                {
                    EditorGUILayout.PropertyField(tProp.FindPropertyRelative(field.Name), false);
                }
            }
        }

        EditorGUI.indentLevel--;


        // if (tProp.isExpanded)
        // {
        //     EditorGUI.indentLevel++;
        //     tProp.arraySize = EditorGUILayout.DelayedIntField("Size", tProp.arraySize);
        //     for (int i = 0, size = tProp.arraySize; i < size; i++)
        //     {
        //         var element = tProp.GetArrayElementAtIndex(i);
        //         EditorGUILayout.PropertyField(element, false);
        //         if (element.isExpanded)
        //         {
        //             EditorGUI.indentLevel++;
        //             var actionTypeElement = element.FindPropertyRelative("actionType");
        //             EditorGUILayout.PropertyField(actionTypeElement);
        //             var actionType = (RoleInfoActionType)actionTypeElement.intValue;

        //             foreach (System.Reflection.FieldInfo field in typeof(RoleInfoAction).GetFields())
        //             {
        //                 foreach (RoleInfoActionTypeAttribute item in field.GetCustomAttributes(typeof(RoleInfoActionTypeAttribute), false))
        //                 {
        //                     if (field.Name == "childRoleInfoActions")
        //                     {
        //                         Debug.Log("bbbbbbb");
        //                         layoutRowInfoAction(element.FindPropertyRelative(field.Name));
        //                     }
        //                     else if (item.getRoleInfoType() == actionType)
        //                     {
        //                         EditorGUILayout.PropertyField(element.FindPropertyRelative(field.Name), false);
        //                     }
        //                 }
        //             }


        //             EditorGUI.indentLevel--;
        //         }
        //     }
        //     EditorGUI.indentLevel--;
        //     EditorGUILayout.Space();
        // }
    }
}