using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Monster_Movement : MonoBehaviour
{
    [Header("추적 설정")]
    public Transform target;
    public float speed = 3.0f;
    public float stoppingDistance = 1.5f; // 몬스터가 플레이어에게 얼마나 가까이 다가오면 멈출지 결정하는 거리

    private Rigidbody rb;
    private Animator animator;
    private bool isSpawned = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(Transform player)
    {
        target = player;
        StartCoroutine(SpawnAnimationCoroutine());
    }

    private IEnumerator SpawnAnimationCoroutine()
    {
        // ... (기존 스폰 애니메이션 코드는 그대로 유지)
        isSpawned = false;
        Vector3 finalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        float duration = 0.5f;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, timer / duration);
            transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, t);
            yield return null;
        }
        transform.localScale = finalScale;
        isSpawned = true;
    }

    private void FixedUpdate()
    {
        if (!isSpawned || target == null)
        {
            // 아직 스폰되지 않았거나 타겟이 없으면 정지 상태로 처리
            UpdateAnimation(0f);
            return;
        }

        // 플레이어와의 거리를 계산
        float distance = Vector3.Distance(transform.position, target.position);
        
        // 목표를 향하는 방향 계산
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f;

        // 거리에 따라 이동 또는 정지 결정
        if (distance > stoppingDistance)
        {
            // 거리가 멀면 이동
            Move(direction);
            UpdateAnimation(1f); // "이동 중" 상태로 애니메이션
        }
        else
        {
            // 거리가 가까우면 정지 (공격 범위)
            Stop();
            UpdateAnimation(0f); // "정지" 상태로 애니메이션
        }

        // 항상 목표를 바라보도록 회전
        Rotate(direction);
    }

    // 이동 로직을 별도 함수로 분리
    void Move(Vector3 direction)
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    // 정지 로직을 별도 함수로 분리
    void Stop()
    {
        // Rigidbody의 속도를 0으로 만들어 미끄러짐 방지
        rb.linearVelocity = Vector3.zero;
    }

    // 회전 로직을 별도 함수로 분리
    void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);
        }
    }

    // 애니메이션 업데이트를 별도 함수로 분리
    void UpdateAnimation(float speedValue)
    {
        if (animator != null)
        {
            // "Speed"라는 Float 파라미터에 값을 전달 (0이면 정지, 1 이상이면 이동)
            animator.SetFloat("Speed", speedValue);
        }
    }
}