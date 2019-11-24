using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoleInfo))]//关联之前的脚本
public class RoleInfoEditor : Editor
{
    private SerializedObject obj;//序列化
    private SerializedProperty roleInfoActions;
    void OnEnable()
    {
        obj = new SerializedObject(target);
        roleInfoActions = obj.FindProperty("roleInfoActions");

    }
    public override void OnInspectorGUI()
    {
        obj.Update();

        SerializedProperty tProp = serializedObject.GetIterator();
        if (tProp.NextVisible(true))
        {
            do
            {
                if (tProp.name == "roleInfoActions")
                {
                    tProp = roleInfoActions.Copy();
                    EditorGUILayout.PropertyField(tProp, false);
                    if (tProp.isArray && tProp.isExpanded)
                    {

                        EditorGUI.indentLevel++;
                        tProp.arraySize = EditorGUILayout.DelayedIntField("Size", tProp.arraySize);
                        for (int i = 0, size = tProp.arraySize; i < size; i++)
                        {
                            var element = tProp.GetArrayElementAtIndex(i);
                            EditorGUILayout.PropertyField(element, false);
                            if (element.isExpanded)
                            {
                                EditorGUI.indentLevel++;
                                var actionTypeElement = element.FindPropertyRelative("actionType");
                                EditorGUILayout.PropertyField(actionTypeElement);
                                var actionType = (RoleInfoActionType)actionTypeElement.intValue;

                                foreach (System.Reflection.FieldInfo field in typeof(RoleInfoAction).GetFields())
                                {
                                    foreach (RoleInfoActionTypeAttribute item in field.GetCustomAttributes(typeof(RoleInfoActionTypeAttribute), false))
                                    {
                                        if (item.getRoleInfoType() == actionType){
                                            EditorGUILayout.PropertyField(element.FindPropertyRelative(field.Name));
                                        }
                                    }
                                }

                                
                                EditorGUI.indentLevel--;
                            }
                        }
                        EditorGUI.indentLevel--;
                        EditorGUILayout.Space();
                    }
                }
                // DrawMyStuff(tProp.Copy());
                // Otherwise normal draw (include the children)
                else
                    EditorGUILayout.PropertyField(tProp, true);

                // EditorGUI.EndDisabledGroup();
            }
            // Skip the children (the draw will handle it)
            while (tProp.NextVisible(false));
        }

        obj.ApplyModifiedProperties();//应用
    }
}