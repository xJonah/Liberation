using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{

    // Fields
    public GameObject[] diceSides;
    public static DiceScript Instance;
    private SpriteRenderer rend;
    private int finalDiceSide;

    // Use this for initialization
    private void Start()
    {
        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();
    }

    public void SetFinalDiceSide(int finalDiceSide)
    {
        this.finalDiceSide = finalDiceSide;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Coroutine started when DiceBattle() calls it from Tile script
    public void RollDice()
    {
        StartCoroutine(nameof(RollTheDice));
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 1;
        // Loop to switch dice sides ramdomly
        // before final side appears. 10 itterations here.
        for (int i = 0; i <= 10; i++)
        {
            randomDiceSide = Random.Range(0, 5);
            
            // Set sprite to upper face of dice from array according to random value
            for (int x = 0; x < 6; x++)
            {
                diceSides[x].SetActive(false);
            }

            diceSides[randomDiceSide].SetActive(true);
            // Pause before next itteration
            yield return new WaitForSeconds(0.1f);
        }
        diceSides[randomDiceSide].SetActive(false);
        diceSides[finalDiceSide].SetActive(true);
    }
}

