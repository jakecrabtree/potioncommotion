using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabimation : MonoBehaviour {

    Animator anim;
    private bool buttonDown;


    public string inputAxis = "HTC_VIU_LeftTrigger";

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        buttonDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        buttonDown = Input.GetAxis(inputAxis) > 0;
        anim.SetBool("Grabbing", buttonDown);
        
	}

}
