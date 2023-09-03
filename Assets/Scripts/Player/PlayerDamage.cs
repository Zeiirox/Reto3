using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float playerLife;
    [SerializeField] private float impactDamage;
    [SerializeField] private float pushingForce;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    [SerializeField] private Image lifeBar;

    private Vector3 pushDirection;

    private float currentLife;
    private void Start()
    {
        currentLife = playerLife;
    }

    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 10)) 
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                pushDirection = -fwd.normalized * pushingForce;
            }
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentLife -= impactDamage;
            animator.SetTrigger("TakingDamage");
            lifeBar.fillAmount = currentLife / playerLife;
            playerController.player.Move(pushDirection);
            if (currentLife <= 0)
            {
                playerController.player.center = new Vector3(0, 1.88f, 0);
                //playerController.player.enabled = false;
                animator.SetTrigger("IsDeath");
                playerController.isDead = true;
            }
        }
    }

    public void StopAnimator()
    {
        //Time.timeScale = 0;
    }


}
