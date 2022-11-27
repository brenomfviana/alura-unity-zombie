using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float speed = 5;
    public GameObject player;

    private new Rigidbody rigidbody;
    private Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        int zombieType = Random.Range(1, 28);
        transform.GetChild(zombieType).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 zombiePosition = transform.position;

        Vector3 direction = (playerPosition - zombiePosition).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        rigidbody.MoveRotation(newRotation);

        float distance = Vector3.Distance(zombiePosition, playerPosition);
        if (distance > 2.5f)
        {
            Vector3 velocity = direction * speed * Time.deltaTime;
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
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.textGameOver.SetActive(true);
        pc.alive = false;
        Time.timeScale = 0;
    }
}
