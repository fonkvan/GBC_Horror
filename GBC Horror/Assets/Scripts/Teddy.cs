using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy : MonoBehaviour
{
    public List<GameObject> solution;
    private AudioSource squeak;
    private static List<GameObject> currentGuess;

    private void Start()
    {
        currentGuess = new List<GameObject>();
        squeak = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        AddGameObjectToList();
        squeak.Play();
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
        GameManager.Instance.puzzleTwoSolved = true;
    }

    private void ResetPuzzle()
    {
        currentGuess.Clear();
    }
}
