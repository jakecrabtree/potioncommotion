using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpawnPoint : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
        recipeManager.endGame.AddListener(EndGame);
        recipeManager.resetGame.AddListener(ResetGame);
	}

	// Update is called once per frame
	void Update()
	{
			
	}
    public void RespawnAll()
    {
        int count = 0;
        for (int z = 0; z < possibleZ.Length; z++)
        {
            for (int y = 0; y < possibleY.Length; y++)
            {
                for (int x = 0; x < possibleX.Length; x++)
                {
                    Vector3 pos = new Vector3(possibleX[x], possibleY[y], possibleZ[z]);
                    int whichIngredient = count;
                    if (count >= ingredientNames.Length)
                    {
                        whichIngredient = Random.Range(0, ingredientNames.Length);
                    }
                    GameObject currPrefab = Resources.Load<GameObject>("Prefabs/" + ingredientNames[whichIngredient]);
                    currPrefab.transform.position = pos;
                    currPrefab.GetComponent<IngredientScript>().spawner = this;
                    GameObject instantiated = Instantiate(currPrefab);
                    ingredients.Add(instantiated);
                    count++;
                }
            }
        }
        GameObject cupPrefab = (GameObject)Resources.Load("Prefabs/goblet", typeof(GameObject));
        Cup cup = cupPrefab.GetComponent<Cup>();
        Vector3 cupPos = cup.spawnPos;
        GameObject instantiatedCup = Instantiate(cupPrefab);
        instantiatedCup.transform.position = new Vector3(0.998f, 1.032f, 0.166f);;
        ingredients.Add(instantiatedCup);
        instantiatedCup.GetComponent<Cup>().spawner = this;
    }

    public void EndGame(){
        foreach (GameObject obj in ingredients){
            Destroy(obj);
        }
        ingredients = new HashSet<GameObject>();
    }

    public void ResetGame(){
        RespawnAll();
    }
    
    public Vector3 getRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-.5f, 0.75f),
                           Random.Range(1.0f,1.5f),
                          -.55f
                         );
    }


    private float[] possibleX = { 0.75f, 0.5f, 0.25f, 0.0f, -.25f, -.5f };
    private float[] possibleY = { 1.5f, 1.0f };
    private float[] possibleZ = { -.55f };
    private string[] ingredientNames = {"Red Dye 40", "Silly Spider", "Tears of a Blue Man", "Blind Eye", "Spiky Boy"};
    public RecipeManager recipeManager;
    public HashSet<GameObject> ingredients = new HashSet<GameObject>();
}
