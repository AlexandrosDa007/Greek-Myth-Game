using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Step : MonoBehaviour
{
    // Start is called before the first frame update
    public int stepID;
    private TextMeshProUGUI text;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        stepID = int.Parse(text.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
