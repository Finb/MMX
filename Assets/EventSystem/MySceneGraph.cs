using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class MySceneGraph : SceneGraph<EventGraph>
{
    private void Awake()
    {
        graph = graph.Copy() as EventGraph;
        graph.trigger(TriggerType.AutoStart);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        graph = graph.Copy() as EventGraph;
        graph.trigger(TriggerType.TriggerEnter);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        graph = graph.Copy() as EventGraph;
        graph.trigger(TriggerType.CollideEnter);
    }
}
