using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private PlayerActions playerActions;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpreed = 0.1f;
    private int index;

    private void Start()
    {
    }

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
        string nickName = PlayerPrefs.GetString("nickName") ?? "Zeirox";
        lines[index] = lines[index].Replace("@nickname", nickName);
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
