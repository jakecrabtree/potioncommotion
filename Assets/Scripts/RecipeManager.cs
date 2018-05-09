using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.IO;   // The System.IO namespace contains functions related to loading and saving files
using UnityEngine.UI;

[System.Serializable]
public class RecipeManager : MonoBehaviour
{
    public class Ingredient
    {
        public string name { get; set; }
        public int amt { get; set; }

        public Ingredient(string name, int amt)
        {
            this.amt = amt;
            this.name = name;
        }

    }

    public class Recipe
    {
        public Recipe()
        {
            ingredients = new List<Ingredient>();
        }

        public void addToRecipe(string name, int amt)
        {
            ingredients.Add(new Ingredient(name, amt));
        }

        public override string ToString()
        {
            string ret = string.Concat(name + ": ");
            foreach (Ingredient i in ingredients)
            {
                ret += i.name + " " + i.amt + ", ";
            }
            return ret;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Recipe);
        }

        public bool Equals(Recipe r)
        {
            return r != null && r.name == this.name;
        }


        //Map from ingredient to remaining needed for recipe calls for
        public string name;
        public int totalIngredients;
        public List<Ingredient> ingredients;
    }


    void Start()
    {
        audio = GetComponent<AudioSource>();
        bellDing = Resources.Load<AudioClip>("Audio/bell");
        pencilCheck = Resources.Load<AudioClip>("Audio/pencil-check");
        allRecipes = LoadRecipes();
        ChooseRecipe(currentRecipe);
        roomAudio = GameObject.Find("Room Audio").GetComponent<AudioSource>();
        LoadBarks();
        spawnPoint.RespawnAll();
        spawnPoint.recipeManager = this;
        resetGame.AddListener(ResetGame);
        endGame.AddListener(EndGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool potionCompleted()
    {
        return addedIngredients == currentRecipe.totalIngredients;
    }

    public bool addIngredient(string type)
    {
        if (currentValues.ContainsKey(type))
        {
            int currAmt;
            if (currentValues.TryGetValue(type, out currAmt))
            {
                if (currAmt > 0)
                {
                    currentValues[type] = currAmt - 1;
                    addedIngredients++;
                    if (currentValues[type] == 0)
                    {
                        audio.PlayOneShot(pencilCheck);
                    }
                    UpdateText();
                    return true;
                }
            }
        }
        return false;
    }

    public void deliverPotion(GameObject cupObj)
    {
        correctInCup = false;
        ChooseRecipe(currentRecipe);
        GameObject newPoof = Instantiate(poof, cupObj.transform);
        Destroy(newPoof, 1.5f);
        cupObj.GetComponent<Cup>().LeftBounds();
        GameObject newSparkles = Instantiate(sparkles, gameObject.transform);
        Destroy(newSparkles, 5);
        numCompletedRecipes = 10 * currentRecipe.totalIngredients;
        scoreText.text = "Gold:\n" + numCompletedRecipes;
        audio.PlayOneShot(bellDing);
        Invoke("Bark", 1);
    }

    void ResetGame()
    {
        ChooseRecipe();
        numCompletedRecipes = 0;
        scoreText.text = "Gold:\n" + numCompletedRecipes;
    }

    void EndGame()
    {
        string endMessage = "\nShop's Closed,\n Great Work Today!\n Touch the Time Crystal Ball\n to begin a new day!";
        gameObject.GetComponent<UnityEngine.UI.Text>().text = endMessage;
    }

    void Bark()
    {
        roomAudio.PlayOneShot(barks[Random.Range(0, barks.Length)]);
    }

    List<Recipe> LoadRecipes()
    {
        List<Recipe> recipes = new List<Recipe>();
        string filename = Path.Combine(Application.dataPath, "recipes.txt");
        if (File.Exists(filename))
        {
            using (TextReader reader = File.OpenText(filename))
            {
                string numAsString = reader.ReadLine();
                numRecipes = int.Parse(numAsString);
                //  spawn.amtInScene = new Dictionary<string, int>();
                for (int i = 0; i < numRecipes; i++)
                {
                    Recipe recipe = new Recipe();
                    string recipeAsText = reader.ReadLine();
                    string[] recipeArr = recipeAsText.Split(',');
                    recipe.name = recipeArr[0];
                    recipe.totalIngredients = 0;
                    int numIngredients = int.Parse(recipeArr[1]);
                    for (int j = 0; j < numIngredients; j++)
                    {
                        string ingredAsText = reader.ReadLine();
                        string[] ingredArr = ingredAsText.Split(',');
                        int numOfIngredients = int.Parse(ingredArr[1]);
                        recipe.addToRecipe(ingredArr[0], numOfIngredients);
                        recipe.totalIngredients += numOfIngredients;
                    }
                    recipes.Add(recipe);
                }
            }
        }
        else
        {
            print("Settings not found");
        }
        return recipes;
    }

    void LoadBarks()
    {
        barks = new AudioClip[barkFileNames.Length];
        for (int i = 0; i < barkFileNames.Length; i++)
        {
            barks[i] = Resources.Load<AudioClip>(barkFileNames[i]);
        }
    }

    void ChooseRecipe(Recipe previous = null)
    {
        do
        {
            int index = Random.Range(0, numRecipes);
            currentRecipe = allRecipes[index];
        } while (previous != null && currentRecipe == previous);
        currentValues = new Dictionary<string, int>();
        foreach (Ingredient i in currentRecipe.ingredients)
        {
            currentValues.Add(i.name, i.amt);
        }
        addedIngredients = 0;
        correctInCup = false;
        UpdateText();
    }

    public void UpdateText()
    {
        string ret = "\n\"" + currentRecipe.name + "\"\n\n";
        foreach (KeyValuePair<string, int> entry in currentValues)
        {
            string ingredString = entry.Value + " " + entry.Key;
            if (entry.Value == 0)
            {
                ingredString = StrikeThrough(ingredString);
            }
            ret += ingredString + "\n";
        }

        if (potionCompleted())
        {
            if (!correctInCup)
            {
                ret += "Put me in a cup!\n";
            }
            else
            {
                ret += "Deliver me!\n";
            }
        }

        gameObject.GetComponent<UnityEngine.UI.Text>().text = ret;
    }

    public string StrikeThrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }



    const int AMOUNT_IN_SCENE = 3;
    Recipe currentRecipe;
    Dictionary<string, int> currentValues;
    int addedIngredients;

    List<Recipe> allRecipes;
    //Total # of loaded recipes
    int numRecipes = 1;

    //Current completed amt of recipes
    int numCompletedRecipes = 0;

    AudioSource audio;
    AudioClip pencilCheck;
    AudioClip bellDing;

    AudioSource roomAudio;
    string[] barkFileNames = 
        { "Audio/Barks/IDontFeelSoGood", "Audio/Barks/IveHadBetter",
        "Audio/Barks/justCantGobletDown", /*"Audio/Barks/kirimvose",*/
        "Audio/Barks/manyThanks2", "Audio/Barks/myKidsAreGonnaLoveThis",
        "Audio/Barks/remindsMeOfHome2", "Audio/Barks/thisIsStrong",
        "Audio/Barks/thisIsVial1", "Audio/Barks/whatsInThis2",
        "Audio/Barks/whyIsThisYellow"};
    AudioClip[] barks;

    public SpawnPoint spawnPoint;

    public GameObject sparkles;
    public GameObject poof;
    public bool correctInCup;
    public Text scoreText;

    public UnityEvent endGame = new UnityEvent();
    public UnityEvent resetGame = new UnityEvent();
}
