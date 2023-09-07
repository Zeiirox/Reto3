using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private PlayerActions playerActions;

    public string[] lines;
    public float textSpreed = 0.1f;
    private int index;

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
            
        }
    }

    public void StartDialogue()
    {
        playerActions.canAttack = false;
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpreed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            playerActions.canAttack = true;
            gameObject.SetActive(false);
        }
    }

    public void EndDialoge()
    {
        StopAllCoroutines();
        dialogueText.text = string.Empty;
        playerActions.canAttack = true;
        gameObject.SetActive(false);
    }
}