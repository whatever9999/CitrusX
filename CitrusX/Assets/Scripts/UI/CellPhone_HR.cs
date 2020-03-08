//Hugo
//Opens and closes the cellphone on the UI using keycodes
//User can use the cellphone to display the main lobby camera

/**
* \class CellPhone_HR
* 
* \brief The player can use C to open and close their cellphone, which has a material that shows the output of a camera
* 
* In update the class checks to see if the player pushes the button. If they do the the GO is toggled on or off
* 
* \author Hugo
* 
* \date Last Modified: 16/02/2020
*/
using UnityEngine;

public class CellPhone_HR : MonoBehaviour
{

    public KeyCode cellphoneOpenKey = KeyCode.C;
    public KeyCode[] cellphoneCloseKeys = { KeyCode.C, KeyCode.Escape };

    private GameObject cellPhone;
    /// <summary>
    /// Inititalise the cellPhone variable and turn the GO off
    /// </summary>
    void Awake()
    {
        cellPhone = GameObject.Find("Cellphone");
        cellPhone.SetActive(false);
    }

    
     /// <summary>
     /// opening and closing the cellPhone
     /// </summary>
    void Update()
    {
        if (!cellPhone.activeInHierarchy && Input.GetKeyDown(cellphoneOpenKey) || Input.GetButtonDown("Phone"))
        {
            cellPhone.SetActive(true);
        }
        else if (cellPhone.activeInHierarchy)
        {
            for (int i = 0; i < cellphoneCloseKeys.Length; i++)
            {
                if (Input.GetKeyDown(cellphoneCloseKeys[i]) || Input.GetButtonDown("Phone"))
                {
                    cellPhone.SetActive(false);
                }
            }
        }
    }
}
