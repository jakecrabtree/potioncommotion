using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAI : MonoBehaviour {

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    private bool isTrotting;

    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        isTrotting = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        // if creature is not wandering, then start wandering.
        if (isWandering == false)
        {
            
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            anim.SetBool("isTrotting", true);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        // set this so that update isnt constantly calling this method
        isWandering = true;

        // wait a few seconds, then walk for a bit
        yield return new WaitForSeconds(walkWait);
        isWalking = true;

        // this is the duration that the walk is for. then stop walking
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        anim.SetBool("isTrotting", false);

        // wait for a few seconds. then turn
        yield return new WaitForSeconds(rotateWait);
        if(rotateLorR == 1)
        {
            isRotatingRight = true;
            anim.SetBool("isTrotting", true);
            yield return new WaitForSeconds(rotTime);
            anim.SetBool("isTrotting", false);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            anim.SetBool("isTrotting", true);
            yield return new WaitForSeconds(rotTime);
            anim.SetBool("isTrotting", false);
            isRotatingLeft = false;
        }
        // this enables this method to repeat
        isWandering = false;
    }
}
