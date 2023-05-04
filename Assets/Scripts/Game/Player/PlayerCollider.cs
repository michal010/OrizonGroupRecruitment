using UnityEngine;



[RequireComponent(typeof(Collider))]
public class PlayerCollider : MonoBehaviour
{
    // Inject logics
    public IPlayerCollider playerCollider;

    private void OnTriggerEnter(Collider other)
    {
        playerCollider.OnTriggerEnter(other);
    }
}
