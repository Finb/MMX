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
                EditorGUILayout.PropertyField(serializedObject.FindProperty("childEventAction"), true);
                break;

            case EventActionType.teleport:
                EditorGUILayout.LabelField("自身配置");
                EditorGUI.indentLevel++;
                action.vector2Var1 = EditorGUILayout.Vector2Field("自身传送偏移量", action.vector2Var1);
                EditorGUILayout.LabelField("传送到这个传送点时，需要偏移的位置", EditorStyles.miniLabel);
                EditorGUI.indentLevel--;


                EditorGUILayout.LabelField("传送配置");
                EditorGUI.indentLevel++;
                action.audioClipVar1 = (AudioClip)EditorGUILayout.ObjectField("传送音效", action.audioClipVar1, typeof(AudioClip), true);
                EditorGUILayout.LabelField("传送到指定传送点");

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
                EditorGUI.indentLevel--;

                break;

            case EventActionType.Wait:
            action.floatVar1 = EditorGUILayout.FloatField("等待时间(单位秒)",action.floatVar1);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("childEventAction"), true);
            EditorGUILayout.LabelField("等待完毕后，子任务只有一个则直接执行，多个则会弹出执行选择框选择执行", EditorStyles.miniLabel);
            break;

            case EventActionType.SendMessage:
            action.stringVar1 = EditorGUILayout.TextField("消息名称", action.stringVar1);
			action.transformVar1 = EditorGUILayout.ObjectField("发送目标", action.transformVar1 , typeof(Transform), true) as Transform;
            EditorGUILayout.LabelField("为空则给自己发送消息", EditorStyles.miniLabel);
            break;
        }

        if (GUI.changed) { EditorUtility.SetDirty(target); }
        serializedObject.ApplyModifiedProperties();
    }
}
