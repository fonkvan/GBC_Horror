using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public TextMeshProUGUI interactUI;
    protected bool playerInRange = false;
    protected bool inTrigger = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowUI();
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideUI();
            inTrigger = false;
        }
    }
    
    protected void ShowUI()
    {
        interactUI.enabled = true;
        playerInRange = true;
    }

    protected void HideUI()
    {
        interactUI.enabled = false;
        playerInRange = false;
    }
}
