using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject weaponPlayer;

    private bool canAttack;
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
}
