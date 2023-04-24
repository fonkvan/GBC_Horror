using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private PlayerController playerController;
    private PuzzleOne puzzleController;
    private float canvasActivationTime;
    private SpriteRenderer spriteRenderer;
    public GameObject dial;
    public Sprite openedChest;
    public GameObject reward;

    void Start()
    {
        puzzleController = FindObjectOfType<PuzzleOne>();
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
            && dial.activeSelf 
            && playerController 
            && (Time.time - canvasActivationTime >= 0.25f))
        {
            canvasActivationTime = 0f;
            playerController.enabled = true;
            dial.gameObject.SetActive(false);
        }
    }
    
    public void Interact()
    {
        if (dial && !GameManager.Instance.puzzleOneSolved)
        {
            dial.gameObject.SetActive(true);
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
        dial.gameObject.SetActive(false);
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
