using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{

    // Array of dice sides sprites to load from Resources folder
    public GameObject[] diceSides;
    public static DiceScript Instance;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend;

    // Use this for initialization
    private void Start()
    {

        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        // diceSides = Resources.LoadAll<Sprite>("DiceSides/");

    }

    private void Awake()
    {
        Instance = this;
    }

    // If you left click over the dice then RollTheDice coroutine is started
    public void RollDice()
    {
        Debug.Log("Roll dice clicked");
        StartCoroutine("RollTheDice");
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            Debug.Log("Rolled");
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);
            Debug.Log("Dice number is:" + randomDiceSide);

            // Set sprite to upper face of dice from array according to random value
            for (int x = 0; x < 6; x++)
            {
                diceSides[x].SetActive(false);
            }

            diceSides[randomDiceSide].SetActive(true);
            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        Debug.Log(finalSide);
    }
}

