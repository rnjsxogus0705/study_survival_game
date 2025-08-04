using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
  
    public float detectionRadius;
    public LayerMask monsterLayer;
    public List<Transform> targets = new List<Transform>();
    public Renderer[] renderer;
    bool isHit = false;
    public Transform target 
    { 
        get { return GetNearestMonster(); }
    }

    public Vector3 Direction()
    {
        Vector3 dirToMonster = (target.position - transform.position).normalized;
        return dirToMonster;
    }

    public List<Transform> GetCollidersHitMonsters(float radius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, monsterLayer);
        List<Transform> targetLists = new List<Transform>();
        foreach (Collider col in hits)
        {
            if (col.GetComponent<MONSTER>().isSpanwed)
            {
                targetLists.Add(col.transform);
            }
        }
        return targetLists;
    }

    public Transform GetNearestMonster()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, monsterLayer);
        
        Transform nearest = null;
        float minDist = Mathf.Infinity;
        targets = new List<Transform>();
        foreach (Collider col in hits)
        {
            if (col.GetComponent<MONSTER>().isSpanwed)
            {
                targets.Add(col.transform);
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = col.transform;
                }
            }
        }
 
        return nearest;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if(isHit == false)
               GetDamage(10);
        }
    }

    public void GetDamage(float dmg)
    {
        isHit = true;
        StartCoroutine(FlashEmission(0.5f));
        MANAGER.SESSION.HP -= dmg;
        if (MANAGER.SESSION.HP <= 0)
        { 
            Debug.Log("게임에서 패배하였습니다.");
            return;
        }
    }

    IEnumerator FlashEmission(float fadeTime)
    {
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetColor("_EmissionColor", Color.white);
        }

        float timer = 0.0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            Color current = Color.Lerp(Color.white, Color.black, timer / fadeTime);
            for(int i = 0; i < renderer.Length; i++)
                renderer[i].material.SetColor("_EmissionColor", current);
            yield return null;
        }
        for(int i = 0; i < renderer.Length; i++)
            renderer[i].material.SetColor("_EmissionColor", Color.black);

        isHit = false;
    }
}
