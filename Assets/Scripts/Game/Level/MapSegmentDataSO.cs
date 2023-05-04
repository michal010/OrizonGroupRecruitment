using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSegmentData", menuName = "ScriptableObjects/MapSegmentData", order = 1)]
public class MapSegmentDataSO : ScriptableObject
{
    public GameObject MapSegmnetPrefab;
    public List<Transform> DecorSpawnPoints;
}
