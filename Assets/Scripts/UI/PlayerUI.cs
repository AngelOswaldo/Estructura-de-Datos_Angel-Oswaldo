using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text playerName;
    public Text textHP;
    public Slider healthBar;

    public Unit playerUnit;

    private void Start()
    {
        //InitUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void InitUI()
    {
        playerName.text = playerUnit.unitName;

        healthBar.maxValue = playerUnit.maxHP;
        healthBar.value = playerUnit.currentHP;
        textHP.text = playerUnit.currentHP.ToString() + "/" +  playerUnit.maxHP.ToString() ;
    }

    public void UpdateUI()
    {
        healthBar.value = playerUnit.currentHP;
        textHP.text = playerUnit.currentHP.ToString() + "/" + playerUnit.maxHP.ToString();
    }
}
