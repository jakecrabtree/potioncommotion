using UnityEngine;
using System.Collections;

public class RightControls : MonoBehaviour
{
    GameObject heldObject;
    bool hasObject = false;
    private Vector3 lastFrame;
    public Vector3 Force { get; set; }

    // Use this for initialization
    void Start()
    {
        lastFrame = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Force *= 0.5f;
        Force += transform.position - lastFrame;
        lastFrame = transform.position;
        if (Input.GetAxis("HTC_VIU_RightTrigger") == 0)

        {
            if (hasObject)
            {
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Rigidbody>().AddForce(Force * 5000);
                heldObject = null;
                hasObject = false;
            }

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (Input.GetAxis("HTC_VIU_RightTrigger") > 0)
        {
            other.gameObject.transform.parent = gameObject.transform;
            hasObject = true;
            heldObject = other.gameObject;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        /*
        if (Input.GetAxis("HTC_VIU_LeftTrigger") > 0)
        {
            transform.parent = MyLeftHand.transform;
            isLeftHeld = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

        }
        */
        /*
       
        if (Input.GetAxis("HTC_VIU_LeftTrigger") == 0)
        {
            if (isLeftHeld)
            {
                transform.parent = null;
                isLeftHeld = false;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(MyLeftHand.GetComponent<HandForce>().Force * 5000);
            }
        }
        */
    }

}
