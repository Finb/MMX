using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;
[CreateAssetMenu]
public class EventGraph : NodeGraph
{
    public string eventId;
    public void trigger(TriggerType type)
    {

        var triggerNodes = this.nodes.Where((node) =>
        {
            if (node is TriggerNode)
            {
                return (node as TriggerNode).triggerType == type;
            }
            return false;
        })
        .Select(node => node as TriggerNode)
        .OrderByDescending(node => node.priority);

        foreach (var node in triggerNodes)
        {
            if (node.trigger())
            {
                break;
            }
        }
    }
}