using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public int heal;

    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        if (gameObject.tag == "Player")
        {
            int random = Random.Range(1, 3);
            if (random == 1)
            {
                InitFighter();
            }
            else
            {
                InitWizard();
            }
        }
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }

    public void Heal(int heal)
    {
        currentHP += heal;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    private void InitFighter()
    {
        maxHP = 15;
        currentHP = 15;

        damage = 5;
        heal = 3;
    }

    private void InitWizard()
    {
        maxHP = 10;
        currentHP = 10;

        damage = 3;
        heal = 5;
    }
}
