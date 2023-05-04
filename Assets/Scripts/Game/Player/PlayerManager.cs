using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerManager
{
    void SpawnPlayer();
}

public class PlayerManager : MonoBehaviour, IPlayerManager
{
    public GameObject PlayerPrefab;
    
    Vector3 spawnPoint;

    public void SpawnPlayer()
    {
        throw new System.NotImplementedException();
    }
}
