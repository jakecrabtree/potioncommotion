using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        time = GetComponentInChildren<Timer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        bool endGame = time.endGame;
        Debug.Log(endGame + collision.gameObject.tag);
        if ((collision.gameObject.CompareTag("LeftHand")
            || collision.gameObject.CompareTag("RightHand")
            || collision.gameObject.CompareTag("LeftModel")
            || collision.gameObject.CompareTag("RightModel"))
            && endGame)
        {
            manager.resetGame.Invoke();
        }
    }

    public RecipeManager manager;
    private Timer time;
}
