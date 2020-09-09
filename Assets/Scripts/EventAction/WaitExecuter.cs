using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitExecuter : IExecute
{
    public void execute(EventAction eventAction)
    {
        eventAction.StartCoroutine(Waiting(eventAction));
    }
    IEnumerator Waiting(EventAction eventAction)
    {
        yield return new WaitForSeconds(eventAction.floatVar1);
        if (eventAction.childEventAction.Length == 1)
        {
            eventAction.childEventAction[0].execute();
        }
        else if (eventAction.childEventAction.Length > 1)
        {
            var boxController = SelectButtonBoxController.Create();
            foreach (var item in eventAction.childEventAction)
            {
                boxController.addButton(item.eventName, () =>
                {
                    item.execute();
                });
            }
            boxController.show();
        }
    }
}
