using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerHealtControler : MonoBehaviour
{
    public int currentHealth, maxHealth;
    public static PlayerHealtControler instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        currentHealth--;

        if(currentHealth <= 0) gameObject.SetActive(false); 
    }
}
