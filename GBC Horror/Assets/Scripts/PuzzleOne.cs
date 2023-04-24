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

    public Image dialOneBG;
    public Image dialTwoBG;
    public Image dialThreeBG;
    public int highlightIndex = 0;

    public GameObject chestGameObject;
    private Chest chest;
    
    void Start()
    {
        InitializeText();
        SwitchHighlighted(0);
        chest = chestGameObject.GetComponent<Chest>();
    }

    private void Update()
    {
        if (chest.dial.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Increase(HighlightedDial());
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Decrease(HighlightedDial());
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                SwitchHighlighted(-1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SwitchHighlighted(1);
            }
        }
    }

    void InitializeText()
    {
        dialOne.text = "0";
        dialTwo.text = "0";
        dialThree.text = "0";
    }
    
    private void SwitchHighlighted(int changeAmount)
    {
        highlightIndex += changeAmount;
        if (highlightIndex == 3)
        {
            highlightIndex = 0;
        }
        else if (highlightIndex == -1)
        {
            highlightIndex = 2;
        }

        if (highlightIndex == 0)
        {
            dialOneBG.gameObject.SetActive(true);
            dialTwoBG.gameObject.SetActive(false);
            dialThreeBG.gameObject.SetActive(false);
        }
        else if (highlightIndex == 1)
        {
            dialOneBG.gameObject.SetActive(false);
            dialTwoBG.gameObject.SetActive(true);
            dialThreeBG.gameObject.SetActive(false);
        }
        else if (highlightIndex == 2)
        {
            dialOneBG.gameObject.SetActive(false);
            dialTwoBG.gameObject.SetActive(false);
            dialThreeBG.gameObject.SetActive(true);
        }
    }

    private TextMeshProUGUI HighlightedDial()
    {
        if (dialOneBG.gameObject.activeSelf)
        {
            return dialOne;
        }
        else if (dialTwoBG.gameObject.activeSelf)
        {
            return dialTwo;
        }
        else if (dialThreeBG.gameObject.activeSelf)
        {
            return dialThree;
        }
        else
        {
            #if UNITY_EDITOR
                Debug.Log("No highlighted dial");
            #endif

            return null;
        }
    }

    public void Increase(TextMeshProUGUI dialToChange)
    {
        /*Button button
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
        */

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

    public void Decrease(TextMeshProUGUI dialToChange)
    {
        /*TextMeshProUGUI dialToChange = GetDial(button);

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
        */

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
    
    /*TextMeshProUGUI GetDial(Button button)
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
    }*/

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
