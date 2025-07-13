using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifetime = 5.0f;
    public GameObject ExplosionParticle;
    private Vector3 direction;
    

    public void Initalize(Vector3 dir)
    {
        direction = dir;
        Destroy(this.gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
