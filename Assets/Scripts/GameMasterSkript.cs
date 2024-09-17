using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMSkript : MonoBehaviour
{
    [SerializeField] float delay = 1.0f;
    [SerializeField] GameObject playerDeck;
    [SerializeField] GameObject computerDeck;
    float timer = 0.0f;
    bool hasClicked = false;
    bool playerWon = false;
    bool isDraw = false;
    List<GameObject> activeCards;

    [SerializeField] public List<GameObject> cardPile;
    public List<GameObject> playerPile;
    public List<GameObject> computerPile;



    void Shuffle<T>(List<T> list)
    {
        // Used chatgpt to create this function
        // Shuffles a given list out of gameobjects
        System.Random rand = new System.Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    void Seperate<T>(List<T> list)
    {
        // Evenly seperates a given list out of gameobjects onto the playerPile and the computerPile
        for (int i = 0; i < list.Count - 1; i += 2)
        {
            playerPile.Add(cardPile[i]);
            computerPile.Add(cardPile[i + 1]);
        }

    }
    void AddCardsToWinnerComputer()
    {
        // This function ads the values of the active cards list to the Computerlist
        for (int i = 0; i < activeCards.Count - 1; i ++)
        {
            computerPile.Add(activeCards[0]);
            activeCards.Remove(activeCards[0]);
        }

    }
    void AddCardsToWinnerPlayer()
    {
        // This function ads the values of the active cards list to the Playerlist
        for (int i = 0; i < activeCards.Count - 1; i++)
        {
            playerPile.Add(activeCards[0]);
            activeCards.Remove(activeCards[0]);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        // Shuffles and separates the cardPile
        Shuffle(cardPile);
        Seperate(cardPile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
