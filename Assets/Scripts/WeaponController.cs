using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawn;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 spawnPosition = spawn.transform.position;
            Quaternion spawnRotation = spawn.transform.rotation;
            Instantiate(bullet, spawnPosition, spawnRotation);
        }
    }
}
