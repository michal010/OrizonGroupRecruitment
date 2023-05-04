using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ISegmentGenerator
{
    public void CheckForSegmentGeneration();
    public void GenerateLevel();
    // Change to some custom data object
    public event Action<GameObject> OnSegmentGeneratedEvent;
}

public class SegmentGenerator : ISegmentGenerator
{
    public event Action<GameObject> OnSegmentGeneratedEvent;
    public float Zoffset = 22f;
    public int SegmentsUntilDeletion = 1;
    public int StartingSegmentsCount = 5;

    private Transform mapSegmentParent;
    private IPlayer player;
    private IObstacleGenerator obstacleGenerator;
    private Queue<GameObject> spawnedMapSegments;
    private List<MapSegmentDataSO> mapSegmentDataSOs;
    private int segmentIndex = 0;
    private const string DATA_PATH = "MapSegmentData";
    
    public SegmentGenerator(IObstacleGenerator obstacleGenerator, IPlayer player, Transform mapSegmentParent)
    {
        mapSegmentDataSOs = new List<MapSegmentDataSO>();
        spawnedMapSegments = new Queue<GameObject>();
        this.player = player;
        this.obstacleGenerator = obstacleGenerator;
        this.mapSegmentParent = mapSegmentParent;
        OnSegmentGeneratedEvent += obstacleGenerator.GenerateObstacles;
        LoadSegmentsData();
    }


    // Used on start of the game
    public void GenerateLevel()
    {
        for (int i = 0; i < StartingSegmentsCount; i++)
        {
            GenerateSegment();
        }
    }
    public void CheckForSegmentGeneration()
    {
        Vector3 playerPos = player.Transform.position;
        if (playerPos.z > (1 + segmentIndex - StartingSegmentsCount + SegmentsUntilDeletion) * Zoffset)
        {
            DeleteSegment();
            GenerateSegment();
        }
    }

    private void GenerateSegment()
    {

        // Choose random segment to spawn
        int randomSegmentIndex = UnityEngine.Random.Range(0, mapSegmentDataSOs.Count);
        MapSegmentDataSO randomSegment = mapSegmentDataSOs[randomSegmentIndex];
        Transform mapSegmentTransform = randomSegment.MapSegmnetPrefab.GetComponent<Transform>();


        // Calculate position of new segment
        Vector3 spawnPoint = new Vector3(mapSegmentTransform.position.x, mapSegmentTransform.position.y, mapSegmentTransform.position.z + (segmentIndex * Zoffset));

        // Fetch prefab to be instantiated
        GameObject segmentPrefab = randomSegment.MapSegmnetPrefab;

        // Create new segment
        GameObject mapSegment = GameObject.Instantiate(segmentPrefab, spawnPoint, Quaternion.identity);
        mapSegment.GetComponent<Transform>().SetParent(mapSegmentParent);
        spawnedMapSegments.Enqueue(mapSegment);
        segmentIndex++;
        OnSegmentGeneratedEvent?.Invoke(mapSegment);
    }
    private void DeleteSegment()
    {
        GameObject lastSegment = spawnedMapSegments.Dequeue();
        GameObject.Destroy(lastSegment);
    }

    private void LoadSegmentsData()
    {
        mapSegmentDataSOs.Clear();
        mapSegmentDataSOs = Resources.LoadAll(DATA_PATH, typeof(MapSegmentDataSO)).Cast<MapSegmentDataSO>().ToList();
    }
}
