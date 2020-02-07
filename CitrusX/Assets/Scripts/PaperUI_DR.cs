using System.Collections;
using UnityEngine;

public class PaperUI_DR : MonoBehaviour
{
    public KeyCode keyToClose = KeyCode.Escape;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyToClose))
        {
            gameObject.SetActive(false);
        }
    }
}
