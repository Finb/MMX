using UnityEngine;
using XNode;

[NodeTint("#006DAB")]
[CreateNodeMenu("Dialogue", -100)]
public class DialogueNode : EventBaseNode
{

    [Input(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
    public int input;
    [Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Multiple)]
    public int output;

    public string nick;
    [TextArea(3, 10)]
    public string content;



    // Use this for initialization
    protected override void Init()
    {
        base.Init();

    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        return null; // Replace this
    }

    public override bool trigger()
    {
        DialogController.shared.showText(content, nick, () =>
        {
            (this.GetOutputPort("output").Connection?.node as EventBaseNode)?.trigger();
            DialogController.shared.hide();
        });
        return true;
    }
}