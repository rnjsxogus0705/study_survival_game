using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    }
    
    private void OnDestroy()
    {
        MANAGER.SESSION.onExpChanged -= EXPChange;
        MANAGER.SESSION.onMonsterCountChanged -= M_CountText;
    }

    public Transform HOLDERLAYER;

    public Image EXPFill;
    public CardSelector CardObject;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI monsterCountText;
    public TextMeshProUGUI TimerText;

    private void Update()
    {
        TimerText.text = Utils_UI.FormatTime(MANAGER.SESSION.GameTime);
    }

    public void SelectCard()
    {
        if (CardObject != null && !CardObject.gameObject.activeInHierarchy)
            CardObject.gameObject.SetActive(true);
        CardObject.Initalize();
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
