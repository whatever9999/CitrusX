/*
 * Hugo
 * 
 * The player can hold an item in front of them for any period of time.
 * They can also throw the item in the air.
 * If the player gets to far from the object they let go of it
 * Objects still colide with the enviorment
 * 
 * Chase (Changes)
 * Added a bool to register first pick up for subtitles, also added subtitle functionality
 * 
 * Dominique (Changes) 03/03/2020
 * Optimising and fixes
 * 
 * Dominique (Changes) 09/03/2020
 * Every 2 seconds that the ball is moving it will have its velocity set to zero so it doesn't roll everywhere
 * 
 * Dominique (Changes) 25/03/2020
 * Ensured that objects stay centrered when held
 * 
 * Dominique (Changes) 01/04/2020
 * Added raycasting for polish - if items collide while held they don't blend into other colliders
 */

/**
* \class HoldandThrow_HR
* 
* \brief Lets the player hold, drop and throw the object using the mouse
* 
* Update checks if the object is being held. If so it drops the object if the player gets too far from it (in the case of it being stuck) or if the player lets go of it
* Hold() sets the parent of the object to an empty HoldGuide object on the player and sets its local position to 0 and ensures that it is not affected by gravity and its velocity is 0.
* Drop stops the object from having a parent and lets gravity affect it again
* When the player uses the mouse on the object's collider they can pick it up/drop it
* 
* \author Hugo
* 
* \date Last Modified: 01/04/2020
*/

using System.Collections.Generic;
using UnityEngine;

public class HoldandThrow_HR : MonoBehaviour
{
    public float minDistanceToPickup = 5;
    public bool canHold = true;
    public float throwForce = 600;

    private Rigidbody RB;
    private Transform player;
    private Transform holdGuide;

    private bool beingHeld = false;
    internal bool isFirstTime = false;
    private Subtitles_HR subtitles;
    private IdleVoiceover_CW idleVos;
    private const float timeToMoveBeforeStop = 2;
    private float currentTimeMoving;

    private const int throwableMask = ~(1 << 9);
    private float rayRange;
    private const float addToCollidingObjectsYPositions = 0.1f;

    /// <summary>
    /// Initialise variables
    /// </summary>
    void Awake()
    {
        RB = GetComponent<Rigidbody>();
        holdGuide = GameObject.Find("HoldGuide").transform;
        player = GameObject.Find("FirstPersonCharacter").transform;
        subtitles = player.GetComponent<Subtitles_HR>();
        idleVos = GameObject.Find("Managers").GetComponent<IdleVoiceover_CW>();

        rayRange = holdGuide.localPosition.z;
    }
    /// <summary>
    /// Drop the object if the player is too far from it or throws it (also add force to it in the forward direction if this is the case)
    /// </summary>
    void Update()
    {
        if (beingHeld)
        {
            //Make sure the object stays in the centre of the players vision
            CheckItemPosition();

            //If the player gets too far from the object he is holding drop it (In case it gets stuck)
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance >= minDistanceToPickup)
            {
                Drop();
            }
            //if right click then throw object
            else if (Input.GetMouseButtonDown(1))
            {
                RB.AddForce(player.forward * throwForce);
                Drop();
            }
        } else
        {
            //If the ball is moving for more than 3 seconds make its velocity 0
            if(RB.velocity != Vector3.zero && currentTimeMoving > timeToMoveBeforeStop)
            {
                RB.velocity = Vector3.zero;
                currentTimeMoving = 0;
            } 
            //If the ball is moving increase the currentTimeMoving timer
            else if (RB.velocity != Vector3.zero)
            {
                currentTimeMoving += Time.deltaTime;
            } 
            //If the ball is moving make sure the currentTimeMoving timer is at 0
            else
            {
                currentTimeMoving = 0;
            }
        }
    }

    /// <summary>
    /// Set the parent of the object to a HoldGuide on the player and set its local position to 0, ensuring it's not affected by gravity
    /// </summary>
    public void Hold()
    {
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PICK_UP_OBJECT, transform.position);
        idleVos.interactedWith = true;
        
        beingHeld = true;

        transform.SetParent(player);

        transform.localPosition = Vector3.zero;

        RB.useGravity = false;
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        RB.freezeRotation = true;
    }

    /// <summary>
    /// Make the object's parent null and let it use gravity
    /// </summary>
    public void Drop()
    {
        SFX_Manager_HR.instance.PlaySFX(SFX_Manager_HR.SoundEffectNames.PUT_DOWN_SCALES, transform.position);
        idleVos.interactedWith = false;
        beingHeld = false;

        transform.SetParent(null);

        RB.useGravity = true;
        RB.freezeRotation = false;
    }

    /// <summary>
    /// Check the distance of the ball to the player. If it is small enough let them pick it up
    /// </summary>
    void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, holdGuide.position);
        //if the player is near the object
        if (distance <= minDistanceToPickup)
        {
            if(!isFirstTime)
            {
                //subtitles.PlayAudio(Subtitles_HR.ID.P7_LINE3);
                isFirstTime = true;
            }
            Hold();
        }
    }

    /// <summary>
    /// Call the Drop() function
    /// </summary>
    void OnMouseUp()
    {
        //release the object
        Drop();
    }

    /// <summary>
    /// If items are colliding they move to the hit point while they'll be in the centre of the player's view otherwise
    /// </summary>
    private void CheckItemPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, player.forward, out hit, rayRange, throwableMask))
        {
            Vector3 newPosition = new Vector3(hit.point.x, hit.point.y + addToCollidingObjectsYPositions, hit.point.z);
            transform.position = newPosition;
        } else
        {
            transform.position = holdGuide.position;
        }
    }
}
