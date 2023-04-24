using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MainRoomController : MonoBehaviour
{
    private AudioSource gateOpening;
    public GameObject gateA;
    public GameObject gateB;
    public Sprite gateOpenSprite;

    private void Start()
    {
        gateOpening = GetComponent<AudioSource>();
        
        if (GameManager.Instance.gemsSet)
        {
            OpenGate();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.gemsSet)
        {
            OpenGate();
            gateOpening.Play();
        }
    }
    
    private void OpenGate()
    {
        gateA.gameObject.GetComponent<SpriteRenderer>().sprite = gateOpenSprite;
        gateB.gameObject.GetComponent<SpriteRenderer>().sprite = gateOpenSprite;

        gateA.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gateB.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
