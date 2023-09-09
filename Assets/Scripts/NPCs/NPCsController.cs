using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsController : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private DialogueController[] dialogues;
    [SerializeField] private int[] turns;
    [SerializeField] private NPCsController[] npcs;

    private Queue<DialogueController> queueDialogues = new Queue<DialogueController>();
    private Animator animator;
    private Quaternion originRotation;
    private DialogueController actualDialogue;

    private bool enableNextDialog;
    private int nextTurn;

    void Start()
    {
        foreach (DialogueController dialogue in dialogues)
        {
            queueDialogues.Enqueue(dialogue);
        }
        enableNextDialog = true;
        nextTurn = 0;
        npcs[turns[nextTurn]].arrow.SetActive(true);
        animator = GetComponent<Animator>();
        originRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (queueDialogues.Count > 0 && gameObject == npcs[turns[nextTurn]].gameObject)
            {
                if (enableNextDialog)
                {
                    enableNextDialog = false;
                    actualDialogue = queueDialogues.Dequeue();
                }
                animator.SetBool("Talking", true);
                actualDialogue.gameObject.SetActive(true);
                actualDialogue.StartDialogue();
            }
        }
    }

    private void Update()
    {
        if (queueDialogues.Count <= 0) {
            arrow.SetActive(false);
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
        if (gameObject == npcs[turns[nextTurn]].gameObject)
        {
            if (actualDialogue.dialogueEnded)
            {
                arrow.SetActive(false);
                enableNextDialog = false;
                nextTurn++;
                if (nextTurn >= turns.Length - 1)
                {
                    nextTurn = turns.Length - 1;
                }
                if (gameObject != npcs[turns[nextTurn]].gameObject)
                {
                    npcs[turns[nextTurn]].nextTurn++;
                }
                npcs[turns[nextTurn]].arrow.SetActive(true);
                npcs[turns[nextTurn]].enableNextDialog = true;
                //if (gameObject == npcs)
                //{
                //    ManageCrystalsController.enableCrystals = true;
                //}
            }
            actualDialogue.EndDialoge();
            animator.SetBool("Talking", false);
            transform.rotation = Quaternion.Slerp(originRotation, transform.rotation, rotationSpeed * Time.deltaTime);

        }
    }
}
