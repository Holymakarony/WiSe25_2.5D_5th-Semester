using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private enum BattleState {Start, Selection, Battle, Won, Lost, Run}
    [Header("Battle State")]
    [SerializeField] private BattleState state;
    [Header("Spawn Points")]
    [SerializeField] private Transform[] partySpawnPoints;
    [SerializeField] private Transform[] enemySpawnPoints;
    [Header("Battlers")]
    [SerializeField] private List<BattleEntities> allBattlers = new List<BattleEntities>();
    [SerializeField] private List<BattleEntities> enemyBattlers = new List<BattleEntities>();
    [SerializeField] private List<BattleEntities> playerBattlers = new List<BattleEntities>();
    [Header("UI")]
    [SerializeField] private GameObject[] enemySelectionButtons;
    [SerializeField] private GameObject[] enemySpecialSelectionButtons;
    [SerializeField] private GameObject battleMenu;
    [SerializeField] private GameObject enemySelectionMenu;
    [SerializeField] private GameObject enemySpecialSelectionMenu;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private GameObject bottomTextPopUp;
    [SerializeField] private TextMeshProUGUI bottomText;


    private PartyManager partyManager;
    private EnemeyManager enemyManager;
    private int currentPlayer;

    private const string ACTION_MESSAGE = "'s Aktion:";
    private const string WIN_MESSAGE = "Dein Team hat den Kampf gewonnen!";
    private const string LOSE_MESSAGE = "Dein Team wurde besiegt!";
    private const string RUN_MESSAGE_SUCCES = "Du bist davongelaufen!";
    private const string RUN_MESSAGE_FAIL = "Du kannst jetzt nicht wegrennen...";
    private const int TURN_DURATION = 2;
    private const int RUN_CHANCE = 50;
    private const string OVERWORLD_SCENE = "OverworldScene";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();
        enemyManager = GameObject.FindFirstObjectByType<EnemeyManager>();

        CreatePartyEntitites();
        CreateEnemyEntities();
        ShowBattleMenu();
        DetermineBattleOrder();
    }
    
    private IEnumerator BattleRoutine()
    {
        enemySelectionMenu.SetActive(false); // enemy selection menu disabled
        enemySpecialSelectionMenu.SetActive(false); // enemy special selection menu disabled
        state = BattleState.Battle; // change our state to the battle state
        bottomTextPopUp.SetActive(true); // enable our bottom text
        DetermineBattleOrder();
        // loop though all battlers
            //-> do appropiate action
        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (state == BattleState.Battle && allBattlers[i].CurrHealth > 0){
                switch (allBattlers[i].BattleAction)
                {
                    case BattleEntities.Action.Attack:
                        yield return StartCoroutine(AttackRoutine(i));
                        break;
                    case BattleEntities.Action.Run:
                        yield return StartCoroutine(RunRoutine());
                        break;
                    case BattleEntities.Action.Heal:
                        yield return StartCoroutine(HealRoutine(i));
                        break;
                    case BattleEntities.Action.SpecialAttack:
                        yield return StartCoroutine(SpecialAttackRoutine(i));
                        break;
                    case BattleEntities.Action.Block:
                        yield return StartCoroutine(BlockRoutine(i));
                        break;
                    default:
                        Debug.Log("Error - incorrect battle action");
                        break;
                }
            }
        }

        RemoveDeadBattlers();

        if(state == BattleState.Battle)
        {
            bottomTextPopUp.SetActive(false);
            currentPlayer = 0;
            ShowBattleMenu();

            for (int i = 0; i < allBattlers.Count; i++)
            {
                allBattlers[i].IsBlocking = false;
            }
        }

        yield return null;
        // if we havent won or lost, repeat loop by opening battle menu
    }

    private IEnumerator AttackRoutine(int i)
    {
        // players turn
        if (allBattlers[i].IsPlayer == true)
        {
            BattleEntities currAttacker = allBattlers[i];
            if (allBattlers[currAttacker.Target].CurrHealth <= 0)
            {
                currAttacker.SetTarget(GetRandomEnemy());
            }

            BattleEntities currTarget = allBattlers[currAttacker.Target];
            
            AttackAction(currAttacker, currTarget); // attack selected enemy (attack action)
            yield return new WaitForSeconds(TURN_DURATION); // wait
            // kill enemy
            if (currTarget.CurrHealth <= 0)
            {
                bottomText.text = string.Format("{0} hat {1} besiegt.", currAttacker.Name, currTarget.Name);
                yield return new WaitForSeconds(TURN_DURATION); // wait
                enemyBattlers.Remove(currTarget);
                FindFirstObjectByType<QuestManager>().CheckQuestStatus(currTarget.Name);
            }
            // if no enemies remain -> won
            if (enemyBattlers.Count <= 0)
            {
                state = BattleState.Won;
                bottomText.text = WIN_MESSAGE;
                yield return new WaitForSeconds(TURN_DURATION); // wait
                SceneManager.LoadScene(OVERWORLD_SCENE);
            }
        }

        // enemies turn
        if (i < allBattlers.Count && allBattlers[i].IsPlayer == false)
        {
            BattleEntities currAttacker = allBattlers[i];
            currAttacker.SetTarget(GetRandomPartyMember()); // get random party member -> target
            BattleEntities currTarget = allBattlers[currAttacker.Target];
            
            AttackAction(currAttacker, currTarget); // attack selected party member (attack action)
            yield return new WaitForSeconds(TURN_DURATION); // wait
            // kill party member
            if(currTarget.CurrHealth <= 0)
            {
                bottomText.text = string.Format("{0} hat {1} besiegt.", currAttacker.Name, currTarget.Name);
                yield return new WaitForSeconds(TURN_DURATION); // wait
                playerBattlers.Remove(currTarget);

                if (playerBattlers.Count <= 0) // if no party members remain -> lost
                {
                    state = BattleState.Lost;
                    bottomText.text = LOSE_MESSAGE;
                    yield return new WaitForSeconds(TURN_DURATION); // wait
                    Debug.Log("Game Over!");
                    SceneManager.LoadScene(OVERWORLD_SCENE);
                    
                    
                }
            }
        }
    }

    private IEnumerator RunRoutine()
    {
        if (state == BattleState.Battle)
        {
            if (Random.Range(1, 101) >= RUN_CHANCE)
            {
                // running succesfull
                bottomText.text = RUN_MESSAGE_SUCCES;
                state = BattleState.Run;
                allBattlers.Clear();
                yield return new WaitForSeconds(TURN_DURATION);
                SceneManager.LoadScene(OVERWORLD_SCENE);
            }
            else
            {
                // running unsuccesfull
                bottomText.text = RUN_MESSAGE_FAIL;
                yield return new WaitForSeconds(TURN_DURATION); 
            }
        }
    }

    private IEnumerator HealRoutine(int i)
    {
        if (state == BattleState.Battle)
        {
            allBattlers[i].CurrHealth += 10;
            if (allBattlers[i].CurrHealth > allBattlers[i].MaxHealth)
            {
                allBattlers[i].CurrHealth = allBattlers[i].MaxHealth;
            }
            SaveHealth();
            allBattlers[i].UpdateUI();
            bottomText.text = string.Format("{0} hat sich um 10 Lebenspukte geheilt.", allBattlers[i].Name);
            yield return new WaitForSeconds(TURN_DURATION);

        }
    }

    private IEnumerator BlockRoutine(int i)
    {
        if (state == BattleState.Battle)
        {
            allBattlers[i].IsBlocking = true;
            bottomText.text = string.Format("{0} blockt den nächsten Angriff.", allBattlers[i].Name);
            yield return new WaitForSeconds(TURN_DURATION);
        }
    }

    private IEnumerator SpecialAttackRoutine(int i)
    {
        // players turn
        if (allBattlers[i].IsPlayer == true)
        {
            BattleEntities currAttacker = allBattlers[i];
            if (allBattlers[currAttacker.Target].CurrHealth <= 0)
            {
                currAttacker.SetTarget(GetRandomEnemy());
            }

            BattleEntities currTarget = allBattlers[currAttacker.Target];
            
            SpecialAttackAction(currAttacker, currTarget); // attack selected enemy (attack action)
            yield return new WaitForSeconds(TURN_DURATION); // wait
            // kill enemy
            if (currTarget.CurrHealth <= 0)
            {
                bottomText.text = string.Format("{0} hat {1} besiegt.", currAttacker.Name, currTarget.Name);
                yield return new WaitForSeconds(TURN_DURATION); // wait
                enemyBattlers.Remove(currTarget);
                FindFirstObjectByType<QuestManager>().CheckQuestStatus(currTarget.Name);
            }
            // if no enemies remain -> won
            if (enemyBattlers.Count <= 0)
            {
                state = BattleState.Won;
                bottomText.text = WIN_MESSAGE;
                yield return new WaitForSeconds(TURN_DURATION); // wait
                SceneManager.LoadScene(OVERWORLD_SCENE);
            }
        }
    }

    private void RemoveDeadBattlers()
    {
        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (allBattlers[i].CurrHealth <= 0)
            {
                allBattlers.RemoveAt(i);
            }
        }
    }

    private void CreatePartyEntitites()
    {
        List<PartyMember> currentParty = new List<PartyMember>();
        currentParty = partyManager.GetAliveParty();

        for (int i = 0; i < currentParty.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();

            tempEntity.SetEntitiesValues(currentParty[i].MemberName, currentParty[i].CurrentHealth, currentParty[i].MaxHealth, currentParty[i].Initiative, currentParty[i].Strenght, currentParty[i].Level, true);
            // spawn party member battle visuals
            BattleVisuals tempBattleVisuals = Instantiate(currentParty[i].MemberBattleVisualPrefab, partySpawnPoints[i].position, Quaternion.identity).GetComponent<BattleVisuals>();
            // set starting values
            tempBattleVisuals.SetStartingValues(currentParty[i].CurrentHealth, currentParty[i].MaxHealth, currentParty[i].Level);
            // assign new party member entity to battlers + playerBattlers
            tempEntity.BattleVisuals = tempBattleVisuals;

            allBattlers.Add(tempEntity);
            playerBattlers.Add(tempEntity);
        }
    }

    private void CreateEnemyEntities()
    {
        List<Enemy> currentEnemies = new List<Enemy>();
        currentEnemies = enemyManager.GetCurrentEnemies();

        for (int i = 0; i < currentEnemies.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();

            tempEntity.SetEntitiesValues(currentEnemies[i].EnemyName, currentEnemies[i].CurrentHealth, currentEnemies[i].MaxHealth, currentEnemies[i].Initiative, currentEnemies[i].Strenght, currentEnemies[i].Level, false);
            // spawn enemy battle visuals
            BattleVisuals tempBattleVisuals = Instantiate(currentEnemies[i].EnemyBattleVisualPrefab, enemySpawnPoints[i].position, Quaternion.identity).GetComponent<BattleVisuals>();
            // set starting values
            tempBattleVisuals.SetStartingValues(currentEnemies[i].MaxHealth, currentEnemies[i].MaxHealth, currentEnemies[i].Level);
            // asign new enemy entity to battle entity
            tempEntity.BattleVisuals = tempBattleVisuals;

            allBattlers.Add(tempEntity);
            enemyBattlers.Add(tempEntity);
        }
    }

    public void ShowBattleMenu()
    {
        // set action text according to which party member's turn it is + enable battle menu
        actionText.text = playerBattlers[currentPlayer].Name + ACTION_MESSAGE;
        battleMenu.SetActive(true);
    }

    public void ShowEnemySelectionMenu()
    {
        // disable the battle menu + set enemy selection buttons + enable selection menu
        DetermineBattleOrder();
        battleMenu.SetActive(false);
        SetEnemySelectionButtons();
        enemySelectionMenu.SetActive(true);

    }

    private void SetEnemySelectionButtons()
    {
        //  disable all buttons + enable only needed buttons + change selection text to enemy names
        for (int i = 0; i < enemySelectionButtons.Length; i++)
        {
            enemySelectionButtons[i].SetActive(false);
        }

        for (int j = 0; j < enemyBattlers.Count; j++)
        {
            enemySelectionButtons[j].SetActive(true);
            enemySelectionButtons[j].GetComponentInChildren<TextMeshProUGUI>().text = enemyBattlers[j].Name;
        }
    }

    public void SelectEnemy(int currentEnemy)
    {
        // set current members target
        BattleEntities currentPlayerEntity = playerBattlers[currentPlayer];
        currentPlayerEntity.SetTarget(allBattlers.IndexOf(enemyBattlers[currentEnemy]));
        // tell battle system member wants to attack 
        currentPlayerEntity.BattleAction = BattleEntities.Action.Attack;
        // increment through all party members 
        currentPlayer++;
        
        if (currentPlayer >= playerBattlers.Count) // if all members have selected action
        {
            // start battle
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySelectionMenu.SetActive(false); // else show menu for next player
            ShowBattleMenu();
        }
    }

    public void ShowEnemySpecialSelectionMenu()
    {
        // disable the battle menu + set special enemy selection buttons + enable special selection menu
        DetermineBattleOrder();
        battleMenu.SetActive(false);
        SetEnemySpecialSelectionButtons();
        enemySpecialSelectionMenu.SetActive(true);

    }

    private void SetEnemySpecialSelectionButtons()
    {
        //  disable all buttons + enable only needed buttons + change selection text to enemy names
        for (int i = 0; i < enemySpecialSelectionButtons.Length; i++)
        {
            enemySpecialSelectionButtons[i].SetActive(false);
        }

        for (int j = 0; j < enemyBattlers.Count; j++)
        {
            enemySpecialSelectionButtons[j].SetActive(true);
            enemySpecialSelectionButtons[j].GetComponentInChildren<TextMeshProUGUI>().text = enemyBattlers[j].Name;
        }
    }

    public void SelectSpecialEnemy(int currentEnemy)
    {
        // set current members target
        BattleEntities currentPlayerEntity = playerBattlers[currentPlayer];
        currentPlayerEntity.SetTarget(allBattlers.IndexOf(enemyBattlers[currentEnemy]));
        // tell battle system member wants to attack 
        currentPlayerEntity.BattleAction = BattleEntities.Action.SpecialAttack;
        // increment through all party members 
        currentPlayer++;
        
        if (currentPlayer >= playerBattlers.Count) // if all members have selected action
        {
            // start battle
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySpecialSelectionMenu.SetActive(false); // else show menu for next player
            ShowBattleMenu();
        }
    }

    private void AttackAction(BattleEntities currAttacker, BattleEntities currTarget)
    {
        if (currTarget.IsBlocking == false)
        {
            int damage = currAttacker.Strenght; // get damage (can use an algorithm)
            currAttacker.BattleVisuals.PlayAttackAnimation(); // play attack animation
            currTarget.CurrHealth -= damage; // deal damage
            currTarget.BattleVisuals.PlayHitAnimation();// target play hit animation
            currTarget.UpdateUI(); // update UI
            bottomText.text = string.Format("{0} greift {1} an und verursacht {2} Schaden.", currAttacker.Name, currTarget.Name, damage);
            SaveHealth();
        }
        else if (currTarget.IsBlocking == true)
        {
            currAttacker.BattleVisuals.PlayAttackAnimation(); // play attack animation
            // currTarget.BattleVisuals.PlayBlockAnimation(); // play block animation if attacked
            bottomText.text = string.Format("{0} hat den Angriff von {1} abgewehrt.", currTarget.Name, currAttacker.Name);
        }
    }

    private void SpecialAttackAction(BattleEntities currAttacker, BattleEntities currTarget)
    {
        int damage = currAttacker.Strenght + 3; // maybe spaeter noch changen, balancing wise, idk
        currAttacker.BattleVisuals.PlayAttackAnimation(); // play attack animation
        int hitChance = Random.Range(0, 10);
        if(hitChance <= 7)
        {
            currTarget.CurrHealth -= damage; // deal damage
            currTarget.BattleVisuals.PlayHitAnimation();// target play hit animation
            currTarget.UpdateUI(); // update UI
            bottomText.text = string.Format("{0} greift {1} an und verursacht {2} Schaden.", currAttacker.Name, currTarget.Name, damage);
            SaveHealth();
        }
        else if (hitChance > 7)
        {
            currTarget.BattleVisuals.PlayHitAnimation();// target play hit animation
            bottomText.text = string.Format("{0} hat {1} verfehlt...", currAttacker.Name, currTarget.Name);
        }
    }

    private int GetRandomPartyMember()
    { 
        List<int> partyMembers = new List<int>(); // create a temporary int(index) list
        
        for (int i = 0; i < allBattlers.Count; i++) // find all the party members -> add them to the list
        {
            if (allBattlers[i].IsPlayer == true) // true = party member
            {
                partyMembers.Add(i);
            } 
        }
        return partyMembers[Random.Range(0, partyMembers.Count)]; // return a random party member
    }

    private int GetRandomEnemy()
    {
        List<int> enemies = new List<int>(); // create a temporary int(index) list
        
        for (int i = 0; i < allBattlers.Count; i++) // find all the enemies -> add them to the list
        {
            if (allBattlers[i].IsPlayer == false) // false = enemy
            {
                enemies.Add(i);
            } 
        }
        return enemies[Random.Range(0, enemies.Count)]; // return a random enemy
    }

    private void SaveHealth()
    {
        for (int i = 0; i < playerBattlers.Count; i++)
        {
            partyManager.SaveHealth(i, playerBattlers[i].CurrHealth);
        }
    }

    private void DetermineBattleOrder()
    {
        allBattlers.Sort((bi1, bi2) => -bi1.Initiative.CompareTo(bi2.Initiative)); // sorts list by initiative starting from highest

        for (int i = 0; i < allBattlers.Count; i++)
        {
            if(allBattlers[i].GetsPriority == true)
            {
                allBattlers[i].GetsPriority = false;
                allBattlers[i].Initiative /= 20;
            }
        }
    }

    public void SelectRunAction()
    {
        state = BattleState.Selection;
        // set current members target
        BattleEntities currentPlayerEntity = playerBattlers[currentPlayer];
        // tell battle system member wants to run
        currentPlayerEntity.BattleAction = BattleEntities.Action.Run;

        battleMenu.SetActive(false);
        // increment through all party members 
        currentPlayer++;
        
        if (currentPlayer >= playerBattlers.Count) // if all members have selected action
        {
            // start battle
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySelectionMenu.SetActive(false); // else show menu for next player
            ShowBattleMenu();
        }
    }

    public void SelectHealAction()
    {
        state = BattleState.Selection;
        // set current members target
        BattleEntities currentPlayerEntity = playerBattlers[currentPlayer];
        // tell battle system member wants to run
        currentPlayerEntity.BattleAction = BattleEntities.Action.Heal;
        currentPlayerEntity.GetsPriority = true;
        currentPlayerEntity.Initiative *= 20;
        battleMenu.SetActive(false);
        // increment through all party members 
        currentPlayer++;
        
        if (currentPlayer >= playerBattlers.Count) // if all members have selected action
        {
            // start battle
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySelectionMenu.SetActive(false); // else show menu for next player
            ShowBattleMenu();
        }
    }

    public void SelectBlockAction()
    {
        state = BattleState.Selection;
        // set current members target
        BattleEntities currentPlayerEntity = playerBattlers[currentPlayer];
        // tell battle system member wants to run
        currentPlayerEntity.BattleAction = BattleEntities.Action.Block;
        currentPlayerEntity.GetsPriority = true;
        currentPlayerEntity.Initiative *= 20;
        battleMenu.SetActive(false);
        // increment through all party members 
        currentPlayer++;
        
        if (currentPlayer >= playerBattlers.Count) // if all members have selected action
        {
            // start battle
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySelectionMenu.SetActive(false); // else show menu for next player
            ShowBattleMenu();
        }
    }
}

[System.Serializable]
public class BattleEntities
{
    public enum Action {Attack, Run, Heal, SpecialAttack, Block}
    public Action BattleAction;

    public string Name;
    public int CurrHealth;
    public int MaxHealth;
    public int Initiative;
    public int Strenght;
    public int Level;
    public bool IsPlayer;
    public BattleVisuals BattleVisuals;
    public int Target;
    public bool IsBlocking = false;
    public bool GetsPriority = false;

    public void SetEntitiesValues(string name, int currHealth, int maxHealth, int initiative, int strenght, int level, bool isPlayer)
    {
        Name = name;
        CurrHealth = currHealth;
        MaxHealth = maxHealth;
        Initiative = initiative;
        Strenght = strenght;
        Level = level;
        IsPlayer = isPlayer;
    }

    public void SetTarget(int target)
    {
        Target = target;
    }

    public void UpdateUI()
    {
        BattleVisuals.ChangeHealth(CurrHealth);
    }
}