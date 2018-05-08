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
        // spawnptObject = GameObject.FindGameObjectWithTag("SpawnPoint");
        // spawn = spawnptObject.GetComponent<SpawnPoint>();
        audio = GetComponent<AudioSource>();
        allRecipes = LoadRecipes();
        ChooseRecipe(currentRecipe);
        // deliverPotion();
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
                        audio.Play();
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
        Instantiate(poof, cupObj.transform);
        cupObj.GetComponent<Cup>().LeftBounds();
        Instantiate(sparkles, gameObject.transform);
        numCompletedRecipes++;
        scoreText.text = "Score:\n" + numCompletedRecipes;
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
                        /* if (!spawn.amtInScene.ContainsKey(ingredArr[0]))
                         {
                             spawn.amtInScene.Add(ingredArr[0], AMOUNT_IN_SCENE);
                         }
                         */
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

    void ChooseRecipe(Recipe previous)
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
    int numRecipes = 1;
    int numCompletedRecipes = 0;

  //  public GameObject[] spawnPointObjects;
  //  private SpawnPoint[] spawnPoints;
    AudioSource audio;


    public GameObject sparkles;
    public GameObject poof;
    public bool correctInCup;
    public Text scoreText;
}
