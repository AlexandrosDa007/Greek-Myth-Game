using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManage : MonoBehaviour
{
    public Player[] players;
    public GameObject questionWindow;
    public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            questionWindow.SetActive(true);
            
        }
    }

}
