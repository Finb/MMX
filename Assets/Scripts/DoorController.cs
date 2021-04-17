using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Sprite defaultSprite;

    private int _doorStack;
    int doorStack
    {
        get
        {
            return _doorStack;
        }
        set
        {
            _doorStack = value;
            if (_doorStack == 0)
            {
                isOpen = false;
            }
            else if (_doorStack == 1)
            {
                isOpen = true;
            }
        }
    }

    private bool _isOpen;
    bool isOpen
    {
        get
        {
            return _isOpen;
        }
        set
        {
            if (_isOpen == value)
            {
                return;
            }
            _isOpen = value;
            if (_isOpen)
            {
                this.GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
            MMX.GameManager.Audio.PlaySfx(Resources.Load<AudioClip>("MetalMax-SFX/0x3D-Door"));
        }
    }

    void Start()
    {
        this.defaultSprite = this.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        doorStack += 1;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        doorStack -= 1;
    }


}
