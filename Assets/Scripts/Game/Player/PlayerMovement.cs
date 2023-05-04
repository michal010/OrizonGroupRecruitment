using UnityEngine;

public class PlayerMovement
{
    public float ForwardMovementSpeed = 3.5f;
    public float SideMovementSpeed = 5f;

    private LevelBoundary levelBoundary;
    private Rigidbody rb;
    private IPlayer player;

    public PlayerMovement(Rigidbody rb, IPlayer player, LevelBoundary levelBoundary)
    {
        this.rb = rb;
        this.player = player;
        this.levelBoundary = levelBoundary;
    }

    public void MovePlayer(Vector3 movementVector)
    {
        player.Transform.Translate(player.Transform.forward * Time.deltaTime * ForwardMovementSpeed, Space.World);

        //  left movement
        if(movementVector.x < 0)
        {
            if (player.Transform.position.x > levelBoundary.LeftBoundary)
                player.Transform.Translate(player.Transform.right * Time.deltaTime * SideMovementSpeed * movementVector.x);
        }
        if(movementVector.x > 0)
        {
            if (player.Transform.position.x < levelBoundary.RightBoundary)
                player.Transform.Translate(player.Transform.right * Time.deltaTime * SideMovementSpeed * movementVector.x);
        }

    }
}
