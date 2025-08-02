using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Base_Canvas : MonoBehaviour
{
    public static Base_Canvas instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    

    private void Start()
    {
        EXPChange(0);
        MANAGER.SESSION.onExpChanged += EXPChange;
        MANAGER.SESSION.onMonsterCountChanged += M_CountText;
        MANAGER.SESSION.onSelectedCard += SetSkillFrame;
        SelectCard();
    }
    
    public Transform HOLDERLAYER;

    public Image EXPFill;
    public CardSelector CardObject;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI TimerText;
    
    public SkillFrame frame;
    public Transform activeFrameContent;
    public Transform passiveFrameContent;
    List<GameObject> SkillFrameGorvage = new List<GameObject>();
    
    private void SetSkillFrame()
    {
        if (SkillFrameGorvage.Count > 0)
        {
            for (int i = 0; i < SkillFrameGorvage.Count; i++)
                Destroy(SkillFrameGorvage[i]);

            SkillFrameGorvage.Clear();
        }
        
        foreach(var data in MANAGER.SESSION.SelectedCards)
        {
            var go = Instantiate(frame, 
                data.Value.db.state == CardState.Active ? 
                activeFrameContent:
                passiveFrameContent);

            go.Initalize(data.Value);
            SkillFrameGorvage.Add(go.gameObject);
        }
    }

    private void Update()
    {
        TimerText.text = Utils_UI.FormatTime(MANAGER.SESSION.GameTime);
    }

    public void SelectCard()
    {
        if (CardObject != null && !CardObject.gameObject.activeInHierarchy)
            CardObject.gameObject.SetActive(true);
        Time.timeScale = 0;
        if (CardObject != null)
        {
            if (!CardObject.gameObject.activeInHierarchy)
                CardObject.gameObject.SetActive(true);
            CardObject.Initalize();
        }
        else
        {
            Debug.LogError("Base_Canvas: CardObject가 null입니다.");
        }
    }

    private void M_CountText(int value) => monsterCountText.text = value.ToString();
    public void EXPChange(float exp)
    {
        float expPercentage = exp / MANAGER.SESSION.GetRequiredExp();
        EXPFill.fillAmount = expPercentage;
        LevelText.text =
            string.Format(
            "Lv.{0} <color=#FFFF00>{1:0.0}</color>%",
            (MANAGER.SESSION.Level + 1),
            expPercentage * 100.0f); 
    }
}
