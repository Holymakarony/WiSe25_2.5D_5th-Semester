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

    [Header("Battle Sounds")]
    public AudioClip Arrow;
    public AudioClip FemaleDeath;
    public AudioClip FemalePain;
    public AudioClip GnomeDeath1;
    public AudioClip GnomeDeath2;
    public AudioClip GnomeDeath3;
    public AudioClip MagicFire1;
    public AudioClip MagicFire2;
    public AudioClip MagicFire3;
    public AudioClip MaleDeath1;
    public AudioClip MaleDeath2;
    public AudioClip MalePain;
    public AudioClip ShieldBlock;
    public AudioClip SlimeAttack;
    public AudioClip SlimeHit;
    public AudioClip SwordHit;
    public AudioClip Heal;

    [SerializeField] private AudioSource _audioSource;

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

    public void PlayArrow()
    {
        _audioSource.PlayOneShot(Arrow);
    }
    public void PlayFemaleDeath()
    {
        _audioSource.PlayOneShot(FemaleDeath);
    }
    public void PlayFemalePain()
    {
        _audioSource.PlayOneShot(FemalePain);
    }
    public void PlayGnomeDeath1()
    {
        _audioSource.PlayOneShot(GnomeDeath1);
    }
    public void PlayGnomeDeath2()
    {
        _audioSource.PlayOneShot(GnomeDeath2);
    }
    public void PlayGnomeDeath3()
    {
        _audioSource.PlayOneShot(GnomeDeath3);
    }
    public void PlayMagicFire1()
    {
        _audioSource.PlayOneShot(MagicFire1);
    }
    public void PlayMagicFire2()
    {
        _audioSource.PlayOneShot(MagicFire2);
    }
    public void PlayMagicFire3()
    {
        _audioSource.PlayOneShot(MagicFire3);
    }
    public void PlayMaleDeath1()
    {
        _audioSource.PlayOneShot(MaleDeath1);
    }
    public void PlayMaleDeath2()
    {
        _audioSource.PlayOneShot(MaleDeath2);
    }
    public void PlayMalePain()
    {
        _audioSource.PlayOneShot(MalePain);
    }
    public void PlayShieldBlock()
    {
        _audioSource.PlayOneShot(ShieldBlock);
    }
    public void PlaySlimeAttack()
    {
        _audioSource.PlayOneShot(SlimeAttack);
    }
    public void PlaySlimeHit()
    {
        _audioSource.PlayOneShot(SlimeHit);
    }
    public void PlaySwordHit()
    {
        _audioSource.PlayOneShot(SwordHit);
    }
    public void PlayHeal()
    {
        _audioSource.PlayOneShot(Heal);
    }
}
