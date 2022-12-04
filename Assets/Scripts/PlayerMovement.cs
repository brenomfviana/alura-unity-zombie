using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementController
{
    public void Rotate(LayerMask ground)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impact;
        // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out impact, 100, ground))
        {
            Vector3 sightPosition = impact.point - transform.position;
            sightPosition.y = 0;
            Rotate(sightPosition);
        }
    }
}
