using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiglogController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void invokeInputAction(){
        if (Dialog.shared.isInteroperable && Input.GetKey(KeyCode.J)){
            Dialog.shared.continueDiglog();
        }
    }
}
