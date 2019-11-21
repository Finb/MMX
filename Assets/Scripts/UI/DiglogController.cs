using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogController : MonoBehaviour, InputEventInterface
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void willOnFocus(){

    }
    public void willLostFocus(){

    }
    public void inputAction(){
        if (Dialog.shared.isInteroperable && Input.GetKey(KeyCode.J)){
            Dialog.shared.continueDiglog();
        }
    }
}
