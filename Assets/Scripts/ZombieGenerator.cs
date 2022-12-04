using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject Zombie;
    public LayerMask ZombieLayer;

    private GameObject player;

    public float SpawnRange = 3.0f;
    public float MinDistanceFromPlayer = 20.0f;
    public float Cooldown = 1;

    private float counter;

    private float colliderRange = 1.0f;

    void Start()
    {
        player = GameObject.FindWithTag(Tags.PLAYER);
    }

    void Update()
    {
        if (DistanceFromPlayer() > MinDistanceFromPlayer)
        {
            counter += Time.deltaTime;
            if (counter >= Cooldown)
            {
                StartCoroutine(GenerateZombie());
                counter = 0;
            }
        }
    }

    float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    IEnumerator GenerateZombie()
    {
        Vector3 position = RandomPosition();
        Collider[] colliders = Physics.OverlapSphere(
            position,
            colliderRange,
            ZombieLayer
        );
        while (colliders.Length > 0)
        {
            position = RandomPosition();
            colliders = Physics.OverlapSphere(
                position,
                colliderRange,
                ZombieLayer
            );
            yield return null;
        }
        Instantiate(Zombie, position, transform.rotation);
    }

    Vector3 RandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * SpawnRange;
        position += transform.position;
        position.y = transform.position.y;
        return position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SpawnRange);
    }
}
