using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumpBox : MonoBehaviour
{
    public GameObject deadthEffect;

    public GameObject collectible;
    [Range(0, 100)]public float chanceToDrop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");

            other.transform.parent.gameObject.SetActive(false);

            Instantiate(deadthEffect, other.transform.position, other.transform.rotation);

            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(0, 100f);

            if(dropSelect <= chanceToDrop)
            {
                Instantiate(collectible,other.transform.position, other.transform.rotation);
            }

            AudioManager.instance.PlaySFX(3);
        }
    }
}
