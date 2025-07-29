using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelector : MonoBehaviour
{
    public Card[] cards;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Intialize()
    {
        animator.Play("Selector_Open");
        for (int i = 0; i < cards.Length; i++) cards[i].Initalize();
    }
    public void SelectCard(int value)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (i == value) cards[i].SetAnimation("Card_Select");
            else cards[i].SetAnimation("Card_NoneSelect");
            cards[i].isSelected = true;
        }

        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        animator.Play("Selector_Close");
        Time.timeScale = 1.0f;
    }
}
