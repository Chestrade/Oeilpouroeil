using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityUnlocks : MonoBehaviour
{
    public bool abilityOne = false;
    public bool abilityTwo = false;
    public bool abilityThree = false;
    public bool abilityFour = false;
    public bool abilityFive = false;


    void Start()
    {
        if (PlayerPrefs.GetInt("Statue" + 0) == 1)
        {
            abilityOne = true;
        }
        if (PlayerPrefs.GetInt("Statue" + 1) == 1)
        {
            abilityTwo = true;
        }
        if (PlayerPrefs.GetInt("Statue" + 2) == 1)
        {
            abilityThree = true;
        }
        if (PlayerPrefs.GetInt("Statue" + 3) == 1)
        {
            abilityFour = true;
        }
        if (PlayerPrefs.GetInt("Statue" + 4) == 1)
        {
            abilityFive = true;
        }

    }

}
