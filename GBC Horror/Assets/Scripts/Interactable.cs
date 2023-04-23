using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public TextMeshProUGUI interactUI;
    protected bool playerInRange = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowUI();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideUI();
        }
    }
    
    private void ShowUI()
    {
        interactUI.enabled = true;
        playerInRange = true;
    }

    private void HideUI()
    {
        interactUI.enabled = false;
        playerInRange = false;
    }
}
