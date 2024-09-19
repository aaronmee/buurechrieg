using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading;


public class GameManager: MonoBehaviour
{
    [SerializeField] float clickDelay = 3.0f;
    [SerializeField] int animationDelay = 1;
    [SerializeField] GameObject playerDeck;
    [SerializeField] GameObject computerDeck;
    float timer = 0.0f;
    bool hasClicked = false;
    bool playerWon = false;
    bool isDraw = false;
    bool gameWon = false;
    bool gameLost = false;
    [SerializeField] List<Card> activeCards = new();
    [SerializeField] public List<Card> cardPile;
    public List<Card> playerPile;
    public List<Card> computerPile;


    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static Vector3 GetPlayerDeckPosition()
    {
        return playerDeckPosition;
    }


    public void definePlayerAttribute()
    {
        //for every card in playerPile the attribute owner gets true.
        foreach (Card card in playerPile)
        {
            card.cardData.owner = true;
        }

    }

    public void definePlayerAttributeActivecards()
    {
        //for every card in activeCards the attribute owner gets true.
        foreach (Card card in activeCards)
        {
            card.cardData.owner = true;
            InitPosition(card.cardData.owner, card);
        }

    }

    void defineComputerAttribute()
    {
        //for every card in computerPile the attribute owner gets false.
        foreach (Card card in computerPile)
        {
            card.cardData.owner = false;
            InitPosition(card.cardData.owner, card);
        }
    }

    void defineComputerAttributeActiveCards()
    {
        //for every card in activeCards the attribute owner gets false.
        foreach (Card card in activeCards)
        {
            card.cardData.owner = false;
        }

    }
    public void Shuffle<T>(List<T> list)
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
        for (int i = 0; i < list.Count; i += 2)
        {
            playerPile.Add(cardPile[i]);
            computerPile.Add(cardPile[i + 1]);
        }

    }
    void AddCardsToWinnerComputer()
    {
        // This function adds the cards of the "activeCards" list to the list "computerPile" and removes them from the "activeCard" list.
        defineComputerAttributeActiveCards();
        foreach (Card card in activeCards)
        {
            computerPile.Add(card);
            Thread.Sleep(animationDelay);
            if (card.cardData.isTribut)
            {
                card.AnimateComputerToDeckDraw();
            }
            else
            {
                card.AnimateComputerToDeck();
            }
        }
        activeCards.Clear();
    }

    void AddCardsToWinnerPlayer()
    {
        // This function adds the cards of the "activeCards" list to the list "playerPile" and removes them from the "activeCard" list
        definePlayerAttributeActivecards();
        foreach (Card card in activeCards)
        {
            playerPile.Add(card);
            Thread.Sleep(animationDelay);
            if (card.cardData.isTribut)
            {
                card.AnimatePlayerToDeckDraw();
            }
            else 
            {
                card.AnimatePlayerToDeck();
            }
        }
        activeCards.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Shuffles and separates the cardPile
        Shuffle(cardPile);
        Seperate(cardPile);
        defineComputerAttribute();
        definePlayerAttribute();
    }

    // Update is called once per frame
    void Update()
    {   
        if (hasClicked) 
        {
            //Checkes if the player has pressed the spacebar in the last 3 seconds and sets the boolean hasClicked to false if she didn't.
            if (timer >= clickDelay)
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
                // Checks if the player has pressed the spacebar and makes sure the player can't spam the spacebar.
            if (!hasClicked)
            {
                // if it has been more than 3 seconds since the player pressed the spacebar, the function compare gets called.
                playerPile[0].AnimatePlayerToPile();
                computerPile[0].AnimateComputerToPile();
                Compare();
            }
        }
    }

    void Compare()
    {
        if (playerPile.Count == 0)
        {
            // checks if the player lost the game and manipulates the boolean gameLost accordingly
            gameLost = true;
            Debug.Log("Player lost the Game");
        }
        if (computerPile.Count == 0)
        {
            // checks if the player won the game and manipulates the boolean gameWon accordingly
            gameWon = true;
            Debug.Log("Player won the Game");
        }
        // A function that compares the top card of the players deck with the top card of the computers deck and
        // decides if the player was higher, if the computer was higher or if it was a draw. In case of a draw it
        // removes the top two cards of both decks, saves them and recurs itself to decide a winner. In case of a
        // winner, it removes the top card of both decks, saves them and then adds all cards saved this way to the
        // bottom of the winners deck.
        if (gameWon == false && gameLost == false)
        {
            Debug.Log(playerPile[0]);
            Debug.Log(computerPile[0]);
            if (playerPile[0].cardData.value > computerPile[0].cardData.value)
            {
                Debug.Log("Player Won comparison");
                playerWon = true;
                SaveActiveCards();
                AddCardsToWinnerPlayer();
            }
            else if (playerPile[0].cardData.value < computerPile[0].cardData.value)
            {
                Debug.Log("Player lost comparison");
                playerWon = false;
                SaveActiveCards();
                AddCardsToWinnerComputer();
            }
            else
            {
                Debug.Log("It was a Draw");
                isDraw = true;
                SaveActiveCards();
                playerPile[0].AnimatePlayerToPileDraw();
                computerPile[0].AnimatePlayerToPileDraw();
                SaveActiveCards();
            }
        }
    }

    public static readonly Vector3 playerDeckPosition = new Vector3(7, 0, 0);
    public static readonly Vector3 computerDeckPosition = new Vector3(-7, 0, 0);

    public static readonly Vector3 playerPilePosition = new Vector3(2.5f, 0f, 0f);
    public static readonly Vector3 computerPilePosition = new Vector3(-2.5f, 0f, 0f);


    private Tweener tweenScale;
    private Tweener tweenMove;
    private bool isFaceUp;

    public void InitPosition(bool ownerIsPlayer, Card card)
    {
        // Sets the card's potition, depending on who the owner is
        switch (ownerIsPlayer)
        {
            case true:
                card.transform.position = playerPilePosition;
                break;
            case false:
                card.transform.position = computerPilePosition;
                break;
        }
    }
}
