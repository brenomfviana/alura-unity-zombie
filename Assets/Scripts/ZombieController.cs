using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float Speed = 5;
    public GameObject Player;

    private new Rigidbody rigidbody;
    private Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
        int zombieType = Random.Range(1, 28);
        transform.GetChild(zombieType).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        Vector3 PlayerPosition = Player.transform.position;
        Vector3 zombiePosition = transform.position;

        Vector3 direction = (PlayerPosition - zombiePosition).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        rigidbody.MoveRotation(newRotation);

        float distance = Vector3.Distance(zombiePosition, PlayerPosition);
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
