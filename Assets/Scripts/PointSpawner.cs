using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PointSpawner : MonoBehaviour
{
    [SerializeField] private Transform startPointForLevel;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float offset;
    [SerializeField] private SpawnPoint spawnPointPrefab;
    [SerializeField] private WayFinder wayFinder;

    private GameObject[,] spawnPoints = new GameObject[10,10];
    private float OffsetY = 3f;
    private void Awake()
    {
        GenerateSpawnPoints();
    }
    private void GenerateSpawnPoints()
    {
        for (int z=0;z<10;z++)
        {
            for(int x=0;x<10;x++)
            {
                GameObject newPoint = Instantiate(spawnPointPrefab.gameObject, this.transform);
                spawnPoints[x, z] = newPoint;
                spawnPoints[x, z].name = "SpawnPoint" +z + x;
                spawnPoints[x, z].transform.position = new Vector3(spawnPoints[x, z].transform.position.x + offset * x,
    spawnPoints[x, z].transform.position.y, spawnPoints[x, z].transform.position.z + OffsetY * z);
            }
        }
        SetStartAndFinish();
    }
    private void SetStartAndFinish()
    {
        int temp = UnityEngine.Random.Range(0, 4);
        int num1Spawn = 0;
        int num2Spawn = UnityEngine.Random.Range(0, 9);
        switch (temp)
        {
        case 1:
        {
            num1Spawn = 9;
        }
        break;
        case 2:
        {
            num1Spawn = num2Spawn;
            num2Spawn = 0;
        }
        break;
        case 3:
        {
            num1Spawn = num2Spawn;
            num2Spawn = 9;
        }
        break;
            default:
                break;
    }
        GameObject start = spawnPoints[num1Spawn, num2Spawn];
        start.GetComponent<SpawnPoint>().SetPointType(1);
        wayFinder.FindWay(num1Spawn,num2Spawn, spawnPoints);
        SpawnPlayerAndEnemies(num1Spawn,num2Spawn);
        SpawnObstacles();
    }
    private void SpawnObstacles()
    {
        for (int i = 0; i < UnityEngine.Random.Range(40, 60);i++)
        {
            var sp=spawnPoints[UnityEngine.Random.Range(0, 9), UnityEngine.Random.Range(0, 9)];
            SpawnPoint spScript = sp.GetComponent<SpawnPoint>();
            if (spScript.PointType==0)
            {
                spScript.SetPointType(3);
                GameObject block=Instantiate(obstaclePrefab, sp.transform);
                block.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
        
    }
    private void SpawnPlayerAndEnemies(int num1,int num2)
    {
        var sp = spawnPoints[num1,num2];
        SpawnPoint spScript = sp.GetComponent<SpawnPoint>();
            GameObject player = Instantiate(playerPrefab,sp.transform.position+new Vector3(0,1f,0),Quaternion.identity);
        int spawnedEnemies=0;
            do {
                GameObject sp2 = spawnPoints[UnityEngine.Random.Range(0, 9), UnityEngine.Random.Range(0, 9)];
                spScript = sp2.GetComponent<SpawnPoint>();
                if (spScript.PointType == 0)
                {
                    spScript.SetPointType(4);
                    GameObject enemy = Instantiate(enemyPrefab, sp2.transform.position, Quaternion.identity);
                    spawnedEnemies++;
                    if (spawnedEnemies == 2)
                        return;
                }
            }   
        while (true) ;
    }    
}
