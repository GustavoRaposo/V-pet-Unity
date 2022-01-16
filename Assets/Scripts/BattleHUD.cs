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
        public Text hpText;
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
            hpText.text = unit.currentHP + "/" + unit.MaxHP;
            // spText.text = (unit.specialCharge * 100) + "%";
        }

        public void setHP(Unit unit)
        {
            hpText.text = unit.currentHP + "/" + unit.MaxHP;
        }

        public void setSp(float sp)
        {
            spText.text = (sp * 100) + "%";
        }
    }
}