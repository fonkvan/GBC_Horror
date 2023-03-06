using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private PlayerController playerController;
    private float canvasActivationTime;
    private SpriteRenderer spriteRenderer;
    public Canvas canvas;
    public Sprite openedChest;
    public GameObject reward;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        if (GameManager.Instance.puzzleOneSolved)
        {
            Unlock();
        }
    }

    void Update()
    {
        if (!GameManager.Instance.puzzleOneSolved 
            && Input.GetKeyDown(KeyCode.E) 
            && canvas.enabled 
            && playerController 
            && (Time.time - canvasActivationTime >= 0.25f))
        {
            canvasActivationTime = 0f;
            playerController.enabled = true;
            canvas.gameObject.SetActive(false);
        }
    }
    
    public void Interact()
    {
        if (canvas && !GameManager.Instance.puzzleOneSolved)
        {
            canvas.gameObject.SetActive(true);
            playerController.enabled = false;
            canvasActivationTime = Time.time;
            
            #if UNITY_EDITOR
                Debug.Log("We have a canvas!");
            #endif
        }
    }
    
    public void Unlock()
    {
        playerController.enabled = true;
        spriteRenderer.sprite = openedChest;
        canvas.gameObject.SetActive(false);
        if (!GameManager.Instance.redCollected)
        {
            reward.SetActive(true);
        }
        GameManager.Instance.puzzleOneSolved = true;
        
        #if UNITY_EDITOR
            Debug.Log("UNLOCKED!");
        #endif
    }
}
