using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpawnPoint : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
			
	}

    public void DestroyedObject(string tag)
    {
        LeftBounds(tag);
    }

    public void LeftBounds(string tag){
        SpawnMore(tag);
    }

    public void SpawnMore(string tag){
        for (int i = 0; i < NUM_TO_SPAWN; i++)
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/" + tag, typeof(GameObject));
            GameObject newObject = Instantiate(prefab);
            newObject.transform.position = transform.position + (i % 2) * Vector3.forward * 0.2f + (i / 2) * Vector3.right * 0.2f;
        }
    }

    public RecipeManager recipeManager;
    private int NUM_TO_SPAWN = 1;
}
