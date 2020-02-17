using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordButton_CW : MonoBehaviour
{
    private CorrectOrder_CW correctOrderPuzzle;
    private Image thisImage;

    private void Start()
    {
        correctOrderPuzzle = CorrectOrder_CW.instance;
        thisImage = GetComponent<Image>();
    }
    public void ChangeColour()
    {
       if(thisImage.color == Color.red)
       {
            thisImage.color = Color.green;
       }
       else if (thisImage.color == Color.green)
       {
            thisImage.color = Color.yellow;
       }
       else if (thisImage.color == Color.yellow)
       {
            thisImage.color = Color.cyan;
       }
       else if (thisImage.color == Color.cyan)
       {
            thisImage.color = Color.red;
       }
       else
       {
            thisImage.color = Color.red;
       }
    }
}
