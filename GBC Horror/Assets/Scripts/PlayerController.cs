using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private bool inRangeOfItem;
    private bool inRangeOfStatue;

    private Vector2 moveInput;

    private GameObject nearbyItem;
    private Teddy teddyToInteractWith;
    private Chest chest;

    public LayerMask solidObjectsLayer;
    public static List<GameObject> collectedGems = new List<GameObject>();

    private Animator animator;

    private void Awake()
    {
        // Get reference to animator controller
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Function that controls collecting gems, removing them from scene, adding to inventory
        CollectGem();

        // Function for removing gems from inventory at statue when all 3 are present. Starts final attack
        PlaceGems();

        InteractWithTeddy();

        InteractWithChest();
        
        // As long as the player is not moving
        if(!isMoving)
        {
            // Find the inputs
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            if (moveInput.x != 0)
            {
                moveInput.y = 0;
            }

            // If there is a nonzero input
            if (moveInput != Vector2.zero)
            {
                // Set animator floats for direction
                animator.SetFloat("moveX", moveInput.x);
                animator.SetFloat("moveY", moveInput.y);

                // Assign target position and call coroutine to move
                Vector3 targetPosition = transform.position;
                targetPosition.x += moveInput.x;
                targetPosition.y += moveInput.y;

                if (IsWalkable(targetPosition))
                {
                    StartCoroutine(MoveOverTime(targetPosition));
                }
            }
        }

        // Set animator bool for moving animations
        animator.SetBool("isMoving", isMoving);
    }

    // Called in Update every frame
    void CollectGem()
    {
        // If in range of an item
        if (inRangeOfItem)
        {
            // If the E key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Pickup the "nearby item" and add to inventory
                collectedGems.Add(nearbyItem);
                #if UNITY_EDITOR
                    print("Attempted Pickup");
                    print(collectedGems);
                    print(collectedGems.Count);
                #endif

                GameManager.Instance.GemCollected(nearbyItem);
                
                // Set bools to false to prevent repeat pickups and remove gameobject
                nearbyItem.SetActive(false);
                inRangeOfItem = false;
            }
        }
    }

    // Called in Update every frame
    void PlaceGems()
    {
        // If the player is in range of the statue
        if (inRangeOfStatue)
        {
            // If there are 3 collected gems in inventory
            if (collectedGems.Count == 3)
            {
                // If the player presses E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Remove all gems from the inventory and begin final attack
                    collectedGems.Clear();
                    #if UNITY_EDITOR
                        print(collectedGems.Count + "    The Gate Opens");
                    #endif    

                    // THIS IS WHERE A CALL FOR STARTING FINAL ATTACK WOULD GO !*!*!*!*!*!*!
                }
            }
        }
    }


    // Moves the player towards the targeted tile over time
    IEnumerator MoveOverTime(Vector3 targetPos)
    {
        // Set the player to isMoving
        isMoving = true;

        Vector3 startingPos = transform.position;

        float t = Time.time;
        
        // While the player is not at targetPos (or very very close)
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // Move towards targetPos over time
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            //catch: tried moving to 1 tile for 1 seconds, can't do this
            if (Time.time - t >= 1f)
            {
                if ((targetPos - startingPos).sqrMagnitude > (transform.position - startingPos).sqrMagnitude)
                {
                    //we can't move, break coroutine.
                    transform.position = startingPos;
                    isMoving = false;
                    yield break;
                }
            }
            
            // When this position gets very close, break the while loop
            yield return null;
        }

        // For the purposes of perfectly aligning with the tile, set the position absolutely to targetPos
        transform.position = targetPos;

        // The player is no longer moving 
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    { 
        float offset = 0.1f;
        Vector3 curPos = transform.position;
        Vector2 startA;
        Vector2 startB;
        Vector2 endA;
        Vector2 endB;
 
        //lines "bound" the player using offset
        if (Mathf.Approximately(targetPos.x, curPos.x))
        {
            //moving vertically
            startA = new Vector2(curPos.x + offset, curPos.y); 
            startB = new Vector2(curPos.x - offset, curPos.y);
            endA = new Vector2(targetPos.x + offset, targetPos.y);
            endB = new Vector2(targetPos.x - offset, targetPos.y);
        }
        else
        { 
            //moving horizontally
            startA = new Vector2(curPos.x, curPos.y + offset);
            startB = new Vector2(curPos.x, curPos.y - offset);
            endA = new Vector2(targetPos.x, targetPos.y + offset);
            endB = new Vector2(targetPos.x, targetPos.y - offset);
        }
 
        Collider2D colCastA = Physics2D.Linecast(startA, endA, solidObjectsLayer).collider;
        Collider2D colCastB = Physics2D.Linecast(startB, endB, solidObjectsLayer).collider;
 
        if (colCastA && colCastB)
        {
            return false;
        }
        return true;
        /*
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }*/
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        #if UNITY_EDITOR
            print("In range of something?");
        #endif

        // When the trigger volume is a gem
        if (collision.gameObject.CompareTag("GemPickup"))
        {

            #if UNITY_EDITOR
                print("In Range of Gem");
            #endif

            // Retain info about the game object, set bool to true for CollectGems function
            inRangeOfItem = true;
            nearbyItem = collision.gameObject;
        }

        // If the trigger volume is the statue
        if (collision.gameObject.CompareTag("Statue"))
        {
            // Set statue bool to true for PlaceGems function
            inRangeOfStatue = true;
            
            #if UNITY_EDITOR
                print(inRangeOfStatue);
            #endif
        }

        if (collision.gameObject.CompareTag("Teddy"))
        {
            teddyToInteractWith = collision.gameObject.GetComponent<Teddy>();
        }

        if (collision.gameObject.CompareTag("Chest"))
        {
            chest = collision.gameObject.GetComponentInParent<Chest>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // When the trigger volume is a gem
        if (collision.gameObject.CompareTag("GemPickup"))
        {

            #if UNITY_EDITOR
                print("No longer in Range of Gem");
            #endif

            // Retain info about the game object, set bool to false - gem is out of range now
            inRangeOfItem = false;
            nearbyItem = null;
        }

        // If the trigger volume was the statue
        if (collision.gameObject.CompareTag("Statue"))
        {
            inRangeOfStatue = false;
            
            #if UNITY_EDITOR
                print(inRangeOfStatue);
            #endif
        }

        if (collision.gameObject.CompareTag("Teddy"))
        {
            teddyToInteractWith = null;
        }

        if (collision.gameObject.CompareTag("Chest"))
        {
            chest = null;
        }
    }

    void InteractWithTeddy()
    {
        if (Input.GetKeyDown(KeyCode.E) && teddyToInteractWith)
        {
            teddyToInteractWith.Interact();
        }
    }

    void InteractWithChest()
    {
        if (Input.GetKeyDown(KeyCode.E) && chest)
        {
            chest.Interact();
        }
    }
}


