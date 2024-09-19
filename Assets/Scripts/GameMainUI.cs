using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMainUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI playerCardCounterText;
    [SerializeField] private TextMeshProUGUI computerCardCounterText;



    private void Awake()
    {
        playerCardCounterText.text = "16 Karten";
        computerCardCounterText.text = "16 Karten";
    }

    void Update()
    {
        //playerCardCounterText.text = GameManager.Instance.playerPile.Count + " Karten";
        //computerCardCounterText.text = GameManager.Instance.computerPile.Count + " Karten";
    }
}
