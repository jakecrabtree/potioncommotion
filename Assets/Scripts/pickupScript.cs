using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour {

    private bool rightCanGrab;
    private bool leftCanGrab;
    private bool isRightHeld;
    private bool isLeftHeld;
    private static bool rightHasObject;
    private static bool leftHasObject;

    private GameObject MyRightHand;
    private GameObject MyLeftHand;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("HTC_VIU_RightTrigger") > 0 && rightCanGrab && !rightHasObject)
        {
            transform.parent = MyRightHand.transform;
            isRightHeld = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (Input.GetAxis("HTC_VIU_LeftTrigger") > 0 && leftCanGrab && !leftHasObject)
        {
            transform.parent = MyLeftHand.transform;
            isLeftHeld = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

        }
        if (Input.GetAxis("HTC_VIU_RightTrigger") == 0)

        {
            if (isRightHeld)
            {
                transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(MyRightHand.GetComponent<HandForce>().Force * 5000);
                isRightHeld = false;
                rightHasObject = false;
            }


        }
        if (Input.GetAxis("HTC_VIU_LeftTrigger") == 0)
        {
            if (isLeftHeld)
            {
                transform.parent = null;
                isLeftHeld = false;
                leftHasObject = false;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(MyLeftHand.GetComponent<HandForce>().Force * 5000);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "LeftHand")
        {
            MyLeftHand = other.gameObject;
            leftCanGrab = true;
        }
        if (other.tag == "RightHand")
        {
            MyRightHand = other.gameObject;
            rightCanGrab = true;
        }
    }

    void OnDestroy()
    {
         if (isRightHeld)
        {
            rightHasObject = false;
        }
         if (isLeftHeld)
        {
            leftHasObject = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "LeftHand")
        {
            MyLeftHand = null;
            leftCanGrab = false;
        }
        if (other.tag == "RightHand")
        {
            MyRightHand = null;
            rightCanGrab = false;
        }
    }

}
