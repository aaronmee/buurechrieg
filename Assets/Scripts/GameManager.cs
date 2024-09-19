using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading;
using System.Diagnostics.Tracing;
using System;


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
    public Vector3 heightener = new (0f, 0f, 0f);
    public Vector3 normalHeight = new (0f, 0f, 0f);
    public Vector3 staticAdder = new(0f, 0.7f, -1f);

    public event EventHandler OnPlayerVictory;
    public event EventHandler OnPlayerDefeat;


    public static GameManager Instance;


    private void Awake()
    {
        // Function that is called before the game starts. 
        //It is used to define certain Variables or move
        //things befor the player sees them.
        // In his case it sets this object as Instance.
        // There is no inpput or output.
        Instance = this;
    }


    public void definePlayerAttribute()
    {
        // A function without in or output which makes sure that all cards inside the players deck
        // have their attribute "owner" set to true, marking them as the players cards
        foreach (Card card in playerPile)
        {
            card.cardData.owner = true;
        }

    }

    public void definePlayerAttributeActivecards()
    {
        // A function without in or output which makes sure that all cards inside the activeCards list 
        // have their attribute "owner" set to true, marking them as the players cards. This function
        // is used after the player won a comparison, to tranfer all of the compared cards to the
        // playerPile.
        foreach (Card card in activeCards)
        {
            card.cardData.owner = true;
            InitPosition(card.cardData.owner, card);
        }

    }

    void defineComputerAttribute()
    {
        // A function without in or output which makes sure that all cards inside the computers deck
        // have their attribute "owner" set to false, marking them as the computers cards.
        foreach (Card card in computerPile)
        {
            card.cardData.owner = false;
            InitPosition(card.cardData.owner, card);
        }
    }

    void defineComputerAttributeActiveCards()
    {
        // A function without in or output which makes sure that all cards inside the activeCards list 
        // have their attribute "owner" set to false, marking them as the computers card. This function
        // is used after the computer won a comparison, to tranfer all of the compared cards to the
        // computerPile.
        foreach (Card card in activeCards)
        {
            card.cardData.owner = false;
        }

    }
    public void Shuffle<T>(List<T> list)
    {
        // Used chatgpt to create this function.
        // It takes a List as an argument but we don't know
        // if it has an output or not.
        // It shuffles a given list of GameObjects/Scripts.
        // It is necessary to randomize our card distribution.
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
        //A Function without Parameters or Outputs.
        //The first cards of the playerPile and computerPile are removed and added to a list called activeCards.
        //This is done to make sure the same card isn't used twice for a comparison or get duplicate because of
        //a failure to remove it from it original Deck.
        activeCards.Add(playerPile[0]);
        playerPile.RemoveAt(0);
        activeCards.Add(computerPile[0]);
        computerPile.RemoveAt(0);
    }

    void Seperate<T>(List<T> list)
    {
        // A function that takes a list as an argument an has no outputs. Seperates a list into two
        // smaller lists of the same size. In our Case the resulting lists are named computerPile and
        // playerPile. They contain our Cards.
        for (int i = 0; i < list.Count; i += 2)
        {
            playerPile.Add(cardPile[i]);
            computerPile.Add(cardPile[i + 1]);
        }

    }
    void AddCardsToWinnerComputer()
    {
        // This function takes no arguments and has no outputs.
        // It adds the cards of the "activeCards" list to the
        // list "computerPile" and removes them from the "activeCard"
        // list. This is used after the Computer won a Comparison.
        defineComputerAttributeActiveCards();
        foreach (Card card in activeCards)
        {
            computerPile.Add(card);
            Thread.Sleep(animationDelay);
            if (card.cardData.isTribut)
            {
                InitPosition(card.cardData.owner, card);
                card.AnimateComputerToDeckDraw();
            }
            else
            {
                InitPosition(card.cardData.owner, card);
                card.AnimateComputerToDeck();
            }
        }
        activeCards.Clear();
    }

    void AddCardsToWinnerPlayer()
    {
        // This function takes no arguments and has no outputs.
        // It adds the cards of the "activeCards" list to the
        // list "playerPile" and removes them from the "activeCard"
        // list. This is used after the Player won a Comparison.
        definePlayerAttributeActivecards();
        foreach (Card card in activeCards)
        {
            playerPile.Add(card);
            Thread.Sleep(animationDelay);
            if (card.cardData.isTribut)
            {
                InitPosition(card.cardData.owner, card);
                card.AnimatePlayerToDeckDraw();

            }
            else 
            {
                InitPosition(card.cardData.owner, card);
                card.AnimatePlayerToDeck();
            }
        }
        activeCards.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Shuffles and separates the cardPile resulting in the playerPile and the computerPile.
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
            //Checkes if the player has pressed the spacebar in the last 3 seconds
            //and sets the boolean hasClicked to false if she didn't.
            if (timer >= clickDelay)
            {
                hasClicked = false;
                timer = 0;
            }
            else
            {
                timer = timer + Time.deltaTime;
                // Makes sure the time isn't affected by the framerate.
            }
        }

        // If statment that listens for a Spacebarpress
        if (Input.GetKeyDown(KeyCode.Space))
        {
                // Makes sure the following code isn't executed if the player
                // has pressed the spacebar in a given amount of time.
            if (!hasClicked)
            {
                if (isDraw)
                {
                    playerPile[0].AnimatePlayerToPile(heightener);
                    InitPositionPile(playerPile[0].cardData.owner, playerPile[0]);
                    computerPile[0].AnimateComputerToPile(heightener);
                    InitPositionPile(computerPile[0].cardData.owner, computerPile[0]);
                }
                else
                {
                    playerPile[0].AnimatePlayerToPile(normalHeight);
                    InitPositionPile(playerPile[0].cardData.owner, playerPile[0]);
                    computerPile[0].AnimateComputerToPile(normalHeight);
                    InitPositionPile(computerPile[0].cardData.owner, computerPile[0]);
                }
                Compare();

            }
        }
    }

    void Compare()
    {
        // Purpose: Compares the top card of the player and computer decks, determines a winner, and handles the result.
        // Arguments: None
        // Output: None (manipulates game state and handles animations accordingly
        // Kommentar von chatGTP
        if (playerPile.Count == 0)
        {
            // checks if the player lost the game and manipulates the boolean gameLost accordingly
            gameLost = true;
            Debug.Log("Player lost the Game");
            OnPlayerVictory?.Invoke(this, EventArgs.Empty);
        }
        if (computerPile.Count == 0)
        {
            // checks if the player won the game and manipulates the boolean gameWon accordingly
            gameWon = true;
            Debug.Log("Player won the Game");
            OnPlayerDefeat?.Invoke(this, EventArgs.Empty);
        }
        // ¨Compares the top card of the players deck with the top card of the computers deck and
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
                isDraw = false;
                heightener = heightener - heightener;
                SaveActiveCards();
                AddCardsToWinnerPlayer();
            }
            else if (playerPile[0].cardData.value < computerPile[0].cardData.value)
            {
                Debug.Log("Player lost comparison");
                playerWon = false;
                isDraw = false;
                SaveActiveCards();
                AddCardsToWinnerComputer();
            }
            else
            {
                Debug.Log("It was a Draw");
                isDraw = true;
                heightener = heightener + staticAdder;
                SaveActiveCards();
                playerPile[0].AnimatePlayerToPileDraw(heightener);
                InitPositionPile(playerPile[0].cardData.owner, playerPile[0]);
                computerPile[0].AnimatePlayerToPileDraw(heightener);
                InitPositionPile(computerPile[0].cardData.owner, computerPile[0]);
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
        // Takes a bool and a object of type Card as arguments and has no outputs.
        // Sets the card's potition to a deck, depending on who the owner is
        switch (ownerIsPlayer)
        {
            case true:
                card.transform.position = playerDeckPosition;
                break;
            case false:
                card.transform.position = computerDeckPosition;
                break;
        }
    }
    public void InitPositionPile(bool ownerIsPlayer, Card card)
    {
        // Takes a bool and a object of type Card as arguments and has no outputs.
        // Sets the card's potition to a pile, depending on who the owner is.
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
