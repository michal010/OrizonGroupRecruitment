using UnityEngine;

public interface IPlayerCollider
{
    void OnTriggerEnter(Collider other);
}

public class EndlessGamePlayerCollider : IPlayerCollider
{
    public void OnTriggerEnter(Collider other)
    {
        // Either get logic of collision from "other"
        // or resolve logic by object tag:
        if (other.tag == "Obstacle")
        {
            GameManager.Instance.GameEvents.OnPlayerHit();
        }
    }
}
