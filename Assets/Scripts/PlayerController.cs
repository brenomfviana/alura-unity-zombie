using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public bool Alive = true;
    public LayerMask GroundMask;
    public GameObject TextGameOver;

    private new Rigidbody rigidbody;
    private Animator animator;
    private Vector3 direction;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

        if (!Alive && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate()
    {
        Vector3 velocity = direction * Speed * Time.deltaTime;
        Vector3 position = rigidbody.position + velocity;
        rigidbody.MovePosition(position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impact;
        // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out impact, 100, GroundMask))
        {
            Vector3 sightPosition = impact.point - transform.position;
            sightPosition.y = transform.position.y;
            Quaternion rotation = Quaternion.LookRotation(sightPosition);
            rigidbody.MoveRotation(rotation);
        }
    }
}
