using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AK.Wwise.Event enemyFs;

    private void EnemyFootstep()
    {
        enemyFs.Post(gameObject);
    }
    

    
}
