using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheet : MonoBehaviour
{
    //Enemy Class
    public string enemyName;
    public int level;
    private CharacterAbilities abilities;
    //Basic stats
    private int maxHP;
    public int currentHP;
    //Other stats
    public int damage;
    public int magicDamage;
    public int intiative;

    private void Awake()
    {
        abilities = GetComponent<CharacterAbilities>();
        InitEnemy(enemyName, abilities);
    }

    private void InitEnemy(string enemyName, CharacterAbilities abilities)
    {
        if (enemyName == "Zombie")
        {
            maxHP = 10 + abilities.constitution;
            currentHP = maxHP;

            damage = 5 + abilities.athletics;
            intiative = abilities.athletics;

            magicDamage = 3 + abilities.magic;

        }
        else if (enemyName == "Skeleton")
        {
            maxHP = 5 + abilities.constitution;
            currentHP = maxHP;

            damage = 3 + abilities.athletics;
            intiative = abilities.athletics;

            magicDamage = 5 + abilities.magic;
        }
    }
}
