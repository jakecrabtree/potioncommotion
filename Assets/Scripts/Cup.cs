using UnityEngine;
using System.Collections;

public class Cup : MonoBehaviour
{
    public bool containsCompletedPotion;
    public Vector3 spawnPos;
    public RecipeManager recipeManager;

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
        GameObject cupObj = gameObject;
        Cup cup = cupObj.GetComponent<Cup>();
        Vector3 pos = cup.spawnPos;
        GameObject variableForPrefab = (GameObject)Resources.Load("Prefabs/goblet", typeof(GameObject));
        Instantiate(variableForPrefab);
        variableForPrefab.transform.position = pos;
        Destroy(cupObj, 0.5f);
    }

}
