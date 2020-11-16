using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManage : MonoBehaviour
{
    public GameObject[] steps;
    // Start is called before the first frame update
    public Sprite normalSprite;
    void Start()
    {
        foreach (GameObject step in steps)
        {
            string text = step.GetComponent<TextMeshPro>().text;
            if (text == "3" || text == "5" || text == "25" || text == "27" ||
                text == "29" || text == "23" || text == "42" || text == "39" || 
                text == "10" || text == "21" || text == "46" || text == "11" || 
                text == "37" || text == "33" || text == "17" || text == "15")
            {
                step.GetComponentInChildren<SpriteRenderer>().sprite = normalSprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
