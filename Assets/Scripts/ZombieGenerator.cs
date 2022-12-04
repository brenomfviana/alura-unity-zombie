using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject Zombie;

    public float Cooldown = 1;

    private float counter;

    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= Cooldown)
        {
            Instantiate(Zombie, transform.position, transform.rotation);
            counter = 0;
        }
    }
}
