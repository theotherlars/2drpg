using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    PlayerController playerController;
    public GameObject inventoryPanel;
    public Text playerHP;

    public GameObject deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        deathMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ToggleInventoryUI();
        playerHP.text = "HP: " + playerController.player_HealthPoints.ToString();
    }

    private void ToggleInventoryUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.gameObject.activeSelf);
        }
    }

    private void FixedUpdate()
    {
        if (playerController.isDead)
        {
            deathMenu.SetActive(true);
        }
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
