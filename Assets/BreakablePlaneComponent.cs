using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlaneComponent : MonoBehaviour
{
    public GameObject breakablePlane;

    // Start is called before the first frame update
    void Start()
    {
        breakablePlane.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collision with Player
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(BreakDelay());
        }
    }

    //Breaking animation
    IEnumerator BreakDelay()
    {
        yield return new WaitForSeconds(2f);
        {
            // Destroy(this.gameObject);
            breakablePlane.SetActive(false);
            yield return new WaitForSeconds(3f);
            {
                breakablePlane.SetActive(true);
            }
        }
    }




}
