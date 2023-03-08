using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public String sceneToLoad;
    public Animator animator;
    public GameObject positionToReturnTo = null;
    private bool fading = false;
    private float animStartTime = 0f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (SceneManager.GetActiveScene().name == "Main Room" && positionToReturnTo)
        {
            GameManager.Instance.mainRoomPos = positionToReturnTo.transform.position;
        }
        
        if (col.CompareTag("Player"))
        {
            FadeToLevel();
        }
    }

    void Update()
    {
        if (fading)
        {
            if (Time.time - animStartTime >= 1f)
            {
                OnFadeComplete();
            }
        }
    }
    
    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
        animStartTime = Time.time;
        fading = true;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
