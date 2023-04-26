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
            StartCoroutine(WaitToDestroy());
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
        spikesAnim.SetTrigger("spikesActive");
        waitToDestroyMonster = true;
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1);
        spikesAnim.SetTrigger("spikesActive");
        GameManager.Instance.DestroyMonster();
        waitToDestroyMonster = false;
    }
}
