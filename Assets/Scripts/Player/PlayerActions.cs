using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerDamage playerDamage;
    [SerializeField] private GameObject weaponPlayer;

    private AudioManager audioManager;

    public bool canAttack;

    public bool canMove
    {
        get { return playerController.canMove; }
        set { playerController.canMove = value; }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        canAttack = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") 
            && playerController.player.isGrounded 
            && !playerController.isDead
            && canAttack)
        {
            audioManager.PlaySFX(audioManager.attack);
            animator.SetTrigger("Attack");

        }
    }

    public void Attack()
    {
        playerController.canMove = false;
        weaponPlayer.SetActive(true);
        canAttack = false;
    }

    public void StopAttacking()
    {
        playerController.canMove = true;
        weaponPlayer.SetActive(false);
        canAttack = true;
    }

    public void StartCollection()
    {
        playerController.canMove = false;
        canAttack = false;
    }

    public void FinishCollection()
    {
        playerController.canMove = true;
        canAttack = true;
    }

    public void Revive()
    {
        animator.Play("PlayerMotion");
        float x = PlayerPrefs.GetFloat("checkPointX");
        float y = PlayerPrefs.GetFloat("checkPointY");
        float z = PlayerPrefs.GetFloat("checkPointZ");
        transform.position = new Vector3(x, y, z);
        playerController.isDead = false;
        playerDamage.CurrentLife = playerDamage.PlayerLife;
        playerDamage.SetLifePlayer();
    }
}
