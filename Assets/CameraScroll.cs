using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y != 0 && transform.position.z <= 20 && transform.position.z >= -7.5f)
        {
            transform.Translate(Vector3.up * Input.mouseScrollDelta.y);
            if(transform.position.z > 20){
                transform.position = new Vector3(transform.position.x, transform.position.y, 20f);
                return;
            }
            if(transform.position.z < -7.5){
                transform.position = new Vector3(transform.position.x, transform.position.y, -7.5f);
                return;
            }    
        }
    }
}
