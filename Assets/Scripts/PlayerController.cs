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

    private PlayerMovement movement;
    private AnimatorController animator;
    private Vector3 direction;

    void Start()
    {
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
        movement.Move(direction, Speed);
        movement.Rotate(GroundMask);
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
