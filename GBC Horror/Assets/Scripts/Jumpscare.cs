using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    public float moveSpeed;
    private Animator animator;
    private SpriteRenderer spriteRend;
    private AudioSource sounds;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        sounds = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void StartJumpscare()
    {
        print("received message");
        animator.SetTrigger("jumpscare");
        Vector3 targetPosition = transform.position + new Vector3(4, 0, 0);
        print(targetPosition);
        StartCoroutine(MoveOverTime(targetPosition));
    }

    IEnumerator MoveOverTime(Vector3 targetPosition)
    {
        print("coroutine started");
        sounds.Play();
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            print(transform.position);

            yield return null;
        }

        Destroy(this.gameObject);
    }
}
