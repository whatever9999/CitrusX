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
 * 
 * Dominique (Changes) 17/03/2020
 * The baron now handles itself and is told when to go for the water/appear in a room (as opposed to being on a timer from the water bowl)
 */

/**
* \class Baron_DR
* 
* \brief When the baron is active he tries to move towards the water bowl. If he collides with it then he reaches for a coin and picks it up
* 
* Where baron is a reference to the Baron_DR script use:
* baron.GetCoin(); to make the baron go for the bowl
* baron.AppearStill(baron.transform, 5); to make the baron appear for a certain amount of time
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

    internal float appearanceTimer;
    private float currentAppearanceTimer;
    private bool gettingCoin = true;
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
        waterBowl = FindObjectOfType<WaterBowl_DR>().transform;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Disable the object after it has been accessed for references
    /// </summary>
    private void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Make the baron look at the water bowl and move forwards
    /// </summary>
    private void FixedUpdate()
    {
        if(gettingCoin)
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
    }

    /// <summary>
    /// A timer runs to make the baron disappear after he has appeared in a room
    /// </summary>
    private void Update()
    {
        if(!gettingCoin)
        {
            appearanceTimer += Time.deltaTime;
            if (appearanceTimer > currentAppearanceTimer)
            {
                appearanceTimer = 0;
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Baron appears at a specified position and rotation for a number of seconds then disappears
    /// </summary>
    /// <param name="location - where the baron will appear"></param>
    /// <param name="lengthOfAppearance - how long until he disappears"></param>
    public void AppearStill(Transform location, float lengthOfAppearance)
    {
        transform.position = location.position;
        transform.rotation = location.rotation;

        currentAppearanceTimer = lengthOfAppearance;

        gettingCoin = false;

        gameObject.SetActive(true);

        animator.SetBool("NotMoving", true);
    }

    /// <summary>
    /// The baron appears in his start position (according to the scene) and goes for the water bowl
    /// </summary>
    public void GetCoin()
    {
        transform.position = startPosition;
        gettingCoin = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Reset the position of the baron and his speed each time he is disabled
    /// </summary>
    private void OnDisable()
    {
        //Reset position and speed
        rigidbody.velocity = Vector3.zero;
        animator.SetBool("NotMoving", false);
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
        Debug.Log("The baron has taken a coin");
        gameObject.SetActive(false);
    }
}
