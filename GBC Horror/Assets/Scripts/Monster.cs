using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving = false;

    private Animator fmAnim;

    private void Start()
    {
        fmAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving && GameManager.Instance.monsterCanMove)
        {
            StartCoroutine(ChasePlayer());
        }
    }

    IEnumerator ChasePlayer()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;

        if (!player)
        {
            yield break;
        }
        
        isMoving = true;
        fmAnim.SetBool("fmIsMoving", isMoving);
        Vector3 playerPos = player.transform.position;
        Vector3 goalPos = transform.position;
        float moveX = 0f;
        float moveY = 0f;

        if (Mathf.Abs(playerPos.x - transform.position.x) > Mathf.Abs(playerPos.y - transform.position.y))
        {
            if (playerPos.x > transform.position.x)
            {
                moveX = 1;
                fmAnim.SetFloat("moveDirection", moveX);
            }
            else
            {
                moveX = -1;
                fmAnim.SetFloat("moveDirection", moveX);
            }
        }
        else
        {
            if (playerPos.y > transform.position.y)
            {
                moveY = 1;
            }
            else
            {
                moveY = -1;
            }
        }

        goalPos.x += moveX;
        goalPos.y += moveY;

        while ((goalPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        fmAnim.SetBool("fmIsMoving", isMoving);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.Instance.ResetGame();
        }
    }

    public void Default()
    {
        isMoving = false;
        fmAnim.SetBool("fmIsMoving", isMoving);
        gameObject.transform.position = GameManager.Instance.defaultMonsterPos;
        gameObject.SetActive(false);
    }
}
