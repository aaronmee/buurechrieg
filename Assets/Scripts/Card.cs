using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public CardData cardData;
    // Tells Unity that the Card GameObjects have scriptable objects of the type CardData.
    // Start is called before the first frame update
    [SerializeField]
    private SpriteRenderer back;
    [SerializeField]
    private SpriteRenderer front;

    private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

    private static readonly Vector3 pDeckPosition = new Vector3(7f, 0f, 0f);
    public static readonly Vector3 cDeckPosition = new Vector3(-7f, 0f, 0f);

    private static readonly Vector3 pPilePosition = new Vector3( 2.5f, 0f, 0f);
    public static readonly Vector3 cPilePosition = new Vector3( -2.5f, 0f, 0f);


    private const float MoveAnimationDuration = 0.7f;
    private const float FlipAnimationDuration = 0.35f;

    private Tweener tweenScale;
    private Tweener tweenMove;
    private bool isFaceUp;



    private void Awake()
    {
        //Function that is called before the game starts. 
        //It is used to define certain Variables or move
        //things befor the player sees them
        Transform transform = gameObject.transform;
        isFaceUp = false;
        back.gameObject.SetActive(false);
        front.gameObject.SetActive(false);
    }
    public void AnimatePlayerToPile(Vector3 addVector)
    {
        // Function that takes a vector as an argument and has no output.
        // It animates a card from the playerDeck to the players cardPile
        // and flips it over, so we see its frontside instead of the back.
        // It needs the vector to move it up the z axis and down the y axis
        // so that the Card isnt covered up by the card that are already on
        // the pile
        StartCoroutine(AnimatePlayerMove(pPilePosition, addVector));
        StartCoroutine(AnimateFlip());
    }
    public void AnimatePlayerToDeck()
    {
        // Function that takes no argument and has no output.
        // It animates a card from the playerPile to the players Deck
        // and flips it over, so we see its put back inside the deck with
        // it's backside up instead of the front.

        StartCoroutine(AnimatePlayerMove(pDeckPosition, zeroVector));
        StartCoroutine(AnimateFlip());
    }
    public void AnimatePlayerToPileDraw(Vector3 addVector)
    {
        // Function that takes a vector as an argument and has no output.
        // It animates a card from the playerDeck to the players cardPile
        // in case of a draw. We dont flip the card, because it isn't used
        // for comparison. It needs the vector to move it up the z axis and
        // down the y axis so that the Card isnt covered up by the card that
        // are already on the pile
        cardData.isTribut = true;
        StartCoroutine(AnimatePlayerMove(pPilePosition, addVector));
    }
    public void AnimatePlayerToDeckDraw()
    {
        // Function that takes no argument and has no output.
        // It animates a card from the playerDeck to the players cardPile
        // in case of a draw. We dont flip the card, because it wasn't used
        // for comparison and therefore already is backside up.
        cardData.isTribut = false;
        StartCoroutine(AnimatePlayerMove(pDeckPosition, zeroVector));
    }
    public void AnimateComputerToPile(Vector3 addVector)
    {
        // Function that takes a vector as an argument and has no output.
        // It animates a card from the computerDeck to the computers cardPile.
        // It needs the vector to move it up the z axis and down the y axis
        // so that the Card isnt covered up by the card that are already on
        // the pile
        StartCoroutine(AnimateComputerMove(cPilePosition, addVector));
        StartCoroutine(AnimateFlip());
    }
    public void AnimateComputerToDeck()
    {
        // Function that takes no argument and has no output.
        // It animates a card from the computerPile to the computer Deck
        // and flips it over, so we see its put back inside the deck with
        // it's backside up instead of the front.
        StartCoroutine(AnimateComputerMove(cDeckPosition, zeroVector));
        StartCoroutine(AnimateFlip());
    }
    public void AnimateComputerToPileDraw(Vector3 addVector)
    {
        // Function that takes a vector as an argument and has no output.
        // It animates a card from the computerDeck to the computers cardPile
        // in case of a draw. We dont flip the card, because it isn't used
        // for comparison. It needs the vector to move it up the z axis and
        // down the y axis so that the Card isnt covered up by the card that
        // are already on the pile.
        cardData.isTribut = true;
        StartCoroutine(AnimateComputerMove(cPilePosition, addVector));

    }
    public void AnimateComputerToDeckDraw()
    {
        // Function that takes no argument and has no output.
        // It animates a card from the computerDeck to the computers cardPile
        // in case of a draw. We dont flip the card, because it wasn't used
        // for comparison and therefore already is backside up.
        cardData.isTribut = false;
        StartCoroutine(AnimateComputerMove(cDeckPosition, zeroVector));
    }

    private IEnumerator AnimatePlayerMove(Vector3 position, Vector3 addVector)
    {
        // This function takes a two Vectors as arguments and has the output:
        // yield return tweenMove.WaitForCompletion(); It uses the vectores to
        // determin the position the cards should be moved to. This function
        // specifically animates the players cards

        position = position + addVector;
        if (tweenMove == null)
            tweenMove = transform
                .DOMove(position, MoveAnimationDuration)
                .SetEase(Ease.Linear)
                .SetAutoKill(false)
                .OnComplete(() => OnPlayerMoveComplete());

        else
            tweenMove.Restart();

        yield return tweenMove.WaitForCompletion();
    }

    private IEnumerator AnimateComputerMove(Vector3 position, Vector3 addVector)
    {
        // This function takes a two Vectors as arguments and has the output:
        // yield return tweenMove.WaitForCompletion(); It uses the vectores to
        // determin the position the cards should be moved to. This function
        // specifically animates the computers cards
        position = position + addVector;
        if (tweenMove == null)
            tweenMove = transform
                .DOMove(position, MoveAnimationDuration)
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
