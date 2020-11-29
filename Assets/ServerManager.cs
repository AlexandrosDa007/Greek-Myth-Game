using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Objects;


[SerializeField]
public class ITurn
{
    public string uid;
}

public class ServerManager : MonoBehaviour
{
    public static string turnUid;

    public GameObject playerPrefab;

    public AudioClip moveSound;
    public GameObject gameOverWindow;
    public GameObject questionWindow;
    public GameObject eventWindow;

    public GameObject dice;

    public Vector3[] positions = new Vector3[4];


    public Sprite[] sprites;

    public string[] names = {"herculesHead", "perseusHead", "achillesHead", "ippoHead"};


    public static string[] arrayOfUserUids = new string[4];
    // Start is called before the first frame update
    void Start()
    {
        Board.isMultiplayer = true;
        // Get from room
        arrayOfUserUids[0] = "player1"; // ipo
        positions[0] = new Vector3(-17.7f, -17, 53);
        arrayOfUserUids[1] = "player2"; // achil
        positions[1] = new Vector3(-23.7f, -17, 47);
        arrayOfUserUids[2] = "player3"; // hercules
        positions[2] = new Vector3(-23.7f, -17, 53);
        arrayOfUserUids[3] = "player4"; // perseus
        positions[3] = new Vector3(-17.7f, -17, 47);
        turnUid = arrayOfUserUids[0];
        DiceOnline.turnId = turnUid;

        SetUp();

        DontDestroyOnLoad(gameObject);

        FirebaseDatabase.ListenForValueChanged("turn", gameObject.name,
        "OnValueChanged", "ErrorValueChanged");
    }

    public void SetUp()
    {
        
        GameObject player = Instantiate(playerPrefab, positions[0], new Quaternion());
        player.transform.Rotate(90f,0f,0f,Space.Self);
        player.GetComponent<SpriteRenderer>().sprite = sprites[3];
        player.name = names[3];
        player.AddComponent<PlayerOnline>();
        PlayerOnline p = player.GetComponent<PlayerOnline>();
        Debug.Log("222");
        Debug.Log(p);
        Debug.Log("222");
        p.User = new FirebaseUser();
        p.User.uid = arrayOfUserUids[0];
        p.moveSound = this.moveSound;
        p.gameOverWindow = this.gameOverWindow;
        p.questionWindow = this.questionWindow;
        p.eventWindow = this.eventWindow;

        dice.GetComponent<DiceOnline>().appPlayer = p;

        for (int i = 0; i < 3; i++)
        {
            GameObject enemy = Instantiate(playerPrefab, positions[i+1], new Quaternion());
            enemy.transform.Rotate(90f,0f,0f,Space.Self);
            enemy.GetComponent<SpriteRenderer>().sprite = sprites[0];
            Debug.Log("ineme");
            enemy.name = names[i];
            enemy.AddComponent<EnemyOnline>();
            EnemyOnline e = enemy.GetComponent<EnemyOnline>();
            e.user = new FirebaseUser();
            e.user.uid = arrayOfUserUids[i+1];
        }
    }

    public void OnValueChanged(string uidObject)
    {

        ITurn turn = JsonUtility.FromJson<ITurn>(uidObject);
        turnUid = turn.uid;
        DiceOnline.turnId = turn.uid;
        Debug.Log("Turn uid is now: " + turnUid);
    }
    public void ErrorValueChanged(string errorMessage)
    {
        Debug.LogError("Error when getting turn" + errorMessage);
    }


    public static void NextTurn()
    {
        int index = -999;
        // Find which index we are
        for (int i = 0; i < arrayOfUserUids.Length; i++)
        {
            if (turnUid == arrayOfUserUids[i])
            {
                index = i;
            }
        }

        // Find new uid
        index++;
        if (index > 3)
        {
            index = 0;
        }

        string newTurnUid = arrayOfUserUids[index];
        Debug.Log("new uid before post: " +newTurnUid );
        ITurn newTurn = new ITurn();
        newTurn.uid = newTurnUid;
        // post new turn
        FirebaseDatabase.PostJSON("turn/uid", newTurnUid, "ServerManager", "ProccedNextTurn", "ErrorNextTurn");
    }

    public void ProccedNextTurn(string value)
    {
        Debug.Log(value);
        // ITurn newTurn = JsonUtility.FromJson<ITurn>(value);
        // turnUid = newTurn.uid;
        // DiceOnline.turnId = turnUid;

        // Debug.Log("Turn NOW IS " + turnUid);
    }
}
