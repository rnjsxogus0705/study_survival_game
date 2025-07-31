using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardDB card;
    [SerializeField] TextMeshProUGUI Title, Description;
    [SerializeField] Image IconImage;
    [SerializeField] Image OutlineImage;
    Animator animator;
    public bool isSelected = false;

    public Color[] colors;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        SetAnimation("Card_PointerDown");
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        SetAnimation("Card_PointerUp");
    }

    public void Initalize(CardDB cardDB)
    {
        if (cardDB == null)
        {
            Debug.LogError("Card: cardDB가 null입니다.");
            return;
        }
        card = cardDB;
        
        // OutlineImage.color = card.state == CardState.Active ? colors[0] : colors[1];
        // Title.text = card.id;

        // Description.text = string.Format(card.description, card.DamagePercentage);
        // IconImage.sprite = MANAGER.DB.GetSprite(card.name);

        // animator.Rebind();
        if (OutlineImage != null)
            OutlineImage.color = card.state == CardState.Active ? colors[0] : colors[1];
        else
            Debug.LogError("Card: OutlineImage가 할당되지 않았습니다.");
        
        if (Title != null)
            Title.text = card.id;
        else
            Debug.LogError("Card: Title이 할당되지 않았습니다.");
        
        if (Description != null)
            Description.text = string.Format(card.description, card.DamagePercentage);
        else
            Debug.LogError("Card: Description이 할당되지 않았습니다.");

        if (IconImage != null && !string.IsNullOrEmpty(card.name))
            IconImage.sprite = MANAGER.DB.GetSprite(card.name);
        else
            Debug.LogError("Card: IconImage가 할당되지 않았거나 card.name이 유효하지 않습니다.");
        
        if (animator == null)
            animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.Rebind();
            isSelected = false;
        } 
        else
            Debug.LogError("Card: Animator 컴포넌트를 찾을 수 없습니다.");
        
    }

    public void SetAnimation(string temp)
    {
        if (isSelected) return;
        animator.Play(temp);
    }
}
