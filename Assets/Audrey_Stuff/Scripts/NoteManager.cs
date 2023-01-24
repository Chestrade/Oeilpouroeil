using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteManager : MonoBehaviour
{
    public GameObject textBox;
    public TextMeshProUGUI noteTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        DisableTextBox();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        noteTextMesh.text = " ";    //Sets it to nothing
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
    }
}
