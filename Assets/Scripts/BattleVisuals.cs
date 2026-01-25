using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleVisuals : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    [SerializeField] private TextMeshProUGUI levelText;

    private int currHealth;
    private int maxHealth;
    private int level;

    private Animator anim;


    private const string LEVEL_ABB = "Lvl: ";

    private const string IS_ATTACK_PARAM = "IsAttack";
    private const string IS_HIT_PARAM = "IsHit";
    private const string IS_DEAD_PARAM = "IsDead";
    private const string IS_SPECIALATTACK_PARAM = "IsSpecialAttack";
    private const string IS_HEAL_PARAM = "IsHeal";
    private const string IS_BLOCK_PARAM = "IsBlock";
    private const string IS_RUN_PARAM = "IsRun";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void SetStartingValues(int currHealth, int maxHealth, int level)
    {
        this.currHealth = currHealth;
        this.maxHealth = maxHealth;
        this.level = level;
        levelText.text = LEVEL_ABB + this.level.ToString();

        UpdateHealthBar();
    }

    public void ChangeHealth(int currHealth)
    {
        this.currHealth = currHealth;

        if (currHealth <= 0)
        {
            PlayDeathAnimation();
            Destroy(gameObject, 1f);
        }
        UpdateHealthBar(); 
    }

    public void UpdateHealthBar()
    {
        healthbar.maxValue = maxHealth;
        healthbar.value = currHealth;


    }

    public void PlayAttackAnimation()
    {
        anim.SetTrigger(IS_ATTACK_PARAM);
    }

    public void PlayHitAnimation()
    {
        anim.SetTrigger(IS_HIT_PARAM);
    }

    public void PlayDeathAnimation()
    {
        anim.SetTrigger(IS_DEAD_PARAM);
    }

    public void PlaySpecialAttackAnimation()
    {
        anim.SetTrigger(IS_SPECIALATTACK_PARAM);
    }

    public void PlayHealAnimation()
    {
        anim.SetTrigger(IS_HEAL_PARAM);
    }

    public void PlayBlockAnimation()
    {
        anim.SetTrigger(IS_BLOCK_PARAM);
    }

    public void PlayRunAnimation()
    {
        anim.SetTrigger(IS_RUN_PARAM);
    }
}
