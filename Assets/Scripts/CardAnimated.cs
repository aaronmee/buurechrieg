using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardAnimated : MonoBehaviour
{



    [SerializeField]
    private SpriteRenderer back;

    [SerializeField]
    private SpriteRenderer front;


    public static readonly Vector3 playerPilePosition = new Vector3(-4, 0, 0);
    public static readonly Vector3 computerPilePosition = new Vector3(4, 0, 0);

    public static readonly Vector3 playerCardMovePosition = new Vector3(-1.4f, 0.8f, 0f);
    public static readonly Vector3 computerCardMovePosition = new Vector3(1.4f, 0.8f, 0f);

    public const float MoveAnimationDuration = 0.7f;
    public const float FlipAnimationDuration = 0.35f;


    [SerializeField] private bool ownerIsPlayer;



    private Tweener tweenScale;
    private Tweener tweenMove;
    private bool isFaceUp;



    private void Awake()
    {
        Transform transform = gameObject.transform;
        isFaceUp = false;
        back.gameObject.SetActive(true);
        front.gameObject.SetActive(false);

        InitPosition(ownerIsPlayer);

        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed();
        }
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




    public void OnSpacePressed()
    {
        switch (ownerIsPlayer)
        {
            case true:
                AnimatePlayer();
                break;
            case false:
                AnimateComputer();
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
