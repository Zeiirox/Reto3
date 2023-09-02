using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float playerLife;
    [SerializeField] private float impactDamage;
    [SerializeField] private Animator animator;

    private CharacterController player;

    private float currentLife;

    private void Start()
    {
        player = GetComponent<CharacterController>();
        currentLife = playerLife;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentLife -= impactDamage;
            animator.SetTrigger("TakingDamage");
            player.transform.Translate(new Vector3(1,1,2) * 10 * Time.deltaTime);
            Debug.Log(currentLife);
        }

    }
}
