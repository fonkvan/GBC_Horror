using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private bool waitToDestroyMonster = false;
    private Animator spikesAnim;

    private void Start()
    {
        spikesAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (waitToDestroyMonster)
        {
            //check if anim done playing
            //if true then:
            spikesAnim.SetTrigger("spikesActive");
            GameManager.Instance.DestroyMonster();
            waitToDestroyMonster = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Monster"))
        {
            ActivateTrap();
        }
    }

    private void ActivateTrap()
    {
        GameManager.Instance.monsterCanMove = false;
        //play anim
        spikesAnim.SetTrigger("spikesActive");
        waitToDestroyMonster = true;
    }
}
