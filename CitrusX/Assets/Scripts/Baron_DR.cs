/*
 * Dominique
 * 
 * When the baron is active he moves towards the water bowl
 * If he collides with the bowl then he takes a coin
 * If he is disabled then he is moved back to his start position
 */
using UnityEngine;

public class Baron_DR : MonoBehaviour
{
    public float speed;

    private Vector3 startPosition;
    private Transform waterBowl;
    private Rigidbody rigidbody;

    private void Start()
    {
        startPosition = transform.position;
        waterBowl = GameObject.FindObjectOfType<WaterBowl_DR>().transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Move towards water bowl
        transform.LookAt(waterBowl);
        rigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
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
