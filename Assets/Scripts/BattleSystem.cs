using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace vpet
{
    public class BattleSystem : MonoBehaviour
    {
        public static event UnityAction<BattleState> OnChanged;      
        public BattleState state;
        public GameObject playerPrefab;
        public GameObject enemyPrefab;

        public Transform playerBattleStation;
        public Transform enemyBattleStation;

        Unit playerUnit;
        Unit enemyUnit;

        public Text battleLogText;

        public BattleHUD playerHUD;

        public BattleHUD enemyHUD;

        // Start is called before the first frame update
        void Start()
        {
            state = BattleState.START;
            OnChanged?.Invoke(state);
            SetupBattle();
        }

        void SetupBattle()
        {
            GameObject playerGameObject = Instantiate(playerPrefab, playerBattleStation);
            playerUnit = playerGameObject.GetComponent<Unit>();
            GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleStation);
            enemyUnit = enemyGameObject.GetComponent<Unit>();

            //battleLogText.text = "New Battle!\n" + playerUnit.unitName + " VS " + enemyUnit.unitName + "\nFight!";

            playerHUD.setHUD(playerUnit);
            enemyHUD.setHUD(enemyUnit);
        }

        void Damage(Unit attackerPet, Unit defenderPet, DamageMove move)
        {
            //todo: add new attribute to the current hp
            if (move.type == MoveType.Physical)
            {
                defenderPet.stats.hp = defenderPet.stats.hp - ((move.power * attackerPet.stats.force) / defenderPet.stats.physicalResistence);
            }
            else if (move.type == MoveType.Magical)
            {
                defenderPet.stats.hp = defenderPet.stats.hp - ((move.power * attackerPet.stats.intelligence) / defenderPet.stats.magicResistence);
            }
        }

        void Heal(Unit pet, HealMove move)
        {
            //todo: add new attribute to the current hp
            pet.stats.hp = pet.stats.hp + move.power * pet.stats.intelligence;
            //todo: verify if the heal exceed the  max hp
        }
    }
}
