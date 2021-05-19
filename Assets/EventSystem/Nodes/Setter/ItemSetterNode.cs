using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateNodeMenu("Setter/HumanItemSetter", 100)]
public class HumanItemSetterNode : EventBaseNode
{
    [Input]
    public bool input;
    public string itemId;
    public VariableOperation operation = VariableOperation.add;
    public int count;

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
        var selectedItem = ItemPack.shared.findItem<MMX.HumanItem>(itemId);
        var selectedCount = selectedItem != null ? selectedItem.count : 0;
        switch (operation)
        {
            case VariableOperation.set:
                selectedCount = count;
                break;
            case VariableOperation.add:
                selectedCount += count;
                break;
            case VariableOperation.sub:
                selectedCount -= count;
                break;
            case VariableOperation.mul:
                selectedCount *= count;
                break;
            case VariableOperation.div:
                selectedCount /= count;
                break;
            case VariableOperation.mod:
                selectedCount %= count;
                break;
        }
        ItemPack.shared.setItem(itemId, selectedCount);
        return true;
    }

}