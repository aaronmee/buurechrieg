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


    private static readonly Vector3 playerDeckPosition = new Vector3(7, 0, 0);
    private static readonly Vector3 computerDeckPosition = new Vector3(-7, 0, 0);

    private static readonly Vector3 playerPilePosition = new Vector3( 2.5f, 0f, 0f);
    private static readonly Vector3 computerPilePosition = new Vector3( -2.5f, 0f, 0f);


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
    public void AnimatePlayerToPile()
    {
        StartCoroutine(AnimatePlayerMove(playerPilePosition));
        StartCoroutine(AnimateFlip());
    }
    public void AnimatePlayerToDeck()
    {
        StartCoroutine(AnimatePlayerMove(playerDeckPosition));
        StartCoroutine(AnimateFlip());
    }
    public void AnimatePlayerToPileDraw()
    {
        cardData.isTribut = true;
        StartCoroutine(AnimatePlayerMove(playerPilePosition));
    }
    public void AnimatePlayerToDeckDraw()
    {
        cardData.isTribut = false;
        StartCoroutine(AnimatePlayerMove(playerDeckPosition));
    }
    public void AnimateComputerToPile()
    {
        StartCoroutine(AnimateComputerMove(computerPilePosition));
        StartCoroutine(AnimateFlip());
    }
    public void AnimateComputerToDeck()
    {
        StartCoroutine(AnimateComputerMove(computerDeckPosition));
        StartCoroutine(AnimateFlip());
    }
    public void AnimateComputerToPileDraw()
    {
        cardData.isTribut = true;
        StartCoroutine(AnimateComputerMove(computerPilePosition));

    }
    public void AnimateComputerToDeckDraw()
    {
        cardData.isTribut = false;
        StartCoroutine(AnimateComputerMove(computerDeckPosition));
    }

    private IEnumerator AnimatePlayerMove(Vector3 position)
    {

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

    private IEnumerator AnimateComputerMove(Vector3 position)
    {

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
