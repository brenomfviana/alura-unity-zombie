using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int InitialHealth = 100;
    public float Speed = 5;

    [HideInInspector]
    public int Health;

    void Start()
    {
        Health = InitialHealth;
    }
}
