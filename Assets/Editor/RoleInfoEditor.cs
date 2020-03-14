using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoleInfo))]//关联之前的脚本
public class RoleInfoEditor : Editor
{
    private SerializedObject obj;//序列化
    private SerializedProperty action;
    void OnEnable()
    {
        action = serializedObject.FindProperty("action");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
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
                {
                    EditorGUILayout.PropertyField(tProp, true);
                }
            }
            while (tProp.NextVisible(false));
        }

        serializedObject.ApplyModifiedProperties();//应用
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
        if (actionType == RoleInfoActionType.none)
        {
            return;
        }


        foreach (System.Reflection.FieldInfo field in typeof(RoleInfoAction).GetFields())
        {
            if (field.Name == "actionName")
            {
                var nameProp = tProp.FindPropertyRelative(field.Name);
                EditorGUILayout.PropertyField(nameProp);
            }
            foreach (RoleInfoActionTypeAttribute item in field.GetCustomAttributes(typeof(RoleInfoActionTypeAttribute), false))
            {
                if (item.getRoleInfoType() == actionType)
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
                    else
                    {
                        EditorGUILayout.PropertyField(tProp.FindPropertyRelative(field.Name), true);
                    }
                }
            }
        }

        EditorGUI.indentLevel--;
    }
}