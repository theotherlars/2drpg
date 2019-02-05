using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorText : MonoBehaviour
{
    private TextMeshProUGUI text;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<TextMeshProUGUI>();
        text.enabled = false;
    }
    public void DisplayText(string inputText)
    {
        text.enabled = true;
        text.text = inputText;
        animator.SetBool("open", true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("open", false);
    }


}
