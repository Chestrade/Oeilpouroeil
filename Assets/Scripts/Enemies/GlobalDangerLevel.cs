using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK;
using AK.Wwise;
using IndieMarc.EnemyVision;


public class GlobalDangerLevel : SingletonMonoBehaviour<GlobalDangerLevel>
{
    [SerializeField] private DangerLevel currentDangerLevel;

    [Header("Wwise Stuff")]
    public AK.Wwise.Switch[] dangerLevel;
    private AK.Wwise.Switch currentSwitch;


    private Enemy[] enemyComponents;
    public List<Enemy> enemies = new List<Enemy>();


    private Cammouflage cammo;
    private PlayerController player;

    private int patrolEnemyCount;
    private int alertEnemyCount;
    private int chaseEnemyCount;
    private int confusedEnemyCount;

    public enum DangerLevel
    {
        Hidden,
        Low,
        Medium,
        High
    }

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        player = PlayerController.instance;
        cammo = player.GetComponent<Cammouflage>();
        currentDangerLevel = DangerLevel.Low;
        currentSwitch = dangerLevel[1];
        currentSwitch.SetValue(gameObject);
        enemyComponents = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemyComponents)
        {
            enemies.Add(enemy);
        }
       
    }
    private void Update()
    {
        EnemyCount();
        SetState();
    }
    void EnemyCount()
    {
        

        foreach (Enemy enemy in enemies)
        {
            if(enemy.state == EnemyState.Patrol)
            {
                if (!enemy.isCountedAsPatrolling)
                {
                    patrolEnemyCount++;
                    enemy.isCountedAsPatrolling = true;
                }
                enemy.isCountedAsAlerted = false;
                enemy.isCountedAsChasing = false;
                enemy.isCountedAsConfused = false;
            }
       
            else if (enemy.state == EnemyState.Alert)
            {
                if (!enemy.isCountedAsAlerted)
                {
                    alertEnemyCount++;
                    enemy.isCountedAsAlerted = true;
                }
                enemy.isCountedAsPatrolling = false;
                enemy.isCountedAsChasing = false;
                enemy.isCountedAsConfused = false;
            }
            

            else if(enemy.state == EnemyState.Chase)
            {
                if(!enemy.isCountedAsChasing)
                {
                    chaseEnemyCount++;
                    enemy.isCountedAsChasing = true;
                }
                enemy.isCountedAsPatrolling = false;
                enemy.isCountedAsAlerted = false;
                enemy.isCountedAsConfused = false;
            }


            else if (enemy.state ==EnemyState.Confused) 
            {
                if (!enemy.isCountedAsConfused)
                {
                    confusedEnemyCount++; 
                    enemy.isCountedAsConfused = true;
                }
                enemy.isCountedAsPatrolling = false;
                enemy.isCountedAsAlerted = false;
                enemy.isCountedAsChasing = false;

            }
            else
            {
                enemy.isCountedAsPatrolling = false;
                enemy.isCountedAsAlerted = false;
                enemy.isCountedAsChasing = false;
                enemy.isCountedAsConfused = false;
            }

        }

        foreach (Enemy enemy in enemies)
        {
            if(enemy.previousState != enemy.state)
            {
                if(enemy.previousState == EnemyState.Patrol)
                {
                    patrolEnemyCount--;
                }
                if(enemy.previousState==EnemyState.Alert)
                {
                    alertEnemyCount--;
                }
                if(enemy.previousState ==EnemyState.Chase)
                {
                    chaseEnemyCount--;
                }
                if(enemy.previousState ==EnemyState.Confused)
                {
                    confusedEnemyCount--;
                }
                enemy.previousState = enemy.state;
            }
        }
    }

    void SetState()
    {
        if (!cammo.isCammouflaged) 
        {
            if (chaseEnemyCount > 0)
            {
                currentDangerLevel = DangerLevel.High;
            }
            else if (confusedEnemyCount > 0 || alertEnemyCount > 0 && chaseEnemyCount == 0)
            {
                currentDangerLevel = DangerLevel.Medium;
            }
            else if (chaseEnemyCount == 0 && confusedEnemyCount == 0 && alertEnemyCount == 0)
            {
                currentDangerLevel = DangerLevel.Low;
            }
            else
            {
                Debug.Log("The frog is not cammouflaged. But there is no danger level set.");
            }
        }
        if (cammo.isCammouflaged)
        {
            if (chaseEnemyCount > 0)
            {
                currentDangerLevel = DangerLevel.Medium;
            }
            else if (confusedEnemyCount > 0 || alertEnemyCount > 0 && chaseEnemyCount == 0)
            {
                currentDangerLevel = DangerLevel.Low;
            }
            else if (chaseEnemyCount == 0 && confusedEnemyCount == 0 && alertEnemyCount == 0)
            {
                currentDangerLevel = DangerLevel.Hidden;
            }
            else
            {
                Debug.Log("The frog is cammouflaged. But there is no danger level set.");
            }
        }

        Debug.Log("Frog cammo = " + cammo.isCammouflaged);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Number of enemies patrolling: " + patrolEnemyCount);
        GUI.Label(new Rect(10, 30, 200, 20), "Number of enemies on alert: " + alertEnemyCount);
        GUI.Label(new Rect(10, 50, 200, 20), "Number of enemies chasing: " + chaseEnemyCount);
        GUI.Label(new Rect(10, 70, 200, 20), "Number of enemies confused: " + confusedEnemyCount);

        Debug.Log("Danger level : " + currentDangerLevel);
        //GUI.Label(new Rect(10, 100, 200, 20), "The current switch is : " + currentSwitch);
        //GUI.Label(new Rect(10, 120, 200, 20), "The current danger level is : " + currentDangerLevel);
    }

}


    