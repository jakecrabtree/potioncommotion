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
    public void RespawnAll()
    {
        unoccupied.Clear();
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
                        count = Random.Range(0, ingredientNames.Length);
                    }
                    GameObject currPrefab = Resources.Load<GameObject>("Prefabs/" + ingredientNames[count]);
                    currPrefab.transform.position = pos;
                    Instantiate(currPrefab);
                    count++;
                }
            }
        }
    }

    private float[] possibleX = { 0.75f, 0.5f, 0.25f, 0.0f, -.25f, -.5f };
    private float[] possibleY = { 1.5f, 1.0f };
    private float[] possibleZ = { -.55f };
    private Queue<Vector3> unoccupied = new Queue<Vector3>();
    private string[] ingredientNames = {"Red Dye 40", "Silly Spider", "Tears of a Blue Man", "Blind Eye", "Spiky Boy"};
}
