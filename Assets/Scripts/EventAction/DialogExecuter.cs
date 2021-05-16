using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogExecuter : IExecute
{
    public void execute(EventAction eventAction)
    {
        var dialog = DialogController.shared;
        dialog.showText(eventAction.stringVar2, eventAction.stringVar1, () =>
        {
            if (eventAction.childEventAction.Length > 0)
            {
                var boxController = SelectButtonBoxController.Create();
                foreach (var item in eventAction.childEventAction)
                {
                    boxController.addButton(item.eventName, () =>
                    {
                        dialog.hide();
                        item.execute();
                    });
                    boxController.canCancel = !eventAction.boolVar1;
                }
                boxController.show();
            }
        });
    }
}
