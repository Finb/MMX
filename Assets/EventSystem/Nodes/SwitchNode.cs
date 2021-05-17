using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;
[NodeTint("#c06014")]
public class SwitchNode : EventBaseNode
{
    [Input(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
    public int input;

    [TextArea(3, 10)]
    public string content;
    [Output(dynamicPortList = true, typeConstraint = TypeConstraint.None, connectionType = ConnectionType.Override)]
    public List<string> options = new List<string>();
    public bool canCancel;

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
        if (options.Count > 0)
        {
            DialogController.shared.showText(content, null, () =>
            {
                var boxController = SelectButtonBoxController.Create();
                foreach (var item in options.Select((value, i) => (value, i)))
                {
                    boxController.addButton(item.value, () =>
                    {
                        var port = this.GetOutputPort("options " + item.i);
                        if (port != null)
                        {
                            (port.Connection?.node as EventBaseNode)?.trigger();
                        }
                        DialogController.shared.hide();
                    });
                    boxController.canCancel = this.canCancel;
                }
                boxController.show();
            }, true);
        }
        return true;
    }
}