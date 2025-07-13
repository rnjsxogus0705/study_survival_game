    using UnityEngine;

    public class Monster_Movement : MonoBehaviour
    {
        public Transform target;
        public float speed = 3.0f;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        public void SetTarget(Transform player)
        {
            target = player;
        }

        private void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            }
        }
    }