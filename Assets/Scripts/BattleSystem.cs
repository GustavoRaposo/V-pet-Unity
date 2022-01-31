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
        public GameObject playerPrefab;
        public GameObject enemyPrefab;

        public Transform playerBattleStation;
        public Transform enemyBattleStation;

        Unit playerUnit;
        Unit enemyUnit;

        public Text battleLogText;

        public BattleHUD playerHUD;

        public BattleHUD enemyHUD;
        public MoveHUD moveHUD;

        private BattleState state;
        public BattleState State
        {
            get => state;
            set
            {
                if (value != state)
                {
                    state = value;
                    OnChanged?.Invoke(state);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            State = BattleState.Start;
            StartCoroutine("SetupBattle");
        }

        void OnEnable()
        {
            moveHUD.OnClickMove += PreCombat2;
        }

        void OnDisable()
        {
            moveHUD.OnClickMove -= PreCombat2;
        }

        private IEnumerator SetupBattle()
        {
            GameObject playerGameObject = Instantiate(playerPrefab, playerBattleStation);
            playerUnit = playerGameObject.GetComponent<Unit>();
            GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleStation);
            enemyUnit = enemyGameObject.GetComponent<Unit>();

            battleLogText.text = "New Battle!\n" + playerUnit.stats.unitName + " VS " + enemyUnit.stats.unitName + "\nFight!";

            playerHUD.setHUD(playerUnit);
            enemyHUD.setHUD(enemyUnit);

            yield return new WaitForSeconds(2);

            PreCombat();
        }

        private void PreCombat()
        {
            State = BattleState.PreCombat;
            // TODO: Show options
            moveHUD.Show(playerUnit.moves);
            battleLogText.text = "";

        }

        private void PreCombat2(CharMove playerMove)
        {
            Debug.Log("PreCombat2");
            moveHUD.Hide();

            // TODO: Decide enemyes choice
            CharMove enemyMove = enemyUnit.moves[Random.Range(0, enemyUnit.moves.Length)];
            StartCoroutine(Combat(playerMove, enemyMove));
        }

        private IEnumerator Combat(CharMove playerMove, CharMove enemyMove)
        {
            State = BattleState.Combat;

            Debug.Log($"Player={playerMove.moveName} | Enemy={enemyMove.moveName}");

            // Decide who attacks first
            TurnMove playerTurnMove = new TurnMove(ref playerMove, ref playerUnit, ref enemyUnit);
            TurnMove enemyTurnMove = new TurnMove(ref enemyMove, ref enemyUnit, ref playerUnit);
            
            TurnMove[] turnMoves;
            if (playerUnit.stats.speed > enemyUnit.stats.speed)
            {
                turnMoves = new TurnMove[] { playerTurnMove, enemyTurnMove };
            }
            else if (playerUnit.stats.speed < enemyUnit.stats.speed)
            {
                turnMoves = new TurnMove[] { enemyTurnMove, playerTurnMove };
            }
            else
            {
                int coin = Random.Range(0, 1);
                if (coin == 0)
                {
                    turnMoves = new TurnMove[] { playerTurnMove, enemyTurnMove };
                }
                else
                {
                    turnMoves = new TurnMove[] { enemyTurnMove, playerTurnMove };
                }
            }


            // Movement loop
            foreach (TurnMove move in turnMoves)
            {
                // TODO: Apply choosen move
                move.Execute();
                // TODO: Update HUD (HP, etc)
                playerHUD.setHP(playerUnit);
                enemyHUD.setHP(enemyUnit);
                
                battleLogText.text = $"{move.attacker.stats.unitName} usou '{move.move.moveName}'!";
                yield return new WaitForSeconds(2);

                // TODO: Test if plaer or enemy is alive
                if(move.defender.currentHP <= 0){
                    BattleOver(move.attacker);
                    yield break;
                }
                
            }

            PreCombat();
        }

        private void BattleOver(Unit winner)
        {
            State = BattleState.End;

            // TODO: Update HUD
            battleLogText.text = $"Parabéns {winner.stats.unitName}, você venceu meu consagrado!\n \\(>w<)/";
        }
    }

    public class TurnMove 
    {
        public CharMove move;
        public Unit attacker;
        public Unit defender;

        public TurnMove(ref CharMove move, ref Unit attacker, ref Unit defender)
        {
            this.move = move;
            this.attacker = attacker;
            this.defender = defender;
        }

        public void Execute()
        {
            move.Execute(ref attacker, ref defender);
        }
    }
}
