using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventActionType
{
    none,
    dialogue, //弹出对话
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

    public string intVar1;
    public string intVar2;

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
        getExecuter().execute(this);
    }
    private IExecute getExecuter(){
        return new DialogExecuter();
    }
}
