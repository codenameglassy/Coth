using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaManager : MonoBehaviour
{
    public static PlayerManaManager instance;

    [Header("Mana Bar")]
    public Image manaBar;

    [Header("Mana")]
    public float maxMana;
    public float startingMana;
    private float currentMana;
    public bool isUsingMana;
    public float manaCost;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentMana = startingMana;
        UdpateManaBar();
    }

    private void FixedUpdate()
    {
        if (isUsingMana)
        {
            RemoveMana(0.5f);
        }
        else
        {
            AddMana(0.5f);
        }
    }

    public void AddMana(float addAmount)
    {
        
        if (currentMana >= maxMana)
        {
            currentMana = maxMana;
            return;
        }

        currentMana += addAmount;
        UdpateManaBar();
    }

    public void RemoveMana(float removeAmount)
    {
        currentMana -= removeAmount;
        if(currentMana <= 0)
        {
            currentMana = 0;
        }
        UdpateManaBar();
    }

    public float CurrentMana()
    {
        return currentMana;
    }

    public void UdpateManaBar()
    {
        manaBar.fillAmount = currentMana / maxMana;
    }
}
