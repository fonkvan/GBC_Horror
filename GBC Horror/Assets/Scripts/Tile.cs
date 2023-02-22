using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool state;
    private PuzzleThree pThree;

    private void Start()
    {
        state = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = !state;
            pThree.StateChange(state);
        }
    }
}
