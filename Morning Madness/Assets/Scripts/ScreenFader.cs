using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

    //fade variables
    public float fadeSpeed = 1.5f;
    public Image fadeScreen;
    private bool sceneStart = true;

	// Use this for initialization
	void Start () {
        fadeScreen.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
	    if (sceneStart)
        {
            SceneStart();
        }
	}

    //fade in
    private void FadeClear()
    {
        //WAAAAAY TOO SLOW! For dumb dumb lab pc
        //fadeScreen.color = Color.Lerp(fadeScreen.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    //fade out
    private void FadeBlack()
    {
        //WAAAAAY TOO SLOW! For dumb dumb lab pc
        //fadeScreen.color = Color.Lerp(fadeScreen.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    private void SceneStart()
    {
        FadeClear();

        if (fadeScreen.color.a <= 0.05f)
        {
            fadeScreen.color = Color.clear;
            fadeScreen.enabled = false;
            sceneStart = false;
        }
    }

    public void SceneEnd()
    {
        fadeScreen.enabled = true;

        FadeBlack();

        if (fadeScreen.color.a >= 95.5f)
        {
            fadeScreen.color = Color.black;
        }
    }
}
