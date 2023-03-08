using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool puzzleOneSolved = false;
    public bool puzzleTwoSolved = false;
    public bool puzzleThreeSolved = false;
    public bool redCollected = false;
    public bool greenCollected = false;
    public bool blueCollected = false;
    public bool gemsSet = false;
    public Vector3 mainRoomPos = new Vector3(1.5f, -0.5f, 0f);

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GemCollected(GameObject gem)
    {
        if (gem.name == "Red Gem")
        {
            redCollected = true;
        }
        else if (gem.name == "Green Gem")
        {
            greenCollected = true;
        }
        else if (gem.name == "Blue Gem")
        {
            blueCollected = true;
        }
    }
}
