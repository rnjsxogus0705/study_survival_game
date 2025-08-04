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
        
        SelectCard(true);
    }
    
    public Transform HOLDERLAYER;

    public Image EXPFill;
    public CardSelector CardObject;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI TimerText;
    
    public SkillFrame frame;
    public Transform frameContent;
    
    private void SetSkillFrame()
    {
        
            for (int i = 0; i < frameContent.childCount; i++)
                Destroy(frameContent.GetChild(i).gameObject);
        
            foreach(var data in MANAGER.SESSION.SelectedCards)
            {
                var go = Instantiate(frame, frameContent);
                go.Initalize(data.Value);
            }
    }

    private void Update()
    {
        TimerText.text = Utils_UI.FormatTime(MANAGER.SESSION.GameTime);
    }

    public void SelectCard(bool AllActive = false)
    {
        Time.timeScale = 0;
        CardObject.Initalize(AllActive);
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
