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
 */

using UnityEngine;

public class HoldandThrow_HR : MonoBehaviour
{
    public float minDistanceToPickup = 1;
    public bool canHold = true;
    public float throwForce = 600;

    private Rigidbody RB;
    private Transform holdPosition;

    private bool beingHeld = false;
    private bool isFirstTime = false;
    private Subtiles_HR subtitles;

    void Awake()
    {
        RB = GetComponent<Rigidbody>();
        holdPosition = GameObject.Find("HoldGuide").transform;
        subtitles = GameObject.Find("FirstPersonCharacter").GetComponent<Subtiles_HR>();
    }
    void Update()
    {
        if (beingHeld)
        {
            //If the player gets too far from the object he is holding drop it (In case it gets stuck)
            float distance = Vector3.Distance(transform.position, holdPosition.position);
            if (distance >= minDistanceToPickup)
            {
                Drop();
            }
            //if right click then throw object
            else if (Input.GetMouseButtonDown(1))
            {
                RB.AddForce(holdPosition.forward * throwForce);
                Drop();
            }
        }
    }

    public void Hold()
    {
        beingHeld = true;

        transform.SetParent(holdPosition);

        transform.localPosition = Vector3.zero;

        RB.useGravity = false;

        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
    }

    public void Drop()
    {
        beingHeld = false;

        transform.SetParent(null);

        RB.useGravity = true;
    }

    void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, holdPosition.position);
        //if the player is near the object
        if (distance <= minDistanceToPickup)
        {
            if(!isFirstTime)
            {
                subtitles.PlayAudio(Subtiles_HR.ID.P7_LINE3);
                isFirstTime = true;
            }
            Hold();
        }
    }
    void OnMouseUp()
    {
        //release the object
        Drop();
    }
}
