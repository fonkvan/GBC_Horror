using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool state;
    public PuzzleThree pThree;
    public Sprite normal;
    public Sprite pushed;
    private AudioSource click;

    private void Start()
    {
        state = false;
        click = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            state = !state;
            pThree.StateChange(state);
        }

        if (state)
        {
            SetSpritePushed();
        }
        else
        {
            SetSpriteNormal();
        }

        click.Play();
    }

    public void SetSpritePushed()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = pushed;
    }

    public void DisableTrigger()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void SetSpriteNormal()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = normal;
    }
}
