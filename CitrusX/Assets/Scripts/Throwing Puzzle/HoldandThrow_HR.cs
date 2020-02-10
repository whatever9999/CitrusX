/*
 * Hugo
 * 
 * The player can hold an item in front of them for any period of time.
 * They can also throw the item in the air.
 * If the player gets to far from the object they let go of it
 * Objects still colide with the enviorment
 */

using UnityEngine;

public class HoldandThrow_HR : MonoBehaviour
{
    private float throwForce = 600;
    private Vector3 objectPos;
    private float distance;
    private Rigidbody itemRB;

    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding = false;

    void Awake()
    {
        itemRB = item.GetComponent<Rigidbody>();
    }
    void Update()
    {
        //If the player gets to far from the object he is holding drop it
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if (distance >= 1f)
        {
            isHolding = false;
        }
        //Check if is holding
        if (isHolding == true)
        {
            itemRB.velocity = Vector3.zero;
            itemRB.angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);

            //if right click them throw object
            if (Input.GetMouseButtonDown(1))
            {
                itemRB.AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }
        }
        else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            itemRB.useGravity = true;
            item.transform.position = objectPos;
        }
    }

    void OnMouseDown()
    {
        //if the player is near the object
        if (distance <= 1f)
        {
            isHolding = true;
            itemRB.useGravity = false;
            itemRB.detectCollisions = true;
        }
    }
    void OnMouseUp()
    {
        //release the object
        isHolding = false;
    }
}
