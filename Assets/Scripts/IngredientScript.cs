using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
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
        Vector3 pos = getRandomSpawnPoint();
        GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/" + gameObject.tag, typeof(GameObject));
        Instantiate(variableForPrefab);
        variableForPrefab.transform.position = pos;
        Destroy(gameObject);
    }

    public Vector3 getRandomSpawnPoint()
    {
        return new Vector3(possibleX[Random.Range(0, possibleX.Length)],
                           possibleY[Random.Range(0, possibleY.Length)],
                           possibleZ[Random.Range(0, possibleZ.Length)]
                          );
    }


    float[] possibleX = { 0.75f, 0.5f, 0.25f, 0.0f, -.25f, -.4f };
    float[] possibleY = { 1.5f, 1.0f };
    float[] possibleZ = { -.55f };
}
