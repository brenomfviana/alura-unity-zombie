using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject Player;

    public float Speed = 5;

    private new Rigidbody rigidbody;
    private Animator animator;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        int zombieType = Random.Range(1, 28);
        transform.GetChild(zombieType).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 zombiePosition = transform.position;

        Vector3 direction = (playerPosition - zombiePosition).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        rigidbody.MoveRotation(newRotation);

        float distance = Vector3.Distance(zombiePosition, playerPosition);
        if (distance > 2.5f)
        {
            Vector3 velocity = direction * Speed * Time.deltaTime;
            Vector3 position = rigidbody.position + velocity;
            rigidbody.MovePosition(position);
            animator.SetBool("IsAttacking", false);
        }
        else
        {
            animator.SetBool("IsAttacking", true);
        }
    }

    void HitPlayer()
    {
        PlayerController pc = Player.GetComponent<PlayerController>();
        int damage = Random.Range(20, 30);
        pc.TakeDamage(damage);
    }
}
