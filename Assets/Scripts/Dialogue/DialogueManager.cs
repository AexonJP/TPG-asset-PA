using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel; // A panel to group all dialogue UI elements
    // public AudioSource audioSource; // Add an AudioSource to play the audio clips

    private Queue<DialogueLine> sentences; // Updated to hold DialogueLine objects
    private DialogueScriptable currentDialogue;

    void Start()
    {
        sentences = new Queue<DialogueLine>();
        dialoguePanel.SetActive(false); // Hide dialogue panel at start
    }

    public void StartDialogue(DialogueScriptable dialogue)
    {
        dialoguePanel.SetActive(true); // Show the dialogue panel
        currentDialogue = dialogue;

        sentences.Clear();

        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine line)
    {
        nameText.text = line.characterNameIndonesian;
        dialogueText.text = "";

        // if (line.audioClip != null)
        // {
        //     audioSource.clip = line.audioClip;
        //     audioSource.Play();
        // }

        float typingSpeed = line.duration / line.lineIndonesian.Length;

        foreach (char letter in line.lineIndonesian.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(1.0f); // Add a short pause after the text is fully displayed
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Hide the dialogue panel
        Debug.Log("End of dialogue.");
    }
}
