using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogueSceneGraph : BaseSceneGraph
{
    public string nick;
    [TextArea(3, 10)]
    public string content;
    public override void setup()
    {
        if (this.graph == null)
        {
            this.graph = Resources.Load("EventGraph/Simple Dialogue Event Graph") as EventGraph;
        }
        base.setup();
        graph.nodes.ForEach((node) =>
        {
            if (node is DialogueNode dialogueNode && dialogueNode != null)
            {
                dialogueNode.nick = nick;
                dialogueNode.content = content;
            }
        });
    }
}

