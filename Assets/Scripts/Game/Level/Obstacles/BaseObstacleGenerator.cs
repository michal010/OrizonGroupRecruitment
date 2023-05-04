using UnityEngine;

public interface IObstacleGenerator
{
    abstract void GenerateObstacles(GameObject mapSegment);
}

public abstract class BaseObstacleGenerator : IObstacleGenerator
{
    public abstract void GenerateObstacles(GameObject mapSegment);
}