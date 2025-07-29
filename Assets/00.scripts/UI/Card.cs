using UnityEngine;
using UnityEngine.EventSystems;


public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;
    public bool isSelected = false;
    
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

    public void Initalize()
    {
        animator.Rebind();
        isSelected = false;
    }

    public void SetAnimation(string temp)
    {
        if (isSelected) return;
        animator.Play(temp);
    }
}
