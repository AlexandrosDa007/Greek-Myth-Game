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
        if(Input.mouseScrollDelta.y != 0 && transform.position.y <= -20 && transform.position.y >= -33)
        {
            transform.Translate(Vector3.up * Input.mouseScrollDelta.y);
            if(transform.position.y > - 20){
                transform.position = new Vector3(transform.position.x, -20, transform.position.z);
                return;
            }
            if(transform.position.y < -33){
                transform.position = new Vector3(transform.position.x, -33, transform.position.z);
                return;
            }    
        }
    }
}
