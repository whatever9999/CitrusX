//Hugo
//Opens and closes the cellphone on the UI using keycodes
//User can use the cellphone to display the main lobby camera
using UnityEngine;

public class CellPhone_HR : MonoBehaviour
{

    public KeyCode cellphoneOpenKey = KeyCode.C;
    public KeyCode[] cellphoneCloseKeys = { KeyCode.C, KeyCode.Escape };

    private GameObject cellPhone;
    void Awake()
    {
        cellPhone = GameObject.Find("Cellphone");
        cellPhone.SetActive(false);
    }

    
     // Opening and closing the journal

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
