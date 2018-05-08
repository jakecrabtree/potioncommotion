using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientScript : MonoBehaviour {
    // Use this for initialization
    
	void Start () {
        mySpawnPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Cauldron")
        {
            LeftBounds();
        }
    }

    private void LeftBounds(){
        Vector3 pos = mySpawnPos;
        GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/" + gameObject.tag, typeof(GameObject));
        variableForPrefab.transform.position = pos;
        Instantiate(variableForPrefab);
        Destroy(gameObject);
    }
/*
    public Vector3 getRandomSpawnPoint()
    {
        return new Vector3(possibleX[Random.Range(0, possibleX.Length)],
                           possibleY[Random.Range(0, possibleY.Length)],
                          possibleZ[Random.Range(0, possibleZ.Length)]
                         );
    }
*/
    private Vector3 mySpawnPos;
}
