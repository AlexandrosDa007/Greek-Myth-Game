using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealDice : MonoBehaviour
{

    public Rigidbody rb;
    public Player player;
    public Enemy enemy;
    public DiceSide[] diceSides;
    public bool thrown;
    public int diceValue;

    // Start is called before the first frame update
    void Start()
    {
        thrown = false;
        //transform.rotation = new Quaternion(Random.Range(0, 1500), Random.Range(0, 1500), Random.Range(0, 1500), 0);
        rb.AddTorque(Random.Range(0,10000), Random.Range(0,10000), Random.Range(0,10000));
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.IsSleeping())
        {
            SideValueCheck();
        }
    }

    public void Reset()
    {
        thrown = false;
        rb.useGravity = false;

    }


    void SideValueCheck()
    {
        diceValue = 0;

        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround)
            {
                diceValue = side.sideValue;
                thrown = true;

                if (Dice.turn == "player"){
                    player.MovePlayer(diceValue);
                }
                else{
                    enemy.MoveEnemy(diceValue);
                }
            }
        }

    }
}
