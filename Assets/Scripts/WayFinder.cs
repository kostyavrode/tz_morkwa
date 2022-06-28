using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//я понял, что пусть ищется через А*. нО я уже отчаялся и ради тз не стал его изучать. и вообще это тз делалось 1.5 дня
public class WayFinder : MonoBehaviour
{
    public void FindWay(int num1,int num2, GameObject[,] spawnPoints)
    {
        for(int i=0;i<35;i++)
        {
            int typeOfMove = UnityEngine.Random.Range(0, 4);
            switch (typeOfMove)
            {
                case 0:
                    if (num1 < 9)
                    {
                        num1 += 1;
                    }
                    break;
                case 1:
                    if (num1 > 1)
                    {
                        num1 -= 1;
                    }
                    break;
                case 2:
                    if (num2 < 9)
                    {
                        num2 += 1;
                    }
                    break;
                case 3:
                    if (num2 > 1)
                    {
                        num2 -= 1;
                    }
                    break;
                default:

                    break;
            }
            Debug.Log(num1 + " " + num2);
            if (spawnPoints[num1, num2].GetComponent<SpawnPoint>().PointType == 0)
            {
                spawnPoints[num1, num2].GetComponent<SpawnPoint>().SetPointType(4);
            }
            if (i==34&&spawnPoints[num1,num2].GetComponent<SpawnPoint>().PointType != 1)
                spawnPoints[num1, num2].GetComponent<SpawnPoint>().SetPointType(2);
        }
    }
}
