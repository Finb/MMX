using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BaseSceneGraph : SceneGraph<EventGraph>
{
    public string eventId;
    private void Awake()
    {
        setup();
    }
    public virtual void setup()
    {
        graph = graph.Copy() as EventGraph;
        graph.eventId = eventId;
        if (graph.eventId == null || graph.eventId.Length <= 0)
        {
            //如果没有eventId，则默认用 gameObject.name 做eventId
            graph.eventId = this.gameObject.name;
        }

        graph.trigger(TriggerType.AutoStart);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        graph.trigger(TriggerType.TriggerEnter);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        graph.trigger(TriggerType.CollideEnter);
    }
}
