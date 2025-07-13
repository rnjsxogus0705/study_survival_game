    using System.Collections;
    using UnityEngine;

    public class Monster_Movement : MonoBehaviour
    {
        public Transform target;
        public float speed = 3.0f;
        private Rigidbody rb;
        bool ispanwed = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        
        public void Initalize(Transform player)
        {
            target = player;
            StartCoroutine(SpawnStartCoroutine(transform.localScale));
        }

        IEnumerator SpawnStartCoroutine(Vector3 scaleEnd)
        {
            Vector3 ScaleStart = Vector3.zero;
            Vector3 ScaleEnd = scaleEnd;
            float duration = 0.5f;
            float timer = 0.0f;
            while (timer < duration)
            {
                float t = timer / duration;
                transform.localScale = Vector3.Lerp(ScaleStart, ScaleEnd, t);
                timer += Time.deltaTime;
                yield return null;
            }
            
            ispanwed = true;
        }

        private void FixedUpdate()
        {
            if (!ispanwed) return;
            
            MoveAndRotate();
        }

        void MoveAndRotate()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }
            
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        } 
}