using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnRadius = 10.0f;
    public float spawnInterval = 3.0f;

    public Transform player; 
    
    public float timer;


    private void Update()
    {
        // 플레이어가 없으면 스폰 로직을 실행하지 않습니다.
        if (player == null) return;
        
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnMonsterAtEdge();
        }
    }

    void SpawnMonsterAtEdge()
    {
        // 1. 플레이어 주변에 스폰 위치를 정합니다.
        Vector3 spawnPos = GetRandomPointOnCircleEdge(player.position, spawnRadius);

        // 2. Instantiate로 몬스터를 '생성'하고, 그 결과를 'monsterInstance'라는 변수에 저장합니다. ★★★
        GameObject monsterInstance = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

        // 3. 방금 생성된 'monsterInstance'에서 Monster_Movement 컴포넌트를 가져옵니다.
        Monster_Movement monsterMovement = monsterInstance.GetComponent<Monster_Movement>();

        // 4. 가져온 컴포넌트의 SetTarget 함수를 호출하여 플레이어를 타겟으로 설정합니다.
        // (monsterMovement가 null이 아닐 때만 호출하여 혹시 모를 오류를 방지합니다.)
        if (monsterMovement != null)
        {
            monsterMovement.SetTarget(player);
        }
    }

    Vector3 GetRandomPointOnCircleEdge(Vector3 center, float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        Vector3 spawnPosition = new Vector3(center.x + x, center.y, center.z + z);
        
        return spawnPosition;
    }
}