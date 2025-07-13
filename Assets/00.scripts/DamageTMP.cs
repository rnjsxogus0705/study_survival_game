using System.Collections;
using TMPro;
using UnityEngine;

public class DamageTMP : MonoBehaviour
{
    private TextMeshProUGUI m_Text;
    private RectTransform rectTransform;

    private Vector2 velocity;
    private float gravity = -1000.0f;
    private float lifetime = 1.0f;
    
    private Color textColor;
        
    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initalize(Transform parent, Vector3 pos, string temp)
    {
        transform.SetParent(parent);
        
        m_Text.text = temp;

        Vector2 screemPosition = Camera.main.WorldToScreenPoint(pos);
        rectTransform.position = screemPosition;
        
        velocity = new Vector2(Random.Range(-50.0f, 50.0f), Random.Range(150.0f, 250.0f));
        
        textColor = m_Text.color;

        StartCoroutine((MoveAndFade()));
    }

    IEnumerator MoveAndFade()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < lifetime)
        {
            velocity.y += gravity * Time.deltaTime;

            rectTransform.anchoredPosition += velocity * Time.deltaTime;

            textColor.a = Mathf.Lerp(1.0f, 0.0f, elapsedTime / lifetime);
            
            elapsedTime += Time.deltaTime;
            yield return null;

        }
        
        Destroy(this.gameObject);
    }
}
