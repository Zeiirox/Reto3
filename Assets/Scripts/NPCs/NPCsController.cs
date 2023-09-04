using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsController : MonoBehaviour
{
    [SerializeField] private DialogueController dialogue;
    [SerializeField] private float rotationSpeed;
    private Quaternion originRotation;

    void Start()
    {
        originRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.gameObject.SetActive(true);
            dialogue.StartDialogue();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 position = other.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.rotation = Quaternion.Slerp(originRotation, transform.rotation, rotationSpeed * Time.deltaTime);
    }
}
