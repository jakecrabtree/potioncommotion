using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandForce : MonoBehaviour {

    private Vector3 lastFrame;
    public Vector3 Force { get; set; }
    public bool hasObject;

	// Use this for initialization
	void Start () {
        lastFrame = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Force *= 0.5f;
        Force += transform.position - lastFrame;

        lastFrame = transform.position;
	}
}
