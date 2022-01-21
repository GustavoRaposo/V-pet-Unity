using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    [Header("Health Bar")]

    [SerializeField] [Range(0, 1)] private float highHealthRange = .5f;
    [SerializeField] [Range(0, 1)] private float midHealthRange = .25f;
    [SerializeField] private Color colorHighHealth = Color.green;
    [SerializeField] private Color colorMidHealth = Color.yellow;
    [SerializeField] private Color colorLowHealth = Color.red;


    [Header("UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Text textCurrentHealth;
    [SerializeField] private Text textMaxHealth;

    public int CurrentHealth
    {
        get => currentHealth;
        set 
        {
            currentHealth = value;
            StartCoroutine("UpdateHealthBarCoroutine");
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set 
        {
            maxHealth = value;
        }
    }

    public void Heal(int health)
    {
        CurrentHealth += health;
    }

    public void Damange(int health)
    {
        CurrentHealth -= health;
    }

    private void Start()
    {
        StartCoroutine("UpdateHealthBarCoroutine");
    }

    private IEnumerator UpdateHealthBarCoroutine()
    {
        textCurrentHealth.text = CurrentHealth.ToString();
        textMaxHealth.text = MaxHealth.ToString();

        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;

        SetHealthBarColor(healthPercentage);

        yield return null;
    }

    private void SetHealthBarColor(float healthPercentage)
    {
        if (healthPercentage > highHealthRange)
            healthBar.color = colorHighHealth;
        else if (healthPercentage > midHealthRange)
            healthBar.color = colorMidHealth;
        else
            healthBar.color = colorLowHealth;
    }
}
