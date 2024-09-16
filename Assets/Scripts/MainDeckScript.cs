using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDeckScript : MonoBehaviour
{
    [SerializeField] public List<GameObject> cardPile;
    public List<GameObject> playerPile;
    public List<GameObject> computerPile;
    //Creates a list wich is named cardPile with every cardobjekt 
    // Start is called before the first frame update
    void Shuffle<T>(List<T> list)
    {
        System.Random rand = new System.Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    //Used chatgpt to create this function
    //The function randomises a given list out of gameobjekts
    void Seperate<T>(List<T> list)
    {
        for (int i = (list.Count /2)-1; i > 0; i--)
        {
            playerPile.Add(cardPile[i]);
            computerPile.Add(cardPile[list.Count/2+ i]);
        }

    }
    //The function seperates the schouffled list in to tow card decks


    void Start()
    {
        Shuffle(cardPile);
        //when the programm starts the list gets schouffled.
        Seperate(cardPile);
        //When the programm starts the card pile gets seperatet.
    }


    // Update is called once per frame
    void Update()
    {

    }
}

