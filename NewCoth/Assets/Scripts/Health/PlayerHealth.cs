using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : HealthBase
{
    [SerializeField] private CanvasGroup hurtImg;
    [SerializeField] private float hurtImgFadeTime;

    [Header("Health Bar")]
    public Image healthBar;
    public override void Death()
    {
        base.Death();
    }
    public override void Start()
    {
        base.Start();
        UdpateHealthBar();
    }

    public override void Hurt()
    {
        base.Hurt();
        hurtImg.alpha = 1.0f;
        hurtImg.DOFade(0, hurtImgFadeTime);
        UdpateHealthBar();
    }

    public override void Takedamage(float damageAmount)
    {
        base.Takedamage(damageAmount);
    }

    public void UdpateHealthBar()
    {
        healthBar.fillAmount = CurrentHealth() / healthData.maxHealth;
    }
}
