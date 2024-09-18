using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GMSkript : MonoBehaviour
{
    [SerializeField] float delay = 3.0f;
    [SerializeField] GameObject playerDeck;
    [SerializeField] GameObject computerDeck;
    float timer = 0.0f;
    bool hasClicked = false;
    bool playerWon = false;
    bool isDraw = false;
    bool gameWon = false;
    bool gameLost = false;
    [SerializeField] List<Card> activeCards = new();
    [SerializeField] public List<GameObject> cardPile;
    [SerializeField] public List<Card> cardSkript;
    public List<Card> playerPile;
    public List<Card> computerPile;




    void Shuffle<T>(List<T> list)
    {
        // Used chatgpt to create this function.
        // Shuffles a given list of GameObjects/Scripts.
        System.Random rand = new System.Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    void SaveActiveCards()
    {
        //The first cards of the playerPile and computerPile are removed and added to a list called activeCards.
        activeCards.Add(playerPile[0]);
        playerPile.RemoveAt(0);
        activeCards.Add(computerPile[0]);
        computerPile.RemoveAt(0);
    }

    void Seperate<T>(List<T> list)
    {
        // Evenly seperates a given list of gameobjects/scripts onto the playerPile and the computerPile.
        for (int i = 0; i < list.Count - 1; i += 2)
        {
            playerPile.Add(cardSkript[i]);
            computerPile.Add(cardSkript[i + 1]);
        }

    }
    void AddCardsToWinnerComputer()
    {
        // This function adds the cards of the "activeCards" list to the list "computerPile" and removes them from the "activeCard" list.
        foreach (Card card in activeCards)
        {
            computerPile.Add(card);
        }
        activeCards.Clear();
    }
    void AddCardsToWinnerPlayer()
    {
        // This function adds the cards of the "activeCards" list to the list "playerPile" and removes them from the "activeCard" list
        foreach (Card card in activeCards)
        {
            playerPile.Add(card);
        }
        activeCards.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Shuffles and separates the cardPile
        Shuffle(cardSkript);
        Seperate(cardSkript);
    }

    // Update is called once per frame
    void Update()
    {   
        if (hasClicked) 
        {
            //Checkes if the player has pressed the spacebar in the last 3 seconds and sets the boolean hasClicked to false if she didn't.
            if (timer >= delay)
            {
                hasClicked = false;
                timer = 0;
            }
            else
            {
                timer = timer + Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Checks if the player has pressed the spacebar and makes sure the player can't spam the spacebar.
            if (!hasClicked)
            {
                // if it has been more than 3 seconds since the player pressed the spacebar, the function compare gets called.
                Compare();
                if (playerPile.Count == 0)
                {
                    // checks if the player lost the game and manipulates the boolean gameLost accordingly
                    gameLost = true;
                }
                if (computerPile.Count == 0)
                {
                    // checks if the player won the game and manipulates the boolean gameWon accordingly
                    gameWon = true;
                }
            }
        }
    }

    void Compare()
    {
        // A function that compares the top card of the players deck with the top card of the computers deck and
        // decides if the player was higher, if the computer was higher or if it was a draw. In case of a draw it
        // removes the top two cards of both decks, saves them and recurs itself to decide a winner. In case of a
        // winner, it removes the top card of both decks, saves them and then adds all cards saved this way to the
        // bottom of the winners deck.
        if (playerPile[0].cardData.value > computerPile[0].cardData.value)
        {
            playerWon = true;
            SaveActiveCards();
            AddCardsToWinnerPlayer();
        }
        else if (playerPile[0].cardData.value < computerPile[0].cardData.value)
        {
            playerWon = false;
            SaveActiveCards();
            AddCardsToWinnerComputer();

        }
        else
        {
            isDraw = true;
            SaveActiveCards();
            SaveActiveCards();
            Compare();

        }
    }
}
