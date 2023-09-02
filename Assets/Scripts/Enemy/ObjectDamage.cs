using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    [SerializeField] private float objectLife;
    [SerializeField] private float impactDamage;
    [SerializeField] private GameObject particle;

    
    private float currentLife;
    // Start is called before the first frame update
    void Start()
    {
        currentLife = objectLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLife <= 0)
        {
            Destroy(gameObject, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WeaponPlayer"))
        {
            currentLife -= impactDamage;
            Instantiate(particle, other.transform.position, Quaternion.identity);
        }
    }
}
