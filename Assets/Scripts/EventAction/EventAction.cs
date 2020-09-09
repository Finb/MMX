using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventActionType
{
    none,
    //弹出对话
    dialogue, 
    //传送
    teleport,
    humanItem, //人类道具店
}
//触发条件
public enum StartConditions
{
    //不触发
    none,
    //按键触发
    KeyTrigger,
    //碰撞
    Collide,
    //Trigger
    TriggerEnter,
}

public interface IExecute {
    void execute(EventAction eventAction);
}

[AddComponentMenu("EventAction/EventAction")]
public class EventAction : MonoBehaviour
{
    //事件类型
    public EventActionType type;
    //事件名称
    public string eventName;
    //事件触发方式
    public StartConditions startCondition;


    //事件参数
    public string stringVar1;
    public string stringVar2;
    public string stringVar3;
    public string stringVar4;
    public string stringVar5;

    public string intVar1;
    public string intVar2;

    public AudioClip audioClipVar1;
    public AudioClip audioClipVar2;

    public Vector2 vector2Var1;
    public Vector2 vector2Var2;

    public Transform transformVar1;
    public Transform transformVar2;

    public EventAction[] childEventAction;

    public ScriptableObject scriptableObjectVar1;
    

    public void OnKeyTrigger(Collider other) {

    }
    private void OnTriggerEnter2D(Collider2D other){
        if (startCondition != StartConditions.TriggerEnter || other.gameObject != TeamQueue.shared.captain.gameObject) {
            return;
        }
        execute();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (startCondition != StartConditions.Collide || other.gameObject != TeamQueue.shared.captain.gameObject) {
            return;
        }
        execute();   
    }
    public void execute(){
        getExecuter()?.execute(this);
    }
    private IExecute getExecuter(){
        switch (type){
            case EventActionType.dialogue:
            return new DialogExecuter();
            case EventActionType.teleport:
            return new TeleportExecuter();
        }
        return null;
    }
}
