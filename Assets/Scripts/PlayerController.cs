using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IKillable
{
    public LayerMask GroundMask;
    public GameObject TextGameOver;
    public UIController ScriptUIC;
    public AudioClip DamageSound;
    
    [HideInInspector]
    public Status status;

    private PlayerMovement movement;
    private AnimatorController animator;

    private Vector3 direction;

    void Start()
    {
        status = GetComponent<Status>();

        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<AnimatorController>();

        Time.timeScale = 1;
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        direction = new Vector3(xAxis, 0, zAxis);

        animator.Move(direction.magnitude);

        if (!IsAlive() && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate()
    {
        movement.Move(direction, status.Speed);
        movement.Rotate(GroundMask);
    }

    private bool IsAlive()
    {
        return status.Health > 0;
    }

    public void TakeDamage(int damage)
    {
        status.Health -= damage;
        ScriptUIC.UpdateHealth();
        AudioController.instance.PlayOneShot(DamageSound);

        if (status.Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        TextGameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
