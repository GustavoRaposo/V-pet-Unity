using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace vpet
{
    public class BattleHUD : MonoBehaviour
    {
        public Text nameText;
        public Text levelText;
        public HealthHUD healthBar;
        public Text spText;
        public Image image;

        void OnEnable()
        {
            BattleSystem.OnChanged += StateChanged;
        }

        void OnDisable()
        {
            BattleSystem.OnChanged -= StateChanged;
        }

        private void StateChanged(BattleState state)
        {
            Debug.Log($"State changed: {state}");
        }

        int maxHP;
        public void setHUD(Unit unit)
        {
            nameText.text = unit.stats.unitName;
            levelText.text = "Lvl " + unit.level;
            image.sprite = unit.sprite;
            healthBar.MaxHealth = unit.MaxHP;
            healthBar.CurrentHealth = unit.currentHP;
            // spText.text = (unit.specialCharge * 100) + "%";
        }

        public void setHP(Unit unit)
        {
            healthBar.CurrentHealth = unit.currentHP;
        }

        public void setSp(float sp)
        {
            spText.text = (sp * 100) + "%";
        }
    }
}