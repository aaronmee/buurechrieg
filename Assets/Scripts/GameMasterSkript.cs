using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    bool gameWon = false;
    bool gameLost = false;
    List<Card> activeCards;
    [SerializeField] public List<GameObject> cardPile;
    [SerializeField] public List<Card> cardSkript;
    public List<Card> playerPile;
    public List<Card> computerPile;




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
    void SaveActiveCards()
    {
        activeCards.Add(playerPile[0]);
        playerPile.Remove(playerPile[0]);   
        activeCards.Add(computerPile[0]);
        computerPile.Remove(computerPile[0]);  
    }

    void Seperate<T>(List<T> list)
    {
        // Evenly seperates a given list out of gameobjects onto the playerPile and the computerPile
        for (int i = 0; i < list.Count - 1; i += 2)
        {
            playerPile.Add(cardSkript[i]);
            computerPile.Add(cardSkript[i + 1]);
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
        Shuffle(cardSkript);
        Seperate(cardSkript);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasClicked) 
        {
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
            if (!hasClicked)
            {
                Debug.Log("Fuck Me");
                Compare();
                if (playerPile.Count == 0)
                {
                    gameLost = true;
                }
                if (computerPile.Count == 0)
                {
                    gameWon = true;
                }
            }
        }
    }

    void Compare()
    {
        Debug.Log(playerPile);
        Debug.Log(playerPile[0].cardData.value);
        Debug.Log(computerPile[0].cardData.value);
        if (playerPile[0].cardData.value > computerPile[0].cardData.value)
        {
            playerWon = true;
            SaveActiveCards();
            AddCardsToWinnerPlayer();
            Debug.Log("True");
        }
        else if (playerPile[0].cardData.value < computerPile[0].cardData.value)
        {
            playerWon = false;
            Debug.Log("False");
            SaveActiveCards();
            AddCardsToWinnerComputer();

        }
        else
        {
            isDraw = true;
            Debug.Log("Draw");
            SaveActiveCards();

        }
    }
}
