using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class PuzzleThree : MonoBehaviour
{
    private bool checkForResume;
    private int count;
    private List<GameObject> tiles;
    private PlayerController playerController;
    public GameObject player;
    public GameObject parentOfTiles;
    public AudioSource gateOpening;

    delegate void EndPuzzle();
    private EndPuzzle endPuzzle;

    delegate void ResumeWorldStatus();
    private ResumeWorldStatus resumeWorldStatus;
    
    void Start()
    {
        checkForResume = false;
        count = 0;
        Transform[] children = parentOfTiles.GetComponentsInChildren<Transform>();
        tiles = new List<GameObject>();

        foreach (Transform c in children)
        {
            tiles.Add(c.gameObject);
        }
        tiles.RemoveAt(0);

        gateOpening = GetComponent<AudioSource>();

        endPuzzle += DisablePuzzle;
        endPuzzle += DisableOtherSounds;
        endPuzzle += PlayPuzzleEndSound;
        endPuzzle += DisablePlayerController;

        resumeWorldStatus += EnableAudio;
        resumeWorldStatus += EnablePlayerController;
        
        gateOpening.ignoreListenerPause = true;

        playerController = player.GetComponent<PlayerController>();
    }

    public void StateChange(bool newState)
    {
        if (newState)
        {
            Increase();
        }
        else
        {
            Decrease();
        }

        Check();
    }
    
    void Increase()
    {
        ++count;
    }

    void Decrease()
    {
        --count;
    }

    void Check()
    {
        if (count == 9)
        {
            endPuzzle?.Invoke();
        }
    }

    void DisablePuzzle()
    {
        foreach (GameObject tile in tiles)
        {
            Collider2D collider = tile.GetComponent<Collider2D>();
            collider.enabled = false;
        }
    }

    void EnableAudio()
    {
        AudioListener.pause = false;
    }
    
    void DisableOtherSounds()
    {
        AudioListener.pause = true;
    }

    void PlayPuzzleEndSound()
    {
        gateOpening.Play();
    }

    void EnablePlayerController()
    {
        playerController.enabled = true;
    }
    
    void DisablePlayerController()
    {
        playerController.enabled = false;
    }

    void Update()
    {
        if (checkForResume && !gateOpening.isPlaying)
        {
            resumeWorldStatus?.Invoke();
        }
    }
    
}
