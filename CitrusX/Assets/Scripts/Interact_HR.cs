using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact_HR : MonoBehaviour
{
    public int RayRange;
    RaycastHit Hit;
    Text UI;
    private void Awake()
    {
        UI = GameObject.Find("UIText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position,transform.forward,out Hit,RayRange))
        {
            if(Hit.transform.tag == "Object")
            {
                UI.text = "Press E to pick up";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 TouchPos = new Vector2(Pos.x, Pos.y);

                    if (TouchPos.x == Pos.x && TouchPos.y == Pos.y)
                    {
                        Destroy(Hit.transform.gameObject);
                        UI.text = "";
                    }
                }
            }
        }
    }
}
