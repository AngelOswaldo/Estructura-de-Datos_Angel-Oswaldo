using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{
    //Lista de enemigos y spawn points
    public List<GameObject> enemyPrefabs;
    public List<Transform> enemySpawnPoints;

    //Componetes de los enemigos y sus turnos
    public List<Unit> enemyUnits;

    //Componentes del ugador
    private Unit playerUnit;
    private PlayerController player;

    //Botones
    private Button attackButton, healButton;

    //Objeto donde spawnean los enemigos
    public Transform enemyParent;

    //Estados del combate y mensajes
    public BattleState state;
    private UIEvents myEvents;

    public bool isNeutral;

    private void Awake()
    {
        //Buscamos las referencias del jugador
        if (player && playerUnit == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            player = playerObject.GetComponent<PlayerController>();
            playerUnit = playerObject.GetComponent<Unit>();
        }

        if (myEvents == null)
        {
            myEvents = GameObject.FindGameObjectWithTag("SecondCamera").GetComponent<UIEvents>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el jugador entra a una habitacion comienza el combate
        if (collision.gameObject.tag == "Player")
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            player = playerObject.GetComponent<PlayerController>();
            player.canMove = false;

            StartRoom();
        }
    }

    private void StartRoom()
    {
        //Iniciamos el combate y bloqueamos el movimiento del jugador
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerController>();
        player.canMove = false;
    }

    private IEnumerator SetUpBattle()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerController>();
        player.canMove = false;

        //Iniciamos la habitacion spawneando a los enemigos
        playerUnit = playerObject.GetComponent<Unit>();

        
        int randomPoint;
        int randomEnemies = Random.Range(1, 5);

        for (int i = 0; i < randomEnemies; i++)
        {
            randomPoint = Random.Range(0, enemySpawnPoints.Count-1);
            Vector3 spawnPoint = new Vector3(enemySpawnPoints[randomPoint].position.x, enemySpawnPoints[randomPoint].position.y, enemySpawnPoints[randomPoint].position.z);

            GameObject newEnemy = Instantiate(enemyPrefabs[0], spawnPoint, Quaternion.identity);
            enemySpawnPoints.RemoveAt(randomPoint);

            enemyUnits.Add(newEnemy.GetComponent<Unit>());
            newEnemy.transform.SetParent(enemyParent);

        }

        yield return new WaitForSeconds(2f);
        InitTurn();

    }

    private void InitTurn() 
    {
        //Decidimos quien empezara el combate
        int randomTurn = Random.Range(1, 2);
        if (randomTurn == 1)
        {
            state = BattleState.PLAYERTURN;
            myEvents.UpdateEvents(playerUnit.unitName + " empieza el combate.");
            PlayerTurn();
        }
        else if (randomTurn == 2)
        {
            state = BattleState.ENEMYTURN;
            myEvents.UpdateEvents(enemyUnits[0].unitName + " empieza el combate.");
            StartCoroutine(EnemyTurn());
        }
    }

    private void PlayerTurn()
    {
        Debug.Log("Jugador Turno");
        myEvents.UpdateEvents("Escoge una accion.");
        ActiveButtons();
    }

    private void ActiveButtons()
    {
        Debug.Log("Botones Activados");
        if (attackButton == null && healButton == null)
        {
            attackButton = GameObject.Find("Attack").GetComponent<Button>();
            healButton = GameObject.Find("Healing").GetComponent<Button>();

            attackButton.onClick.AddListener(OnAttackButton);
            healButton.onClick.AddListener(OnHealingButton);
        }
        
        attackButton.interactable = true;
        healButton.interactable = true;

    }

    private void DisableButtons()
    {
        Debug.Log("Botones desactivados");
        if (attackButton == null && healButton == null)
        {
            attackButton = GameObject.Find("Attack").GetComponent<Button>();
            healButton = GameObject.Find("Healing").GetComponent<Button>();

            attackButton.onClick.AddListener(OnAttackButton);
            healButton.onClick.AddListener(OnHealingButton);
        }

        attackButton.interactable = false;
        healButton.interactable = false;
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealingButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHealing());
    }

    private IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnits[0].TakeDamage(playerUnit.damage);
        myEvents.UpdateEvents(playerUnit.unitName + " ataco.");
        yield return new WaitForSeconds(1f);
        
        if(isDead)
        {
            enemyUnits[0].gameObject.SetActive(false);
            myEvents.UpdateEvents(playerUnit.unitName + " elimino un " + enemyUnits[0].unitName + ".");
            enemyUnits.RemoveAt(0);

            if (enemyUnits.Count<=0)
            {
                state = BattleState.WON;
                EndBattle();
            }

        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator PlayerHealing()
    {
        DisableButtons();
        myEvents.UpdateEvents(playerUnit.unitName + " recupero vida.");
        playerUnit.Heal(playerUnit.heal);

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        DisableButtons();
        myEvents.UpdateEvents("Zombie" + " ataca.");

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(3);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        //Checamos si el jugador gano o perdio
        if(state==BattleState.WON)
        {
            myEvents.UpdateEvents("¡Acabaste con los enemigos!");
            player.canMove = true;
            gameObject.SetActive(false);
        }
        else if(state==BattleState.LOST)
        {
            myEvents.UpdateEvents("¡Los enemigos te han derrotado!");
            player.gameObject.SetActive(false);
        }
    }

}
