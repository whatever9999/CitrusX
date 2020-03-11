/*
 * Dominique
 * 
 * When the baron is active he moves towards the water bowl
 * If he collides with the bowl then he takes a coin
 * If he is disabled then he is moved back to his start position
 * 
 * Dominique (Changes) 20/02/2020
 * Changed the way the baron moves (change velocity instead of adding force)
 * Added animation to the baron
 */

/**
* \class Baron_DR
* 
* \brief When the baron is active he tries to move towards the water bowl. If he collides with it then he reaches for a coin and picks it up
* 
* \author Dominique
* 
* \date Last Modified: 20/02/2020
*/
using System.Collections;
using UnityEngine;

public class Baron_DR : MonoBehaviour
{
    public float speed;

    private Vector3 startPosition;
    private Transform waterBowl;
    private Rigidbody rigidbody;
    private Animator animator;

    /// <summary>
    /// Initialise variables
    /// </summary>
    private void Awake()
    {
        startPosition = transform.position;
        waterBowl = GameObject.FindObjectOfType<WaterBowl_DR>().transform;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Make the baron look at the water bowl and move forwards
    /// </summary>
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

    /// <summary>
    /// Reset the position of the baron and his speed each time he is disabled
    /// </summary>
    private void OnDisable()
    {
        //Reset position and speed
        transform.position = startPosition;
        rigidbody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Start the PickUpCoin() coroutine if the player collides with the water bowl
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WaterBowl")
        {
            WaterBowl_DR waterBowl = collision.gameObject.GetComponent<WaterBowl_DR>();
            StartCoroutine(PickUpCoin(waterBowl));
        }
    }

    /// <summary>
    /// Play the animation of the baron picking up the coin, remove it from the bowl and reset him
    /// </summary>
    /// <param name="waterBowl - the WaterBowl_DR that the baron collides with"></param>
    public IEnumerator PickUpCoin(WaterBowl_DR waterBowl)
    {
        //Play the reach animation and pick up a coin then disappear
        animator.SetBool("ReachedBowl", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        waterBowl.RemoveCoin();
        waterBowl.ResetBaron();
        Debug.Log("The baron has taken a coin");
    }
}
