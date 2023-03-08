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
    private AudioSource gateOpening;
    public GameObject gateA;
    public GameObject gateB;
    public Sprite gateOpenSprite;
    public GameObject gem;

    delegate void EndPuzzle();
    private EndPuzzle endPuzzle;

    delegate void ResumeWorldStatus();
    private ResumeWorldStatus resumeWorldStatus;
    
    void Start()
    {
        if (GameManager.Instance.puzzleThreeSolved)
        {
            OpenGate();
            if (GameManager.Instance.blueCollected)
            {
                Destroy(gem);
            }
        }
        
        checkForResume = false;
        count = 0;
        Transform[] children = parentOfTiles.GetComponentsInChildren<Transform>();
        tiles = new List<GameObject>();

        foreach (Transform c in children)
        {
            tiles.Add(c.gameObject);
        }
        tiles.RemoveAt(0);

        if (GameManager.Instance.puzzleThreeSolved)
        {
            foreach (GameObject t in tiles)
            {
                Tile tile = t.gameObject.GetComponent<Tile>();
                tile.SetSpritePushed();
                tile.DisableTrigger();
            }
        }

        gateOpening = GetComponent<AudioSource>();

        endPuzzle += DisablePuzzle;
        endPuzzle += DisableOtherSounds;
        endPuzzle += PlayPuzzleEndSound;
        endPuzzle += DisablePlayerController;
        endPuzzle += OpenGate;

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
            GameManager.Instance.puzzleThreeSolved = true;
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

        checkForResume = true;
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

    void OpenGate()
    {
        gateA.gameObject.GetComponent<SpriteRenderer>().sprite = gateOpenSprite;
        gateB.gameObject.GetComponent<SpriteRenderer>().sprite = gateOpenSprite;

        gateA.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gateB.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        if (checkForResume && !gateOpening.isPlaying)
        {
            checkForResume = false;
            resumeWorldStatus?.Invoke();
        }
    }
    
}
