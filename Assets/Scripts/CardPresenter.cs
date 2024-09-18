using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPresenter : MonoBehaviour
{

    

    [Header("Sprites")]
    [SerializeField]
    private SpriteRenderer back;

    [SerializeField]
    private SpriteRenderer front;

    [SerializeField]
    private SpriteRenderer type;


    private Tweener tweenScale;
    private bool isFaceUp;



    private void Awake()
    {
        Transform transform = GetComponent<Transform>();
        isFaceUp = true;
    }


    private void OnMouseDown()
    {
        // When the card is clicked, the flip is initiated
        AnimateFlip();
    }



    private void AnimateFlip()
    {
        // Scale X from 1 to 0 then back to 1 again,
        // switching between front and back sprites in the middle.
        // This gives the illusion of flipping the card in 2D.
        // Srouce: https://github.com/gubicsz/Solitaire       

        if (tweenScale == null)
            tweenScale = transform
                .DOScaleX(0f, 0.7f / 2f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .SetAutoKill(false)
                .OnStepComplete(() => Flip(isFaceUp))
                .OnComplete(() => CardFlipped());
        else
            tweenScale.Restart();
    }

    private void Flip(bool isFaceUp)
    {
        // Flips the card, depending on which side is currently facing up.
        back.gameObject.SetActive(isFaceUp);
        front.gameObject.SetActive(!isFaceUp);
    }

    private void CardFlipped()
    {
        // Updates the 'isFaceUp' variable to the next bool value.

        switch (isFaceUp)
        {
            case true: 
                isFaceUp = false;
                break;
            case false:
                isFaceUp = true;
                break;

        }
    }


}
