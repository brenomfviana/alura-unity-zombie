using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public LayerMask GroundMask;
    public GameObject TextGameOver;
    public UIController ScriptUIC;
    public AudioClip DamageSound;

    public float Speed = 10;
    public int Health = 100;

    private Animator animator;
    private Vector3 direction;
    private Movement movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        Time.timeScale = 1;
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        direction = new Vector3(xAxis, 0, zAxis);

        if (direction != Vector3.zero)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (!IsAlive() && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate()
    {
        movement.Move(direction, Speed);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impact;
        // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out impact, 100, GroundMask))
        {
            Vector3 sightPosition = impact.point - transform.position;
            sightPosition.y = 0;
            movement.Rotate(sightPosition);
        }
    }

    private bool IsAlive()
    {
        return Health > 0;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        ScriptUIC.UpdateHealth();
        AudioController.instance.PlayOneShot(DamageSound);
        if (Health <= 0)
        {
            TextGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
