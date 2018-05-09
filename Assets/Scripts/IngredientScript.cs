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
        Vector3 pos = spawner.getRandomSpawnPoint();
        GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/" + gameObject.tag, typeof(GameObject));
        variableForPrefab.transform.position = pos;
        variableForPrefab.GetComponent<IngredientScript>().spawner = spawner;
        GameObject instantiated = Instantiate(variableForPrefab);
        spawner.ingredients.Add(instantiated);
        spawner.ingredients.Remove(gameObject);
        Destroy(gameObject);
    }

    public void EndGame()
    {
        Destroy(gameObject);
    }

    private Vector3 mySpawnPos;
    public SpawnPoint spawner;
}
