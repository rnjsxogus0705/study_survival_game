using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string BulletName;
    public float speed = 10.0f;
    public float lifetime = 5.0f;
    public float delay;
    private Vector3 direction;

    bool isHit = false;

    [SerializeField] private ParticleSystem BulletParticle;
    [SerializeField] private GameObject ExplosionParticle;

    public void Initalize(Vector3 dir)
    {
        isHit = false;
        direction = dir;
        BulletParticle.gameObject.SetActive(true);
        
        BulletParticle.Clear();
        BulletParticle.Play();
        
        ExplosionParticle.SetActive(false);
        StartCoroutine(DestroyCoroutine(5));
    }

    private void Update()
    {
        if (isHit) return;
        transform.position += direction * speed * Time.deltaTime;
    }

    IEnumerator DestroyCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        MANAGER.POOL.m_pool_Dictionary[BulletName].Return(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHit) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            isHit = true;
            BulletParticle.gameObject.SetActive(false);
            ExplosionParticle.SetActive(true);
            other.gameObject.GetComponent<MONSTER>().GetDamage(MANAGER.SESSION.Damage);
            
            StopAllCoroutines();
            StartCoroutine(WaitEffectAndreturn(delay));
        }
    }

    IEnumerator WaitEffectAndreturn(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnBullet();
    }

    private void ReturnBullet()
    {
        ExplosionParticle.SetActive(false);
        StopAllCoroutines();
        MANAGER.POOL.m_pool_Dictionary[BulletName].Return(this.gameObject);
    }
}
