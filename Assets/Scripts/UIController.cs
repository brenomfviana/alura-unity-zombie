using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider PlayerHealth;
    
    private PlayerController scriptPC;
        
    void Start()
    {
        scriptPC = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        PlayerHealth.maxValue = scriptPC.Health;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        PlayerHealth.value = scriptPC.Health;
    }
}
