using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scripts.GameModels;


public class RoomManage : MonoBehaviour
{

    public List<Room> rooms;

    public GameObject roomPrefab;

    public GameObject content;

    public string activeRoom;

    public GameObject createRoomView;
    public GameObject hostRoom;
    public bool hostRoomLoading = false;


    public TMP_InputField createRoomName;
    public TMP_Dropdown diffultyDrop;
    public TMP_Dropdown maxPlayersDrop;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        FirebaseDatabase.ListenForChildAdded("rooms", gameObject.name, "OnRoomAdded", "OnError");


    }

    public void OnUserRetrieve(string user)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRoomAdded(string roomJson)
    {
        // Destructre json to room
        try
        {
            JRoom roomObj = JsonConvert.DeserializeObject<JRoom>(roomJson);
            GameObject tempRoom = Instantiate(roomPrefab, transform.position, Quaternion.identity);
            tempRoom.GetComponent<Room>().SetRoom(roomObj);
            tempRoom.transform.SetParent(content.transform);
            rooms.Add(tempRoom.GetComponent<Room>());

        }
        catch (System.Exception e)
        {
            Debug.LogError("Error deserialize room!" + e.Message);
        }

    }

    public void OnError(string errorMessage)
    {
        Debug.LogError("Error: " + errorMessage);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void CreateRoom()
    {
        string roomNameText = createRoomName.text;
        string diff = "";
        int maxP = 0;
        switch (diffultyDrop.value)
        {
            case 0:
                {
                    diff = "easy";
                    break;
                }
            case 1:
                {
                    diff = "medium";
                    break;
                }
            case 2:
                {
                    diff = "hard";
                    break;
                }
        }
        switch (maxPlayersDrop.value)
        {
            case 0:
                {
                    maxP = 4;
                    break;
                }
            case 1:
                {
                    maxP = 3;
                    break;
                }
            case 2:
                {
                    maxP = 2;
                    break;
                }
        }

        JRoom obj = new JRoom();
        obj.activePlayers = 0;
        obj.roomHost = FirebaseManager.currentUser.uid;
        obj.difficulty = diff;
        obj.maxPlayers = maxP;
        obj.roomName = roomNameText;

        JUser u = new JUser();
        u.displayName = "alex";
        u.uid = "pl1";

        JUser u2 = new JUser();
        u.displayName = "kwstas";
        u.uid = "pl2";

        try
        {
            string json = JsonConvert.SerializeObject(obj);
            string userJson = JsonConvert.SerializeObject(FirebaseManager.currentUser.user);
            Debug.Log(json);
            FirebaseDatabase.CreateRoom("rooms", json, userJson, gameObject.name, "OnRoomCreation", "OnError");
            hostRoomLoading = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error when serialize JRoom" + e.Message);
        }

        
    }

    public void OnRoomCreation(string roomKey)
    {

        Debug.Log("Crated room with id: " +roomKey);
        hostRoomLoading = false;
        hostRoom.GetComponent<HostRoom>().roomKey = roomKey;
        hostRoom.SetActive(true);
        
    }
}
