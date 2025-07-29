using UnityEngine;

public delegate void OnExpChanaged(float exp);
public delegate void OnMonsterCountChanged(int value);

public class Session_Mng : MonoBehaviour
{
    public OnExpChanaged onExpChanged;
    public OnMonsterCountChanged onMonsterCountChanged;
    public int CurrentWave;
    public int Level;
    public int Damage;
    public int monsterCount;

    public float mangnetRadius;
    public float EXP;
    public float GameTime;
    
    public bool isGameOver = false;

    private void Update()
    {
        GameTime += Time.unscaledDeltaTime;
    }
    public void AddMonster()
    {    
        monsterCount++;
        onMonsterCountChanged?.Invoke(monsterCount);
    }


    public void RemoveMonster()
    {
        monsterCount--;
        onMonsterCountChanged?.Invoke(monsterCount);
    }
    public void AddExp(float exp)
    {
        EXP += exp;
        if (EXP >= GetRequiredExp())
        {
            EXP = 0;
            Level++;
            Time.timeScale = 0;
            Base_Canvas.instance.Selectcard();
        }
        onExpChanged?.Invoke(EXP);
    }

    public int GetRequiredExp()
    {
        int level = Level + 1;
        if (level < 20)
            return (level * 10) - 5;
        else if (level == 20)
            return (level * 10) - 5 + 600;
        else if (level < 40)
            return (level * 13) - 6;
        else if (level == 40)
            return (level * 13) - 6 + 2400;
        else return (level * 16) -8;

    }
}
