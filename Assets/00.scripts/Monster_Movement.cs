    using System.Collections;
    using UnityEngine;

    public class Monster_Movement : MonoBehaviour
    {
        public Transform target;
        public float speed = 3.0f;
        
        private Rigidbody rb;
        private Animator animator;
        
        private bool ispanwed = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }
        
        public void Initalize(Transform player)
        {
            target = player;
            Rotate(direction());
            
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
            animator.SetTrigger("MOVE");
        }

        private void FixedUpdate()
        {
            if (!ispanwed) return;
            
            MoveAndRotate();
        }

        void MoveAndRotate()
        {
            Rotate(direction(), false);
            rb.MovePosition(rb.position + direction() * speed * Time.fixedDeltaTime);
        }

        Vector3 direction()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            return direction;
        }

        void Rotate(Vector3 direction, bool Lerp = true)
        {
            
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                if (Lerp)
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
                else transform.rotation = targetRotation;
            }
        }
}