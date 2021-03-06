﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public AudioClip backgroundMusic;

    public RoomInfo[] rooms {
        get {
            return gameObject.GetComponentsInChildren<RoomInfo>();
        }
    }
}