using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paper : Interactable
{
    public GameObject asset;
    private bool paperActive = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !paperActive)
        {
            ShowAsset();
            return;
        }

        if (paperActive)
        {
            if (!playerInRange || (Input.GetKeyDown(KeyCode.E) && paperActive))
            {
                HideAsset();
            }
        }
    }
    
    private void ShowAsset()
    {
        asset.SetActive(true);
        paperActive = true;
    }

    private void HideAsset()
    {
        asset.SetActive(false);
        paperActive = false;
    }
}
