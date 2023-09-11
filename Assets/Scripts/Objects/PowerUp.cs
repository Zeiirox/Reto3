using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int additionalLife;
    private PlayerDamage playerDamage;
    private AudioManager audioManager;

    private void Start()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerDamage.CurrentLife < playerDamage.PlayerLife)
            {
                playerDamage.CurrentLife += additionalLife;
                playerDamage.SetLifePlayer();
                audioManager.PlaySFX(audioManager.powerUp);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
