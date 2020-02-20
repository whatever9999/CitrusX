/*
 * Dominique
 * 
 * When the baron is active he moves towards the water bowl
 * If he collides with the bowl then he takes a coin
 * If he is disabled then he is moved back to his start position
 * 
 * Dominique (Changes) 20/02/2020
 * Changed the way the baron moves
 */
using UnityEngine;

public class Baron_DR : MonoBehaviour
{
    public float speed;

    private Vector3 startPosition;
    private Transform waterBowl;
    private Rigidbody rigidbody;

    private void Awake()
    {
        startPosition = transform.position;
        waterBowl = GameObject.FindObjectOfType<WaterBowl_DR>().transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Set target as water bowl
        transform.LookAt(waterBowl);
        //Make sure that the baron doesn't rotate in the wrong axis
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Euler(rotation);
        //Move towards water bowl
        rigidbody.velocity = transform.forward * speed;

    }

    private void OnDisable()
    {
        //Reset position and speed
        transform.position = startPosition;
        rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WaterBowl")
        {
            WaterBowl_DR waterBowl = collision.gameObject.GetComponent<WaterBowl_DR>();
            waterBowl.RemoveCoin();
            Debug.Log("The baron has taken a coin");
            waterBowl.ResetBaron();
        }
    }
}
