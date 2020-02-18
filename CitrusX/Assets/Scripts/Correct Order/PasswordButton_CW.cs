using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordButton_CW : MonoBehaviour
{
    private CorrectOrder_CW correctOrderPuzzle;
    private Image thisImage;
    public bool isFirstBox = false;
    public bool isSecondBox = false;
    public bool isThirdBox = false;
    public bool isFourthBox = false;
    public bool isPasswordBox = false;
    private float flashTimer = 0;
    private float flashNow;

    private void Awake()
    {
        correctOrderPuzzle = CorrectOrder_CW.instance;
        thisImage = GetComponent<Image>();
        if(isFirstBox)
        {
            flashNow = 2f;
        }
        else if(isSecondBox)
        {
            flashNow = 4f;
        }
        else if(isThirdBox)
        {
            flashNow = 8f;
        }
        else if(isFourthBox)
        {
            flashNow = 10f;
        }
    }
    public void ChangeColour()
    {
       if(thisImage.color == Color.red)
       {
            thisImage.color = Color.green;
            AssignBoxColour(Color.green);
       }
       else if (thisImage.color == Color.green)
       {
            thisImage.color = Color.yellow;
            AssignBoxColour(Color.yellow);
        }
       else if (thisImage.color == Color.yellow)
       {
            thisImage.color = Color.cyan;
            AssignBoxColour(Color.cyan);
        }
       else if (thisImage.color == Color.cyan)
       {
            thisImage.color = Color.red;
            AssignBoxColour(Color.red);
        }
       else
       {
            thisImage.color = Color.red;
       }
    }
    private void Update()
    {
        Debug.Log(flashTimer);
        if(flashTimer >= flashNow)
        {
            Flash();
            flashTimer = 0;
        }
        else
        {
            flashTimer+=Time.deltaTime;
        }

        
    }
    private void Flash()
    {
        if(isFirstBox && !isPasswordBox)
        {
            thisImage.color = Color.red;
            correctOrderPuzzle.boxes[0] = Color.red;
        }
        else if(isSecondBox && !isPasswordBox)
        {
            thisImage.color = Color.yellow;
            correctOrderPuzzle.boxes[1] = Color.yellow;
        }
        else if(isThirdBox && !isPasswordBox)
        {
            thisImage.color = Color.green;
            correctOrderPuzzle.boxes[2] = Color.green;
        }
        else if(isFourthBox && !isPasswordBox)
        {
            thisImage.color = Color.cyan;
            correctOrderPuzzle.boxes[3] = Color.cyan;
        }
    }
    public void CheckForCompletion()
    {
        if(correctOrderPuzzle.boxes[0] == correctOrderPuzzle.boxes[4])
        {
            if(correctOrderPuzzle.boxes[1] == correctOrderPuzzle.boxes[5])
            {
                if(correctOrderPuzzle.boxes[2] == correctOrderPuzzle.boxes[6])
                {
                    if(correctOrderPuzzle.boxes[3] == correctOrderPuzzle.boxes[7])
                    {
                        GameObject.Find("Correct Order Message Text").GetComponent<Text>().text = "COMPLETE"; 
                    }
                }
            }
        }
    }
    private void AssignBoxColour(Color colour)
    {
        if(isFirstBox && isPasswordBox)
        {
            correctOrderPuzzle.boxes[4] = colour;
        }
        else if(isSecondBox && isPasswordBox)
        {
            correctOrderPuzzle.boxes[5] = colour;
        }
        else if(isThirdBox && isPasswordBox)
        {
            correctOrderPuzzle.boxes[6] = colour;
        }
        else if(isFourthBox && isPasswordBox)
        {
            correctOrderPuzzle.boxes[7] = colour;
        }
    }
}
