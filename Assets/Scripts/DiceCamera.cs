using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCamera : MonoBehaviour
{

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position;
        transform.LookAt(target);
        //transform.position = new Vector3(pos.x +10.0f,pos.y+10.0f,pos.z +10.0f);
    }
}
