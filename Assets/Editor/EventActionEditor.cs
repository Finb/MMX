using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(EventAction))]
public class EventActionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EventAction action = (EventAction)target;

        action.type = (EventActionType)EditorGUILayout.EnumPopup("事件名称", action.type);
        action.eventName = EditorGUILayout.TextField("事件名称", action.eventName);
        action.startCondition = (StartConditions)EditorGUILayout.EnumPopup("触发方式", action.startCondition);

        switch (action.type)
        {

            case EventActionType.dialogue:
                action.stringVar1 = EditorGUILayout.TextField("昵称", action.stringVar1);
                action.stringVar2 = EditorGUILayout.TextArea(action.stringVar2, GUILayout.Height(60));
                layoutChildAction();
                break;

            case EventActionType.teleport:
                action.audioClipVar1 = (AudioClip)EditorGUILayout.ObjectField("传送音效", action.audioClipVar1, typeof(AudioClip), true);
                action.vector2Var1 = EditorGUILayout.Vector2Field("传送偏移量", action.vector2Var1);

                EditorGUILayout.LabelField("使用指定传送点");

                EditorGUI.indentLevel++;
                action.transformVar1 = (Transform)EditorGUILayout.ObjectField("目标传送点", action.transformVar1, typeof(Transform), true);
                EditorGUI.indentLevel--;

                EditorGUILayout.LabelField("或使用");

                EditorGUI.indentLevel++;
                action.stringVar1 = EditorGUILayout.TextField("地图名(MapName)", action.stringVar1);
                action.stringVar2 = EditorGUILayout.TextField("房间名(RoomName)", action.stringVar2);
                EditorGUI.indentLevel++;
                action.stringVar3 = EditorGUILayout.TextField("传送点名称", action.stringVar3);
                EditorGUI.indentLevel--;
                EditorGUILayout.LabelField("或");
                EditorGUI.indentLevel++;
                action.vector2Var2 = EditorGUILayout.Vector2Field("传送点坐标", action.vector2Var2);
                EditorGUI.indentLevel--;

                EditorGUI.indentLevel--;

                break;

        }

        if (GUI.changed) { EditorUtility.SetDirty(target); }
        serializedObject.ApplyModifiedProperties();
    }

    void layoutChildAction()
    {

        EditorGUILayout.PropertyField(serializedObject.FindProperty("childEventAction"), true);


    }
}
