using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Fusebox_CW : MonoBehaviour
{
  

    private FirstPersonController fpsController;


    private bool isFuseboxSolved;
    internal bool GetState() { return isFuseboxSolved; }

    void Awake()
    {
        //fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
       
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //OpenFusebox();
        // gameObject.SetActive(false);
    }


  
    //reused and tweaked some of Dominique's code to open/close the fusebox to lock the cursor etc as will only have one fusebox in game
    public void OpenFusebox()
    {
        //Make the cursor useable for solving the puzzle
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);

        //Stop the player from moving while using the fusebox
        fpsController.enabled = false;
    }
    public void CloseFusebox()
    {
        //Make sure raycasts know the fusebox is a fusebox
        gameObject.tag = "Fusebox";

        //Make the cursor invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);

        //Let the player move again
        fpsController.enabled = true;
    }
}
