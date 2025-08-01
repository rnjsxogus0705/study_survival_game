using UnityEngine;

public class Player_Detector : MonoBehaviour
{
    public LayerMask orbLayer;

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, MANAGER.SESSION.magnetRadius, orbLayer);

        foreach (var hit in hits)
        {
            Orb orb = hit.GetComponent<Orb>();
            if (orb != null && orb.isIdle)
            {
                orb.StartFollow(transform);
            }
        }
    }
}
