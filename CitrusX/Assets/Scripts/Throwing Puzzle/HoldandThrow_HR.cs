using System.Collections;
using System.Collections.Generic;
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
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if (distance >= 1f)
        {
            isHolding = false;
        }
        //Check if isholding
        if (isHolding == true)
        {
            itemRB.velocity = Vector3.zero;
            itemRB.angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);

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
        if (distance <= 1f)
        {
            isHolding = true;
            itemRB.useGravity = false;
            itemRB.detectCollisions = true;
        }
    }
    void OnMouseUp()
    {
        isHolding = false;
    }
}
