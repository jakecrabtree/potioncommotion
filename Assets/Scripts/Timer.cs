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
	}

    void UpdateTime(){
        timer -= Time.deltaTime;
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

    void ResetGame()
    {
        endGame = false;
        timer = (float)totalTime;
    }

    Text text;
    static int totalTime = 120; //in seconds
    float timer = (float)totalTime;
    int minutes;
    int seconds;

    public RecipeManager manager;
    public bool endGame = false;
}
