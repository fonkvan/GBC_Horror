using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public Sprite statueFullSprite;
    private SpriteRenderer renderer;
    private bool spriteChanged = false;
    private Sprite statueEmptySprite;

    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        statueEmptySprite = renderer.sprite;
    }
    private void Update()
    {
        if (GameManager.Instance.gemsSet)
        {
            if (!spriteChanged)
            {
                renderer.sprite = statueFullSprite;
                spriteChanged = true;
            }
        }
        else
        {
            if (spriteChanged)
            {
                spriteChanged = false;
                renderer.sprite = statueEmptySprite;
            }
        }
    }
}
