using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {


	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        UpdateTime();
        manager.resetGame.AddListener(ResetGame);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTime();
       // UpdateMusic();
	}

    void UpdateTime(){
        timer -= Time.deltaTime;
        if (timer >= 0f)
        {
            UpdateMusic();
        }
        minutes = (int)timer / 60;
        minutes = (minutes < 0) ? 0 : minutes;
        seconds = (int)timer % 60;
        seconds = (seconds < 0) ? 0 : seconds;
        string leadingZero = (seconds < 10) ? "0" : ""; 
        text.text = "Time:\n" + minutes + ":" + leadingZero + seconds;
        if (timer <= 0 && !endGame)
        {
            manager.endGame.Invoke();
            endGame = true;
        }
        
    }

    void UpdateMusic()
    {
        float t = ((totalTime - timer )/ totalTime);
        potionsPlease.pitch = Mathf.Lerp(startingPitch,maxPitch,t);
    }

    void ResetGame()
    {
        endGame = false;
        timer = (float)totalTime;
        potionsPlease.pitch = startingPitch;
    }

    Text text;
    static float totalTime = 120; //in seconds
    float timer = totalTime;
    int minutes;
    int seconds;

    float startingPitch = 1.0f;
    float maxPitch = 1.5f;
    int timeToDecreae;

    public RecipeManager manager;
    public bool endGame = false;
    public AudioSource potionsPlease;
}
