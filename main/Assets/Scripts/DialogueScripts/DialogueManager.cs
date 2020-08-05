using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Text nameText;
	public Text dialogueText;

    public GameObject dialogueContainer;
    private bool isOpen;

    public GameObject tutorial;


    private Queue<string> sentences;
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        Debug.Log("Dialogue Manager starting up");
        Hide();
        
    }

    void Update(){
        if(isOpen){
            if(Input.GetKeyDown("return")){
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue){
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;
        isOpen = true;
        Show();

        sentences.Clear();

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        Debug.Log(sentences.Count);
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentences.Count);
        StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
        // Debug.Log(sentence);
    }

    IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

    void EndDialogue(){
        isOpen = false;
        Debug.Log("End of conversation");
        Hide();
        if(tutorial != null){
            tutorial.SetActive(true);
        }

    }

    void Hide() {
        dialogueContainer.SetActive(false);
        // dialogueContainer.alpha = 0f; //this makes everything transparent
        // dialogueContainer.interactable = false; //this prevents further interactions
        // dialogueContainer.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void Show() {
        dialogueContainer.SetActive(true);
        // dialogueContainer.alpha = 1f;
        // dialogueContainer.interactable = true;
        // dialogueContainer.blocksRaycasts = true;
    }
}
