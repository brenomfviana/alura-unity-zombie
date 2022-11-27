using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject Zombie;

    private float counter;
    private float cooldown = 1;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= cooldown)
        {
            Instantiate(Zombie, transform.position, transform.rotation);
            counter = 0;
        }
    }
}
