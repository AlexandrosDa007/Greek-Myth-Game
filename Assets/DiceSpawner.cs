using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{

    public GameObject prefab;
    public Player player;
    public Enemy enemy;
    GameObject gb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gb != null && gb.GetComponent<RealDice>().thrown)
        {
            Destroy(gb);
        }
    }

    public void SpawnDice()
    {
        gb = Instantiate(prefab, transform.position, transform.rotation);
        gb.GetComponent<RealDice>().player = player;
        gb.GetComponent<RealDice>().enemy = enemy;
        
    }
}
