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
        currentDangerLevel = DangerLevel.Low;
        currentSwitch = dangerLevel[1];
        enemyComponents = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemyComponents)
        {
            enemies.Add(enemy);
        }
       
    }
    private void Update()
    {
        EnemyCount();
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
            }
            else
            {
                enemy.isCountedAsPatrolling = false;
            }

            if (enemy.state == EnemyState.Alert)
            {
                if (!enemy.isCountedAsAlerted)
                {
                    alertEnemyCount++;
                    enemy.isCountedAsAlerted = true;
                }
            }
            else
            {
                enemy.isCountedAsAlerted = false;
            }

            if(enemy.state == EnemyState.Chase)
            {
                if(!enemy.isCountedAsChasing)
                {
                    chaseEnemyCount++;
                    enemy.isCountedAsChasing = true;
                }
            }
            else
            {
                enemy.isCountedAsChasing = false;
            }

            if (enemy.state ==EnemyState.Confused) 
            {
                if (!enemy.isCountedAsConfused)
                {
                    confusedEnemyCount++; 
                    enemy.isCountedAsConfused = true;
                }
                
            }
            else
            {
                enemy.isCountedAsChasing= false;
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
                if(enemy.previousState!=EnemyState.Chase)
                {
                    chaseEnemyCount--;
                }
                if(enemy.previousState>EnemyState.Confused)
                {
                    confusedEnemyCount--;
                }
                enemy.previousState = enemy.state;
            }
        }

        Debug.Log(patrolEnemyCount + "enemies are patrolling");
        Debug.Log(alertEnemyCount + "enemies are alerted");
        Debug.Log(chaseEnemyCount + "enemies are chasing");
        Debug.Log(confusedEnemyCount + "enemies are confused");
    }
    
}


    