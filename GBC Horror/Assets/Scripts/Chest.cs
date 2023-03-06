using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool locked = true;

    public void Unlock()
    {
        locked = false;
        #if UNITY_EDITOR
            Debug.Log("UNLOCKED!");
        #endif
    }
}
