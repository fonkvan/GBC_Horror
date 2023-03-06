using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy : MonoBehaviour
{
    public List<GameObject> solution;
    
    [SerializeField]
    private static List<GameObject> currentGuess;

    private void Start()
    {
        currentGuess = new List<GameObject>();
    }

    public void Interact()
    {
        AddGameObjectToList();
        if (currentGuess.Count == 5)
        {
            if (CheckGuess())
            {
                EndPuzzle();
            }
            else
            {
                #if UNITY_EDITOR
                    Debug.Log("Incorrect Solution!");
                #endif
                ResetPuzzle();
            }
        }
    }

    private void AddGameObjectToList()
    {
        #if UNITY_EDITOR
            Debug.Log(gameObject.name);
        #endif
        
        if (currentGuess.Contains(gameObject))
        {
            currentGuess.Remove(gameObject);
            return;
        }
        currentGuess.Add(gameObject);
    }

    private bool CheckGuess()
    {
        for (int i = 0; i < 5; ++i)
        {
            if (currentGuess[i] != solution[i])
            {
                return false;
            }
        }

        return true;
    }

    private void EndPuzzle()
    {
        //unlock holder
        #if UNITY_EDITOR
            Debug.Log("You solved the Teddy Puzzle!");
        #endif
    }

    private void ResetPuzzle()
    {
        currentGuess.Clear();
    }
}
