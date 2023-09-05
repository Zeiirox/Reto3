using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsController : MonoBehaviour
{
    private ManageCrystalsController ManageCrystalsController;
    [SerializeField] private Animator playerAnimator;

    public bool isVisible;

    private void Start()
    {
        ManageCrystalsController = FindObjectOfType<ManageCrystalsController>();
        isVisible = false;
        gameObject.SetActive(isVisible);
    }

    private void Update()
    {
        if (isVisible)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerAnimator.SetTrigger("Collecting");
                StartCoroutine(Collecting());
            }
        }        
    }

    IEnumerator Collecting()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        ManageCrystalsController.collectedCrystals++;
    }
}
