using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

    Text text;
    static int totalTime = 120; //in seconds
    float timer = (float)totalTime;
    int minutes;
    int seconds;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        UpdateTime();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTime();
	}

    void UpdateTime(){
        timer -= Time.deltaTime;
        minutes = (int)timer / 60;
        seconds = (int)timer % 60;
        text.text = "Time:\n" + minutes + ":" + seconds;
    }
}
