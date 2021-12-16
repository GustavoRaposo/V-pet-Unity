using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleSate{START,PLAYERTURN,ENEMYTURN,WON,LOST}
public class BattleSystem : MonoBehaviour
{
    public BattleSate state;
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
        state = BattleSate.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGameObject = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGameObject.GetComponent<Unit>();
        GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGameObject.GetComponent<Unit>();

        battleLogText.text = "New Battle!\n" + playerUnit.unitName + " VS " + enemyUnit.unitName + "\nFight!";

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);
    }
}
