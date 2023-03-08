using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public Sprite statueFullSprite;
    private SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (GameManager.Instance.gemsSet)
        {
            GameManager.Instance.gemsSet = false;
            renderer.sprite = statueFullSprite;
        }
    }
}
