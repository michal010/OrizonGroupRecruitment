using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleGenerator : BaseObstacleGenerator
{
    public static int MaxObstacles = 4;

    public int Rows = 10;
    public int Columns = 4;
    public float xPadding = 2f;
    public float zPadding = 2f;

    private List<ObstacleDataSO> obstacleDataSOs;
    private const string DATA_PATH = "ObstaclesData";

    public ObstacleGenerator()
    {
        obstacleDataSOs = new List<ObstacleDataSO>();
        LoadObstacleData();
    }

    private void LoadObstacleData()
    {
        obstacleDataSOs.Clear();
        obstacleDataSOs = Resources.LoadAll(DATA_PATH, typeof(ObstacleDataSO)).Cast<ObstacleDataSO>().ToList();
    }

    public override void GenerateObstacles(GameObject mapSegment)
    {
        for (int i = 0; i < MaxObstacles; i++)
        {
            // Calculate position to spawn
            int randomX = UnityEngine.Random.Range(0, Columns);
            int randomZ = UnityEngine.Random.Range(0, Rows);
            Vector3 obstaclePos = new Vector3(randomX * xPadding, -1f, randomZ * zPadding);


            // Choose random obstacle to spawn
            int randomObstacleIndex = UnityEngine.Random.Range(0, obstacleDataSOs.Count);
            //ObstacleDataSO randomSegment = obstacleDataSOs[randomObstacleIndex];
            //GameObject obstaclePrefab = randomSegment.ObstaclePrefab;

            // Code for static obstacle generation, move to separate generator.

            //GameObject obstacle = GameObject.Instantiate(obstaclePrefab);
            //obstacle.transform.parent = mapSegment.transform;
            //obstacle.transform.localPosition = obstaclePos;
            //obstacle.transform.rotation = Quaternion.identity;

            MovingObstacle movingObstacle = MonoFactory.Create<MovingObstacle>("MovingObstacle");
            movingObstacle.Data = new MovingObstacleData { ForwardMovementSpeed = UnityEngine.Random.Range(1.5f, 4f), Player = GameManager.Instance.GetGame().player };
            movingObstacle.transform.parent = mapSegment.transform;
            movingObstacle.transform.localPosition = obstaclePos;
            movingObstacle.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
    }
}
