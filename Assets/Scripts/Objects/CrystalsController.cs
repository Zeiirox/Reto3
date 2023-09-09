using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject letter;

    private void Start()
    {
        gameObject.SetActive(false);
        letter.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            letter.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerAnimator.SetTrigger("Collecting");
                StartCoroutine(Collecting());
            }
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            letter.gameObject.SetActive(false);
        }        
    }

    IEnumerator Collecting()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        ManageCrystalsController.collectedCrystals++;
    }
}
