using UnityEngine;

public class MONSTER : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    
    public Transform target;
    public string monsterId;
    
    protected bool isSpanwed = false;
    public bool isDead = false;
    
    private IFactory<MONSTER> factory;

    public virtual void Initalize(Transform player)
    {
        isSpanwed = true;
        HP = 10;
        MaxHP = HP;

        monsterId = Random.Range(0, 2) == 1 ? "Skeleton_01" : "Skeleton_02";
        factory = new GenericPartFactory<MONSTER>(MANAGER.DB.Monster);
        target = player;
        factory.Build(this, monsterId);
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
                value.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
            });
            
                MANAGER.instance.Run(Util_Coroutine.Delay(0.5f,
                () => MANAGER.POOL.m_pool_Dictionary["DeadEffect"].Return(deadEffect)));
            
            MANAGER.POOL.m_pool_Dictionary["Monster"].Return(this.gameObject);
            
        }
    }
}
