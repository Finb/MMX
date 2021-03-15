using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerExecuter : IExecute
{
    public void execute(EventAction eventAction)
    {
        if (CheckCondition(eventAction)){
            eventAction.eventAction1?.execute();
        }
        else{
            eventAction.eventAction2?.execute();
        }
    }

    bool CheckCondition(EventAction eventAction){
		bool pass = false;

		if(eventAction.conditionChecker == ConditionChecker.GreaterOrEqual){
			if(GlobalChecker.shared.checkValue(eventAction.stringVar1) >= eventAction.intVar1){
				pass = true;
			}
		}else if(eventAction.conditionChecker == ConditionChecker.Greater){
			if(GlobalChecker.shared.checkValue(eventAction.stringVar1)  > eventAction.intVar1){
				pass = true;
			}
		}else if(eventAction.conditionChecker == ConditionChecker.Equal){
			if(GlobalChecker.shared.checkValue(eventAction.stringVar1)  == eventAction.intVar1){
				pass = true;
			}
		}else if(eventAction.conditionChecker == ConditionChecker.LessOrEqual){
			if(GlobalChecker.shared.checkValue(eventAction.stringVar1)  <= eventAction.intVar1){
				pass = true;
			}
		}else if(eventAction.conditionChecker == ConditionChecker.Less){
			if(GlobalChecker.shared.checkValue(eventAction.stringVar1)  < eventAction.intVar1){
				pass = true;
			}
		}else if(eventAction.conditionChecker == ConditionChecker.NotEqual){
			if(GlobalChecker.shared.checkValue(eventAction.stringVar1)  != eventAction.intVar1){
				pass = true;
			}
		}
		return pass;
	}
}
