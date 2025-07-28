using UnityEngine;

public class MONSTER : MonoBehaviour
{
    public float HP;
    public float MaxHP;

    public Transform target;

    public string monsterid;

    public bool isSpanwed = false;
    public bool isDead = false;

    private IFactory<MONSTER> factory;

    public virtual void Initalize(Transform player)
    {
        isSpanwed = true;
        HP = 10;
        MaxHP = HP;

        monsterid = Random.Range(0, 2) == 1 ? "Skeleton_01" : "Skeleton_02";
        factory = new GenericPartFactory<MONSTER>(MANAGER.DB.Monster);
        target = player;
        factory.Build(this, monsterid);
    }

    public void GetDamage(int dmg)
    {
        HP -= dmg;
        var damageFont = MANAGER.POOL.Pooling_OBJ("DamageTMP").Get((value) =>
        {
            value.GetComponent<DamageTMP>().Initalize(
                Base_Canvas.instance.transform, 
                transform.position, 
                dmg.ToString());
        });            
        if (HP <= 0)
        {
            isDead = true;
            var deadEffect = MANAGER.POOL.Pooling_OBJ("DeadEffect").Get((value) =>
            {
                value.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            });

            MANAGER.instance.Run(Util_Coroutine.Delay(0.5f,
                () => MANAGER.POOL.m_pool_Dictionary["DeadEffect"].Return(deadEffect)));

            MANAGER.POOL.m_pool_Dictionary["Monster"].Return(this.gameObject);
            DropEXP(transform.position, Random.Range(1.0f, 5.0f));
            
        }
        
    }
    

    private void DropEXP(Vector3 deathPosition, float exp = 1.0f)
    {
        float[] units = { 3.0f, 1.0f, .25f };

        foreach(float unit in units)
        {
            while(exp >= unit)
            {
                exp -= unit;

                OrbMake(deathPosition, unit);
              
            }
        }

        if(exp > 0.01f)
        {
            OrbMake(deathPosition, exp);
        }
    }

    private void OrbMake(Vector3 deathPosition, float exp)
    {
        Vector3 spawnPos = deathPosition + Utils_World.GetRandomCircleOffset(1.5f);
        spawnPos.y += 0.5f;
        var orb = MANAGER.POOL.Pooling_OBJ("Orb").Get((value) =>
        {
            value.transform.position = transform.position;
            value.GetComponent<Orb>().Initalize(exp, spawnPos);
        });
    }
}
