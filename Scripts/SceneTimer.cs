using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.Events;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;
using System.Collections.ObjectModel;

public class SceneTimer : MonoBehaviour {

    //TIMER AND SCORE
    private float timer;
    private float score;

    public Text timerText;
    public Text scoreText;

	void Start ()
    {
        timer = 60f;
        score = 0f;
        timerText.text = "";
        scoreText.text = "";

        SetScoreText();
    }
	

	void Update ()
    {
        CountDown();
        //MenuButton();
    }


    void CountDown()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time Left: " + timer.ToString();

        if (timer < 0f)
        {
            SceneManager.LoadScene("Menu");
        }
    }


    public void AddScore()
    {
        score = score + 1f;
        SetScoreText();
    }


    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    //EXIT MENU BUTTON
    
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void MenuButton()
    {
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
