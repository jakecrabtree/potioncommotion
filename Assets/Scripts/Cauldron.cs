using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class Cauldron : MonoBehaviour
{
    public RecipeManager recipeManager;
    public GameObject bubbles;
    public ParticleSystem bubSys;


    private AudioSource sourcySizzle;

    public Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow, Color.cyan, Color.magenta };
    public int currColor = 0;

    bool isPlaying = true;
	// Use this for initialization
	void Start()
	{
        Renderer liquidRenderer = gameObject.transform.Find("Liquid").gameObject.GetComponent<Renderer>();
        liquidRenderer.enabled = false;
        sourcySizzle = GetComponents<AudioSource>()[1];
        recipeManager.endGame.AddListener(EndGame);
        recipeManager.resetGame.AddListener(ResetGame);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

    void EndGame(){
        Renderer liquidRenderer = gameObject.transform.Find("Liquid").gameObject.GetComponent<Renderer>();
        liquidRenderer.enabled = false;
        isPlaying = false;
    }

    void ResetGame(){
        currColor = 0;
        isPlaying = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isPlaying){
            return;
        }
        
        if (other.gameObject.tag != null){
            if (other.gameObject.tag == "cup" || other.gameObject.tag == "LeftHand" || other.gameObject.tag == "RightHand")
            {
            }
            else if (other.gameObject.tag == "cup_liquid"){
                Renderer liquidRenderer = other.gameObject.GetComponent<Renderer>();
                liquidRenderer.enabled = true;
                liquidRenderer.material.color = colors[currColor];
                Cup cup = other.gameObject.transform.parent.gameObject.GetComponent<Cup>();
                cup.containsCompletedPotion = recipeManager.potionCompleted();
                if (cup.containsCompletedPotion){
                    recipeManager.correctInCup = true;
                    recipeManager.UpdateText();
                }
            }
            else{
                sourcySizzle.Play();
                if (recipeManager.addIngredient(other.gameObject.tag))
                {
                    currColor = (currColor + 1) % (colors.Length - 1);
                }
                Renderer liquidRenderer = gameObject.transform.Find("Liquid").gameObject.GetComponent<Renderer>();
                liquidRenderer.enabled = true;
                liquidRenderer.material.color = colors[currColor];
                ParticleSystem.MainModule main = bubSys.main;
                main.startColor = colors[currColor];
                ParticleSystem sys = Instantiate(bubSys, transform);
                sys.Play();
                Destroy(sys, 1);
            }

        }

        //particle effect, smoke color change etc
    }
}
