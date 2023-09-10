using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectDamage : MonoBehaviour
{
    [SerializeField] private float objectLife;
    [SerializeField] private float impactDamage;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject particle;
    [SerializeField] private Image bloodImage;
    [SerializeField] private TextMeshProUGUI lifeText;

    
    private float currentLife;
    // Start is called before the first frame update
    void Start()
    {
        currentLife = objectLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeText)
        {
            lifeText.text = currentLife.ToString();
            bloodImage.fillAmount = currentLife / objectLife;
        }
        if (currentLife <= 0)
        {
            Destroy(gameObject, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WeaponPlayer"))
        {
            if (animator)
            {
                animator.SetTrigger("Damage");
            }
            currentLife -= impactDamage;
            Instantiate(particle, other.transform.position, Quaternion.identity);
        }
    }

    
}
