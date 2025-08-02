using System.Collections.Generic;
using UnityEngine;

public delegate void OnExpChanaged(float exp);
public delegate void OnMonsterCountChanged(int value);
public delegate void OnSelectedCard();
public class Session_Mng : MonoBehaviour
{
    public OnExpChanaged onExpChanged;
    public OnMonsterCountChanged onMonsterCountChanged;
    public OnSelectedCard onSelectedCard;
    public List<Orb> Orbs = new List<Orb>();
    public Dictionary<string, SelectCard> SelectedCards = new Dictionary<string, SelectCard>();
    
    public int CurrentWave;
    public int Level;
    public int Damage;
    public int monsterCount;

    public float magnetRadius;
    public float EXP;
    public float GameTime;
    public bool isGameOver = false;

    private void Update()
    {
        GameTime += Time.deltaTime;
    }

    public bool HaveCard(CardDB db)
    {
        return SelectedCards.ContainsKey(db.id);
    }

    public void SelectedCard(CardDB db)
    {
        if (HaveCard(db))
        {
            var data = SelectedCards[db.id];
            data.Level++;
        }
        else
        {
            var selected = new SelectCard();
            selected.db = db;
            selected.Level = 1;
            SelectedCards.Add(db.id, selected);
        }
        RegisterSkill(db);
    }

    public void RegisterSkill(CardDB db)
    {
        MANAGER.SKILL.RegisterSkill(db, SelectedCards[db.id].Level);
        onSelectedCard?.Invoke();
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
            Base_Canvas.instance.SelectCard();
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
