using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    [SerializeField] Text nameText;
    [SerializeField] Text conversationText;
    [SerializeField] GameObject dialogueCanvas;
    Animator myAnimator;


    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue(string[] dialogue,string name)
    {

      
        nameText.text = name;
        sentences.Clear();

        foreach (string sentence in dialogue)
        {

            sentences.Enqueue(sentence);
        }
        DisplayNexSentence();

    }

    public void DisplayNexSentence()
    {
        if (sentences.Count==0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        conversationText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string charSentence)
    {
        conversationText.text = "";
        foreach (char letter in charSentence.ToCharArray())
        {
            conversationText.text += letter;
            yield return null;

        }


    }


    public void EndDialogue()
    {
        
        CloseDialogueWiindow();

    }

    public void CloseDialogueWiindow()
    {
        
        new WaitForSeconds(0.5f);
        dialogueCanvas.SetActive(false);
    }
}