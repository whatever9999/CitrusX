/*
 * Hugo
 * 
 * Script to make sure the button only works with the correct ball by comparing masses 
 */
using UnityEngine;

public class BallButtonLogic_HR : MonoBehaviour
{
    public int massRequired;

    private void OnCollisionEnter(Collision collision)
    {
        //check if the mass of the ball is the required to push the button
        if (collision.gameObject.GetComponent<Rigidbody>().mass == massRequired)
        {
            Destroy(collision.gameObject);
        }
    }
}
