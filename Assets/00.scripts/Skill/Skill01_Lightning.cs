using UnityEngine;
using System.Collections.Generic;
public class Skill01_Lightning : SkillBase
{
    protected override void OnInitalize() { }

    protected override void OnLevelUp() { }

    protected override void Fire()
    {
        for (int i = 0; i < level; i++)
        {
            if (targets.Count <= 0) return;
            Transform targetPoint = targets[Random.Range(0, targets.Count)];
            var lightning = MANAGER.POOL.Pooling_OBJ("Lightning").Get((value) =>
            {
                value.transform.position = targetPoint.position;
                targetPoint.GetComponent<MONSTER>().GetDamage(Damage());
                value.GetComponent<ParticleSystem>().Play();
            });
        }
    }
}
