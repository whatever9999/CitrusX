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
    private int GetBoxNumber()
    {
        if (isFirstBox && !isPasswordBox) { return 0; }
        else if (isFirstBox && isPasswordBox) { return 4; }
        else if (isSecondBox && !isPasswordBox) { return 1; }
        else if (isSecondBox && isPasswordBox) { return 5; }
        else if (isThirdBox && !isPasswordBox) { return 2; }
        else if (isThirdBox && isPasswordBox) { return 6; }
        else if (isFourthBox && !isPasswordBox) { return 3; }
        else if (isFourthBox && isPasswordBox) { return 7; }
        else { return 0; }
    }
    private void Awake()
    {
        correctOrderPuzzle = GameObject.Find("CorrectOrderUI").GetComponent<CorrectOrder_CW>();
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
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.green);
        }
       else if (thisImage.color == Color.green)
       {
            thisImage.color = Color.yellow;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.yellow);
        }
       else if (thisImage.color == Color.yellow)
       {
            thisImage.color = Color.cyan;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.cyan);
        }
       else if (thisImage.color == Color.cyan)
       {
            thisImage.color = Color.red;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(),Color.red);
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
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.red);
        }
        else if(isSecondBox && !isPasswordBox)
        {
            thisImage.color = Color.yellow;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.yellow);
        }
        else if(isThirdBox && !isPasswordBox)
        {
            thisImage.color = Color.green;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.green);
        }
        else if(isFourthBox && !isPasswordBox)
        {
            thisImage.color = Color.cyan;
            correctOrderPuzzle.AssignBoxColour(GetBoxNumber(), Color.cyan);
        }
    }
  
  
}
