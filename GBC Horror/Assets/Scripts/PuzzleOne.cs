using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PuzzleOne : MonoBehaviour
{
    public int redDolls = 0;
    public int greenDolls = 0;
    public int blueDolls = 0;
    
    public TextMeshProUGUI dialOne;
    public TextMeshProUGUI dialTwo;
    public TextMeshProUGUI dialThree;

    public GameObject chestGameObject;
    private Chest chest;
    
    void Start()
    {
        InitializeText();
        chest = chestGameObject.GetComponent<Chest>();
    }

    void InitializeText()
    {
        dialOne.text = "0";
        dialTwo.text = "0";
        dialThree.text = "0";
    }
    
    public void Increase(Button button)
    {
        TextMeshProUGUI dialToChange = GetDial(button);

        #if UNITY_EDITOR
                Debug.Log("Button pressed: " + button.name);
        #endif
        
        if (CheckDial(dialToChange))
        {
            int val = int.Parse(dialToChange.text);
            ++val;
            if (val > 9)
            {
                val = 0;
            }

            dialToChange.text = val.ToString();
        }

        CheckSolution();
    }

    public void Decrease(Button button)
    {
        TextMeshProUGUI dialToChange = GetDial(button);

        #if UNITY_EDITOR
            Debug.Log("Button pressed: " + button.name);
        #endif
        
        if (CheckDial(dialToChange))
        {
            int val = int.Parse(dialToChange.text);
            --val;
            if (val < 0)
            {
                val = 9;
            }

            dialToChange.text = val.ToString();
        }

        CheckSolution();
    }

    bool CheckDial(TextMeshProUGUI dial)
    {
        if (!dial)
        {
            #if UNITY_EDITOR
                Debug.Log("dialToChange was not initialized");
            #endif
            
            return false;
        }

        return true;
    }
    
    TextMeshProUGUI GetDial(Button button)
    {
        Transform[] transforms = button.GetComponentsInParent<Transform>();
        GameObject parent = transforms[1].gameObject;

        TextMeshProUGUI dialToChange = new TextMeshProUGUI();
        if (parent.name.Equals("Dial One"))
        {
            dialToChange = dialOne;
        }
        else if (parent.name.Equals("Dial Two"))
        {
            dialToChange = dialTwo;
        }
        else if (parent.name.Equals("Dial Three"))
        {
            dialToChange = dialThree;
        }

        #if UNITY_EDITOR
            Debug.Log(parent.name);
        #endif
        
        return dialToChange;
    }

    void CheckSolution()
    {
        if ((int.Parse(dialOne.text) == redDolls) 
            && (int.Parse(dialTwo.text) == greenDolls) 
            && (int.Parse(dialThree.text) == blueDolls))
        {
            GetComponent<AudioSource>().Play();
            chest.Unlock();
        }
    }
}
