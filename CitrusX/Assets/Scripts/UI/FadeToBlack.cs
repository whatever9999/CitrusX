using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    Image blackScreen;

    private void Awake()
    {
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
    }

    public IEnumerator Fade()
    {
        //Fade to black
        while (blackScreen.color.a < 1f)
        {
            Color newColor = blackScreen.color;
            newColor.a += Time.deltaTime * 2;
            //Emphasise the fade
            yield return new WaitForSeconds(Time.deltaTime);
            blackScreen.color = newColor;
        }
    }

    public IEnumerator FadeFromBlack()
    {
        while (blackScreen.color.a > 0)
        {
            Color newColor = blackScreen.color;
            newColor.a -= Time.deltaTime * 2;
            //Emphasise the fade
            yield return new WaitForSeconds(Time.deltaTime);
            blackScreen.color = newColor;
        }
    }
}
