using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDeckScript : MonoBehaviour
{


    [SerializeField] public List<int> cardPile;
    public List<int> playerPile;
    public List<int> computerPile;



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

