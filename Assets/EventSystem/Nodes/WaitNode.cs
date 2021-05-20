using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : EventBaseNode
{
    [Input(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
    public int input;
    [Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Multiple)]
    public int output;

    public float duration = 0f;
    public override bool trigger()
    {
        MMX.GameManager.Input.StartCoroutine(run());
        return true;
    }
    IEnumerator run()
    {
		//将焦点交给地图，地图不会响应任何操作，所以可以冻结操作
		MMX.GameManager.Input.pushTarget(MMX.GameManager.map);
        yield return new WaitForSeconds(duration);
		//解除冻结
		MMX.GameManager.Input.popTarget();
        foreach (var connection in this.GetOutputPort("output").GetConnections())
        {
            (connection.node as EventBaseNode).trigger();
        }
    }
}