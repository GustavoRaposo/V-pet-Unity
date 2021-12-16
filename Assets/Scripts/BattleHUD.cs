using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Text hpText;
    public Text spText;
    public Image image;

    int maxHP;
    public void setHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        image = GetComponent<Image>();
        image.sprite = unit.unitUIImage;
        hpText.text = unit.currentHP + "/" + unit.maxHP;
        maxHP = unit.maxHP;
        spText.text = (unit.specialCharge * 100) + "%";
    }

    public void setHP(int hp)
    {
        hpText.text = hp + "/" + maxHP;
    }

    public void setSp(float sp)
    {
        spText.text = (sp * 100) + "%";
    }
}
