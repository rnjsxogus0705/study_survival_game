using System.Collections;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    public Card[] cards;
    Animator animator;
    bool isSelected = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Initalize()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            animator.Play("Selector_Open");
            var Cards = MANAGER.DB.GetRandomCardSet();
            for (int i = 0; i < cards.Length; i++) cards[i].Initalize(Cards[i]);
        }
        else
        {
            Debug.LogError("CardSelector: Animator 컴포넌트를 찾을 수 없습니다.");
        }
    }

    public void SelectCard(int value)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (i == value)
            {
                cards[i].SetAnimation("Card_Select");
                MANAGER.SESSION.SelectedCard(cards[i].card);
            }
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
