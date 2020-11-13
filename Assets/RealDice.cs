using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealDice : MonoBehaviour
{

    Rigidbody rb;
    public bool hasLanded;
    public bool thrown;

    private Vector3 initialPosition;

    public int diceValue;

    public DiceSide[] diceSides;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }

        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            SideValueCheck();

        }
        else if (rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            RollAgain();
        }
    }

    void RollDice()
    {
        if (!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if (thrown && hasLanded)
        {
            Reset();
        }
    }

    void Reset()
    {
        transform.position = initialPosition;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;

    }

    void RollAgain()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));

    }

    void SideValueCheck()
    {
        diceValue = 0;
        
        foreach (DiceSide side in diceSides)
        {
            
            if (side.OnGround)
            {
                diceValue = side.sideValue;
                Debug.Log("You got: diceValue");
            }
        }

    }
}
