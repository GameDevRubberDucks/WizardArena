using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerHealth : MonoBehaviour
{
    //--- Public Variables ---//
    public Image m_imgHealthFill;
    public TextMeshProUGUI m_txtHealthLabel;
    public GameObject m_txtGameOver;



    //--- Private Variables ---//
    private Player_Health m_playerHealth;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_playerHealth = FindObjectOfType<Player_Health>();
    }

    private void Update()
    {
        // Grab the current player health
        float currentHealth = m_playerHealth.currentHealth;
        float maxHealth = m_playerHealth.maxHealth;
        float healthPercentage = currentHealth / maxHealth;

        // Update the UI elements
        m_imgHealthFill.fillAmount = healthPercentage;
        m_txtHealthLabel.text = currentHealth.ToString("F0") + " / " + maxHealth.ToString("F0");

        // If the health dropped below 0, show the game over text
        if (healthPercentage <= 0.0f)
            m_txtGameOver.SetActive(true);
    }
}
