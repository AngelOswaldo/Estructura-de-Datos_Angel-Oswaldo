using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    //Character Class
    public string nameClass;
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
        InitClass(nameClass, abilities);
    }

    private void InitClass(string nameClass, CharacterAbilities abilities)
    {
        if(nameClass=="Fighter")
        {
            maxHP = 10 + abilities.constitution;
            currentHP = maxHP;

            damage = 5 + abilities.athletics;
            intiative = abilities.athletics;

            magicDamage = 3 + abilities.magic;

        }
        else if(nameClass=="Wizard")
        {
            maxHP = 5 + abilities.constitution;
            currentHP = maxHP;

            damage = 3 + abilities.athletics;
            intiative = abilities.athletics;

            magicDamage = 5 + abilities.magic;
        }
    }
}
