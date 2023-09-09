using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float playerLife;
    [SerializeField] private float impactDamage;
    [SerializeField] private float pushingForce;
    [SerializeField] private float timeToRevive;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    [SerializeField] private Image lifeBar;

    private Vector3 pushDirection;

    private float currentLife;

    public float PlayerLife
    {
        get { return playerLife; }
        set { playerLife = value; }
    }

    public float CurrentLife
    {
        get { return currentLife; }
        set { currentLife = value; }
    }

    public Image LifeBar
    {
        get { return lifeBar; }
        set { lifeBar = value; }
    }

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
                animator.SetTrigger("IsDeath");
                playerController.isDead = true;
                other.GetComponent<CapsuleCollider>().isTrigger = false;
                StartCoroutine(EnableTrigger(other));
            }
        }
    }

    IEnumerator EnableTrigger(Collider other)
    {
        yield return new WaitForSeconds(5);
        other.GetComponent<CapsuleCollider>().isTrigger = true;
    }


}
