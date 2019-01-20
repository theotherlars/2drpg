using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResponsButton : MonoBehaviour
{
    [SerializeField]
    private TextHandler textHandler;
    public int nextIndex;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    public void HandleClick()
    {
        textHandler.LoadText(nextIndex);
    }
}
