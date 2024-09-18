using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager: MonoBehaviour
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
    [SerializeField] public List<Card> cardPile;
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
        for (int i = 0; i < list.Count; i += 2)
        {
            playerPile.Add(cardPile[i]);
            computerPile.Add(cardPile[i + 1]);
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
        Shuffle(cardPile);
        Seperate(cardPile);
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
                // Checks if the player has pressed the spacebar and makes sure the player can't spam the spacebar.
            if (!hasClicked)
            {
                // if it has been more than 3 seconds since the player pressed the spacebar, the function compare gets called.
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
                SaveActiveCards();
                Compare();

            }
        }
    }
    [SerializeField]
    private SpriteRenderer back;

    [SerializeField]
    private SpriteRenderer front;


    private static readonly Vector3 playerPilePosition = new Vector3(-4, 0, 0);
    private static readonly Vector3 computerPilePosition = new Vector3(4, 0, 0);

    private static readonly Vector3 playerCardMovePosition = new Vector3(-1.4f, 0.8f, 0f);
    private static readonly Vector3 computerCardMovePosition = new Vector3(1.4f, 0.8f, 0f);

    private const float MoveAnimationDuration = 0.7f;
    private const float FlipAnimationDuration = 0.35f;

    private Tweener tweenScale;
    private Tweener tweenMove;
    private bool isFaceUp;



    private void Awake()
    {
        Transform transform = gameObject.transform;
        isFaceUp = false;
        back.gameObject.SetActive(false);
        front.gameObject.SetActive(false);
    }
    public void InitPosition(bool ownerIsPlayer)
    {
        // Sets the card's potition, depending on who the owner is
        switch (ownerIsPlayer)
        {
            case true:
                transform.position = playerPilePosition;
                break;
            case false:
                transform.position = computerPilePosition;
                break;
        }
    }

    private void AnimatePlayer()
    {
        StartCoroutine(AnimatePlayerMove());
        StartCoroutine(AnimateFlip());
    }

    private void AnimateComputer()
    {
        StartCoroutine(AnimateComputerMove());
        StartCoroutine(AnimateFlip());
    }


    private IEnumerator AnimatePlayerMove()
    {

        if (tweenMove == null)
            tweenMove = transform
                .DOMove(playerCardMovePosition, MoveAnimationDuration)
                .SetEase(Ease.Linear)
                .SetAutoKill(false)
                .OnComplete(() => OnPlayerMoveComplete());

        else
            tweenMove.Restart();

        yield return tweenMove.WaitForCompletion();
    }

    private IEnumerator AnimateComputerMove()
    {

        if (tweenMove == null)
            tweenMove = transform
                .DOMove(computerCardMovePosition, MoveAnimationDuration)
                .SetEase(Ease.Linear)
                .SetAutoKill(false)
                .OnComplete(() => OnComputerMoveComplete());

        else
            tweenMove.Restart();

        yield return tweenMove.WaitForCompletion();
    }


    private IEnumerator AnimateFlip()
    {
        // Scale X from 1 to 0 then back to 1 again,
        // switching between front and back sprites in the middle.
        // This gives the illusion of flipping the card in 2D.
        // Source: https://github.com/gubicsz/Solitaire       

        if (tweenScale == null)
            tweenScale = transform
                .DOScaleX(0f, FlipAnimationDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .SetAutoKill(false)
                .OnStepComplete(() => Flip(isFaceUp))
                .OnComplete(() => CardFlipped());
        else
            tweenScale.Restart();

        yield return tweenScale.WaitForCompletion();
    }

    private void Flip(bool isFaceUp)
    {
        // Flips the card, depending on which side is currently facing up.
        back.gameObject.SetActive(isFaceUp);
        front.gameObject.SetActive(!isFaceUp);
    }

    private void CardFlipped()
    {
        // Switches the 'isFaceUp' value.
        isFaceUp = !isFaceUp;
    }


    private void OnPlayerMoveComplete()
    {
        Debug.Log("Player Card moved");
        //transform.position += playerCardMovement;
        Debug.Log(transform.position);
    }

    private void OnComputerMoveComplete()
    {
        Debug.Log("Computer Card moved");
        //transform.position += computerCardMovement;
        Debug.Log(transform.position);
    }
}
