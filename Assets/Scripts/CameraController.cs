using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    private Vector3 distance;

    void Start()
    {
        distance = transform.position - Player.transform.position;
    }

    void Update()
    {
        transform.position = distance + Player.transform.position;
    }
}
