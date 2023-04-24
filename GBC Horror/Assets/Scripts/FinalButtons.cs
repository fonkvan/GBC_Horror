using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalButtons : Interactable
{
    private bool buttonsChanged = false;
    private bool pressedThisButton = false;
    public Sprite buttonActive;
    public Sprite buttonDown;
    
   void Update()
    {
        if (!GameManager.Instance.gemsSet || pressedThisButton)
        {
            HideUI();
        }
        else if (!buttonsChanged)
        {
            Activate();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && inTrigger)
            {
                GetComponent<SpriteRenderer>().sprite = buttonDown;
                if (!pressedThisButton)
                {
                    pressedThisButton = true;
                    GameManager.Instance.buttonsPressed += 1;
                }
            }
        }
    }

   private void Activate()
   {
       GetComponent<SpriteRenderer>().sprite = buttonActive;
       buttonsChanged = true;
   }
}
