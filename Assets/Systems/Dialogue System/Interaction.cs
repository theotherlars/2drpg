using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    [SerializeField]
    private GameObject interactionText; //Only Temp

    private TextHandler textHandler;
    public Dialogue dialogue;
    public GameObject dialoguePanel;

    private bool isCollidingWithPlayer;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && isCollidingWithPlayer)
        {
            interactionText.SetActive(false);
            InitiateDialogue();
        }
    }

    private void Awake()
    {
        dialoguePanel.SetActive(false);
        textHandler = dialoguePanel.GetComponent<TextHandler>();
    }

    public void InitiateDialogue()
    {
        dialoguePanel.SetActive(true);
        textHandler.LoadDialogue(dialogue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionText.SetActive(true);
            isCollidingWithPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isCollidingWithPlayer = false;
        }
    }
}
