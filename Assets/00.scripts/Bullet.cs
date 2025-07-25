using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifetime = 5.0f;
    public GameObject ExplosionParticle;
    public GameObject DamageObject;
    private Vector3 direction;
    

    public void Initalize(Vector3 dir)
    {
        direction = dir;
        StartCoroutine(DestroyCoroutine(5));
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    IEnumerator DestroyCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        MANAGER.POOL.m_pool_Dictionary["Projectile"].Return(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity);

            var damageFont = MANAGER.POOL.Pooling_OBJ("DamageTMP").Get((value) =>
            {
                value.GetComponent<DamageTMP>().Initalize(
                    Base_Canvas.instance.transform, 
                    transform.position, 
                    "10");
            });
            
            StopAllCoroutines();
            MANAGER.POOL.m_pool_Dictionary["Projectile"].Return(this.gameObject);
        }
    }
}
