using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Objects;
using Newtonsoft.Json.Linq;

[SerializeField]
public class IPlayers
{
    public Dictionary<string, int> alex;
}
[SerializeField]
public class ITurn
{
    public string uid;
}

public class ServerManager : MonoBehaviour
{
    public static string turnUid = "";

    public GameObject playerPrefab;

    public AudioClip moveSound;
    public GameObject gameOverWindow;
    public GameObject questionWindow;
    public GameObject eventWindow;

    public GameObject dice;

    public Vector3[] positions = new Vector3[4];


    public Sprite[] sprites;

    public string[] names = { "herculesHead", "perseusHead", "achillesHead", "ippoHead" };


    public static Dictionary<string, string> playerDic = new Dictionary<string, string>();

    public static string[] arrayOfUserUids = new string[4];

    

    public static string myUid;

    // Start is called before the first frame update
    void Start()
    {

        Board.isMultiplayer = true;


        GetRoomStuff();

        myUid = FirebaseManager.currentUser.uid;
        Debug.Log("I am : " +myUid);
        
        // Get from room
        //arrayOfUserUids[0] = "u6bnGbv3jyUnTSh5WPcslXfLCb03"; // ipo
        positions[0] = new Vector3(-17.7f, -17, 53);
        //arrayOfUserUids[1] = "eJsYWa9Wbvg5pUFYpDRmPc0StY12"; // achil
        positions[1] = new Vector3(-23.7f, -17, 47);
        //arrayOfUserUids[2] = "player3"; // hercules
        //positions[2] = new Vector3(-23.7f, -17, 53);
        //arrayOfUserUids[3] = "player4"; // perseus
        //positions[3] = new Vector3(-17.7f, -17, 47);
        // turnUid = arrayOfUserUids[0];
        // DiceOnline.turnId = turnUid;

        DontDestroyOnLoad(gameObject);

        FirebaseDatabase.ListenForValueChanged("turn", gameObject.name,
        "OnValueChanged", "ErrorValueChanged");
    }

    public void GetRoomStuff()
    {
        FirebaseDatabase.GetJSON("rooms/room1/players", gameObject.name, "OnRoomSuccess", "OnRoomFail");
    }

    public void OnRoomSuccess(string json)
    {

        JObject jdjd = JObject.Parse(json);

        string[] uids = new string[jdjd.Count];
        int i = 0;
        foreach (JProperty p in jdjd.Properties())
        {
            Debug.Log("p is : " +i);
            Debug.Log(p.Name);
            uids[i] = p.Name;
            arrayOfUserUids[i] = p.Name;
            Debug.Log(p.Value.ToString());
            playerDic.Add(p.Name, p.Value.ToString());
            Debug.Log("****");
            i++;
        }

        Debug.Log("My name is : " + playerDic[myUid]);
        SetUp();
    }

    public void SetUp()
    {
        GameObject player = Instantiate(playerPrefab, positions[0], new Quaternion());
        player.transform.Rotate(90f, 0f, 0f, Space.Self);
        player.GetComponent<SpriteRenderer>().sprite = FromNameToSprite(playerDic[myUid]);
        player.name = playerDic[myUid];
        player.AddComponent<PlayerOnline>();
        PlayerOnline p = player.GetComponent<PlayerOnline>();
        p.User = new FirebaseUser();
        p.User.uid = myUid;
        p.moveSound = this.moveSound;
        p.gameOverWindow = this.gameOverWindow;
        p.questionWindow = this.questionWindow;
        p.eventWindow = this.eventWindow;

        dice.GetComponent<DiceOnline>().appPlayer = p;

        int i = 0;
        foreach (KeyValuePair<string,string> item in playerDic)
        {
            if (item.Key == myUid) {
                continue;
            }
            GameObject enemy = Instantiate(playerPrefab, positions[i + 1], new Quaternion());
            enemy.transform.Rotate(90f, 0f, 0f, Space.Self);
            enemy.GetComponent<SpriteRenderer>().sprite = FromNameToSprite(playerDic[item.Key]);
            enemy.name = playerDic[item.Key];
            enemy.AddComponent<EnemyOnline>();
            EnemyOnline e = enemy.GetComponent<EnemyOnline>();
            e.user = new FirebaseUser();
            e.user.uid = item.Key;
            i++;
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
        int index = 0;
        // Find which index we are
        
        for(int i =0;i<arrayOfUserUids.Length;i++) {
            if (arrayOfUserUids[i] == turnUid){
                index = i;
                break;
            }   
        }

        // Find new uid
        index++;
        if (index > 1)
        {
            index = 0;
        }

        string newTurnUid = arrayOfUserUids[index];
        Debug.Log("new uid before post: " + newTurnUid);
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


    public Sprite FromNameToSprite(string name)
    {
        foreach (Sprite sp in sprites)
        {
            if (sp.name == name)
            {
                return sp;
            }
        }


        Debug.LogError("SOMETHING WENT WRONG");

        return null;
    }

   
}
