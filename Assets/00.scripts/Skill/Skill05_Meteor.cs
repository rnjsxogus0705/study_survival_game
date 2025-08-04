using NUnit.Framework;
using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Skill05_Meteor : SkillBase
{
    private int count;
    protected override void Fire()
    {
        StartCoroutine(MeteorCoroutine());
    }

    IEnumerator MeteorCoroutine()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            var meteor = MANAGER.POOL.Pooling_OBJ("Meteor").Get((value) =>
            {
                Vector3 pos = RandomPos(10.0f);
                value.transform.position = pos;
                MANAGER.instance.Run(Util_Coroutine.Delay(0.35f,() =>
                {
                    foreach (var monster in RangeMonsterLists(pos, 5.0f))
                    {
                        monster.GetDamage(Damage());
                    }
                }));
                
                MANAGER.instance.Run(Util_Coroutine.Delay(1.0f, () =>
                {
                    MANAGER.POOL.m_pool_Dictionary["Meteor"].Return(value);
                }));
                
            });
        }
    }
    
    protected override void OnInitalize()
    {
        count = 1 + (level - 1);
    }

    protected override void OnLevelUp()
    {
        count = 1 + (level - 1);

    }
}
