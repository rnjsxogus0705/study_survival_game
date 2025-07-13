using System.Collections;
using UnityEngine;

public class Player_Attacker : MonoBehaviour
{
    public GameObject bulletPrefab;

    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        FireProjectile();
        StartCoroutine(FireCoroutine());
    }

    void FireProjectile()
    {
        Vector3 fireDir;
        if (Player.instance.target != null)
        {
            fireDir = Player.instance.Direction();
        }
        else
        {
            {
                fireDir = transform.forward;
            }
        }
        
        GameObject bullet = Instantiate(bulletPrefab,
            (transform.position + new Vector3(0,1.0f,0)) + fireDir * 1.0f,
            Quaternion.identity);
        bullet.GetComponent<Bullet>().Initalize(fireDir);
    }
}
