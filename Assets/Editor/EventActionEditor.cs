using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(EventAction))]
public class EventActionEditor : Editor
{
    public override void OnInspectorGUI(){
        
        EventAction action = (EventAction)target;

        action.type = (EventActionType)EditorGUILayout.EnumPopup("事件名称", action.type);
        action.eventName = EditorGUILayout.TextField("事件名称", action.eventName);
        action.startCondition = (StartConditions)EditorGUILayout.EnumPopup("触发方式", action.startCondition);

        switch(action.type) {
            
            case EventActionType.dialogue:
            action.stringVar1 = EditorGUILayout.TextField("昵称",action.stringVar1);
            action.stringVar2 = EditorGUILayout.TextArea(action.stringVar2, GUILayout.Height(60));
            layoutChildAction();    
            break;

        }
    }

    void layoutChildAction(){
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("childEventAction"),true);
        serializedObject.ApplyModifiedProperties();
    }
}
