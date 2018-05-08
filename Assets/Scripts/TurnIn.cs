using UnityEngine;
using System.Collections;

public class TurnIn : MonoBehaviour
{
    public RecipeManager recipeManager;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
	    		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != null && other.gameObject.tag == "cup")
        {
            Cup cup = other.gameObject.GetComponent<Cup>();
            if (cup.containsCompletedPotion)
            {
                recipeManager.deliverPotion(other.gameObject);
            }
        }
    }
}
