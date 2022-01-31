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

    [Header("Animation")]
    [SerializeField] private float animationTime = 1;
    [SerializeField] private AnimationCurve barAnimationCurve;

    [Header("UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image healthBarMask;
    [SerializeField] private Text textCurrentHealth;
    [SerializeField] private Text textMaxHealth;

    private Animator anim;

    public int CurrentHealth
    {
        get => currentHealth;
        set 
        {
            if (value != currentHealth)
            {
                if (value < 0)
                    currentHealth = 0;
                else if (value > MaxHealth)
                    currentHealth = MaxHealth;
                else
                    currentHealth = value;

                anim.SetTrigger("healthChanged");
                StopAllCoroutines();
                StartCoroutine("UpdateHealthBarCoroutine");
            }
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set 
        {
            if (value < 0)
                maxHealth = 0;
            else
                maxHealth = value;

            if (maxHealth < currentHealth)
                CurrentHealth = maxHealth;
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

    public void SetHealth(string health)
    {
        CurrentHealth = int.Parse(health);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        healthBarMask.fillAmount = 1;
        StartCoroutine("UpdateHealthBarCoroutine");
    }

    private IEnumerator UpdateHealthBarCoroutine()
    {
        textCurrentHealth.text = CurrentHealth.ToString();
        textMaxHealth.text = MaxHealth.ToString();

        float healthPercentage = (float)currentHealth / maxHealth;
        
        float currentAnimationTime = 0;
        float fillAmount = healthBarMask.fillAmount;

        do
        {
            float time = barAnimationCurve.Evaluate(currentAnimationTime / animationTime);
            healthBarMask.fillAmount = Mathf.Lerp(fillAmount, healthPercentage, time);
            SetHealthBarColor(healthBarMask.fillAmount);
            yield return null;
            currentAnimationTime += Time.deltaTime;
        } while (healthBarMask.fillAmount != healthPercentage);
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
