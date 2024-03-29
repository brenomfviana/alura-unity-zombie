using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Spawn;
    public AudioClip ShotSound;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 SpawnPosition = Spawn.transform.position;
            Quaternion SpawnRotation = Spawn.transform.rotation;
            Instantiate(Bullet, SpawnPosition, SpawnRotation);
            AudioController.instance.PlayOneShot(ShotSound);
        }
    }
}
