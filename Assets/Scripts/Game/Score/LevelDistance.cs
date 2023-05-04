using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelDistance
{
    public uint Distance { get; }
}

// Change to seconds.
public class LevelDistance : ILevelDistance
{

    public uint Distance { get { return distance; } private set { distance = value; } }

    private uint distance;
    private IPlayer player;
    
    public LevelDistance(IPlayer player)
    {
        this.player = player;
        Distance = 0;
    }

    public void UpdateDistance()
    {
        //Distance = (uint)player.Transform.position.z;
        Distance = (uint)Time.timeSinceLevelLoad;
    }
}
