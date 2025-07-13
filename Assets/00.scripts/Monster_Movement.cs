    using UnityEngine;

    public class Monster_Movement : MonoBehaviour
    {
        public Transform target;
        public float speed = 3.0f;

        public void SetTarget(Transform player)
        {
            target = player;
        }

        private void Update()
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }