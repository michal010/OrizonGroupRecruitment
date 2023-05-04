using UnityEngine;

public struct MovingObstacleData
{
    public IPlayer Player;
    public float ForwardMovementSpeed;
}


[FromFactory("MovingObstacle", true)]
public class MovingObstacle : MonoBehaviour
{
    private bool isDestroying = false;
    private bool playerNearby = false;
    public MovingObstacleData Data;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!playerNearby)
        {
            CheckForPlayerProximity();
            return;
        }
        if (isDestroying)
            return;
        Move();
        CheckForDeletion();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Data.ForwardMovementSpeed);
        transform.LookAt(Data.Player.Transform);
    }

    // For now, until obstacle generator will be moved separately to mapSegment itself.
    // So it's gonna be possible to mix and match diffrent segments, with cool
    // mechanics
    private void CheckForPlayerProximity()
    {
        if(transform.position.z - Data.Player.Transform.position.z < 22f)
        { playerNearby = true; }
    }

    private void CheckForDeletion()
    {
        if (Data.Player.Transform.position.z > transform.position.z)
        {
            isDestroying = true;
            Destroy(gameObject, 3f);
        }
    }
}
