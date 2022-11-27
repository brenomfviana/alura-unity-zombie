using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject zombie;

    private float counter;
    private float cooldown = 1;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= cooldown)
        {
            Instantiate(zombie, transform.position, transform.rotation);
            counter = 0;
        }
    }
}
