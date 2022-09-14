using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public GameObject obstacle;
    private void OnTriggerEnter(Collider other)
    {
        obstacle.SetActive(true);
    }
}
