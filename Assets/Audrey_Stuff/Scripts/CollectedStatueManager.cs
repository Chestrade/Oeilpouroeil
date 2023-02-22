using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedStatueManager : MonoBehaviour
{
    public GameObject[] statue;
    public int statueID;


    void Start()
    {
        for (int i = 0; i < statue.Length; i++)
        {
            statue[i].gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Statue" + 0) == 1)
        {
            statue[0].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Statue" + 1) == 1)
        {
            statue[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Statue" + 2) == 1)
        {
            statue[2].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Statue" + 3) == 1)
        {
            statue[3].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Statue" + 4) == 1)
        {
            statue[4].gameObject.SetActive(true);
        }
    }

    public void EnableStatue()
    {
        statue[statueID].gameObject.SetActive(true);
    }

}
