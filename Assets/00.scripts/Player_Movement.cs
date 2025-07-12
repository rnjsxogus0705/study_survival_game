using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    // 카메라의 기본 오프셋을 설정하여 플레이어 뒤에 위치하도록 합니다.
    // 인스펙터 창에서 이 값을 조정할 수 있습니다.
    public Vector3 cameraOffset = new Vector3(0, 5, -7); 

    private Camera m_Camera;
    private Vector3 moveDir;
    private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        // 1. Camera.main을 사용하여 메인 카메라를 올바르게 찾습니다.
        m_Camera = Camera.main; 
        if (m_Camera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다! 카메라에 'MainCamera' 태그가 있는지 확인해주세요.");
        }

        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        Animate();
    }

    // LateUpdate는 모든 Update 호출이 끝난 후에 실행되므로,
    // 플레이어 이동이 완료된 후 카메라를 이동시키는 데 적합합니다.
    private void LateUpdate()
    {
        if (m_Camera != null)
        {
            CameraMove();
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 2. 카메라의 방향을 기준으로 이동 방향을 계산합니다.
        Vector3 camForward = m_Camera.transform.forward;
        Vector3 camRight = m_Camera.transform.right;
        camForward.y = 0; // 높이 변화는 무시합니다.
        camRight.y = 0;

        // 카메라의 방향에 따라 입력 값을 변환합니다.
        moveDir = (camForward.normalized * v + camRight.normalized * h).normalized;
        
        // CharacterController.SimpleMove는 Time.deltaTime을 자동으로 적용해줍니다.
        controller.SimpleMove(moveDir * moveSpeed);
    }

    void Rotate()
    {
        // 이동 방향이 있을 때만 회전합니다.
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            // Slerp를 사용하여 부드럽게 회전합니다.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
    }

    void Animate()
    {
        // 애니메이터의 "SPEED" 파라미터에 이동 속도를 전달합니다.
        // moveDir의 크기를 사용하여 걷거나 뛰는 애니메이션을 제어할 수 있습니다.
        animator.SetFloat("SPEED", moveDir.magnitude);
    }

    void CameraMove()
    {
        // 3. 목표 위치를 '플레이어 위치 + 오프셋'으로 설정합니다.
        Vector3 targetPosition = transform.position + cameraOffset;
        
        // Lerp를 사용하여 카메라를 부드럽게 이동시킵니다.
        m_Camera.transform.position = Vector3.Lerp(
            m_Camera.transform.position,
            targetPosition,
            2.0f * Time.deltaTime);
        
        // 카메라가 항상 플레이어를 바라보도록 합니다.
        m_Camera.transform.LookAt(transform.position);
    }
}