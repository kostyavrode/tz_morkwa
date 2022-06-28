using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    private int pointType;
    public int PointType => pointType;
    public int SetPointType(int temp)
    {
        pointType = temp;
        switch (temp)
        {
            case 1:
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
                break;
            case 2:
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                break;
            case 4:
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.grey;
                break;
        }
        return pointType;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (PointType == 2 && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
