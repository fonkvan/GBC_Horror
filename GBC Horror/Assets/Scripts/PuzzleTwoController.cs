using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTwoController : MonoBehaviour
{
    public GameObject gem;
    public GameObject jumpscareMonster;
    private static bool notDoneSolvedThing = false;

    // Update is called once per frame
    void Update()
    {
        if (!notDoneSolvedThing)
        {
            if (GameManager.Instance.puzzleTwoSolved)
            {
                notDoneSolvedThing = true;
                jumpscareMonster.SetActive(true);
                gem.SetActive(true);
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
