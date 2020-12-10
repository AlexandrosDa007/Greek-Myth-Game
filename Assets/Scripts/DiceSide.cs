using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool onGround;
    public int sideValue;

    public bool OnGround { get => onGround; set => onGround = value; }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ground")
        {
            OnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            OnGround = false;
        }
    }
}
