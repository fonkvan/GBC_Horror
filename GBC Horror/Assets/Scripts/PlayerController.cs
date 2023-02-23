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

    public List<GameObject> collectedGems = new List<GameObject>();

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
                // Assign target position and call coroutine to move
                Vector3 targetPosition = transform.position;
                targetPosition.x += moveInput.x;
                targetPosition.y += moveInput.y;

                StartCoroutine(MoveOverTime(targetPosition));
            }
        }
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
                print("Attempted Pickup");
                collectedGems.Add(nearbyItem);
                print(collectedGems);
                print(collectedGems.Count);

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
                    print(collectedGems.Count + "    The Gate Opens");

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

        // While the player is not at targetPos (or very very close)
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // Move towards targetPos over time
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            // When this position gets very close, break the while loop
            yield return null;
        }

        // For the purposes of perfectly aligning with the tile, set the position absolutely to targetPos
        transform.position = targetPos;

        // The player is no longer moving 
        isMoving = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("In range of something?");

        // When the trigger volume is a gem
        if (collision.gameObject.CompareTag("GemPickup"))
        {

            print("In Range of Gem");

            // Retain info about the game object, set bool to true for CollectGems function
            inRangeOfItem = true;
            nearbyItem = collision.gameObject;
        }

        // If the trigger volume is the statue
        if (collision.gameObject.CompareTag("Statue"))
        {
            // Set statue bool to true for PlaceGems function
            inRangeOfStatue = true;
            print(inRangeOfStatue);
        }
    }
}


