using UnityEngine;
using System.Collections;

public class Cup : MonoBehaviour
{
    public bool containsCompletedPotion;
    public Vector3 spawnPos;
    public RecipeManager recipeManager;
    public SpawnPoint spawner;

	// Use this for initialization
	void Start()
	{
        containsCompletedPotion = false;
        spawnPos = new Vector3(0.998f,1.032f,0.166f);
    }

    // Update is called once per frame
    void Update()
	{
			
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            LeftBounds();
        }
    }

    public void LeftBounds(){
        Cup cup = gameObject.GetComponent<Cup>();
        Vector3 pos = cup.spawnPos;
        GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/goblet", typeof(GameObject));
        variableForPrefab.GetComponent<Cup>().spawner = spawner;
        GameObject instantiated = Instantiate(variableForPrefab);
        instantiated.transform.position = pos;
        spawner.ingredients.Add(instantiated);
        spawner.ingredients.Remove(gameObject);
        Destroy(gameObject, 0.5f);
    }

}
