using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scripts.GameModels;
using UnityEngine;
using UnityEngine.UI;

public class HostRoom : MonoBehaviour
{
    public GameObject roomsView;

    public string roomKey;
    public JRoom currentRoom;

    public GameObject playerGroup;
    public GameObject playerSetPrefab;

    public bool isRoomHost = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("room key is : " + roomKey);
        FirebaseDatabase.ListenForValueChanged("rooms/" + roomKey, gameObject.name, "OnRoomGet", "OnError");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRoomGet(string roomJson)
    {
        Debug.Log(roomJson);
        if (roomJson == "" || roomJson == null || roomJson == "null")
        {
            Debug.Log("room is null");
            // If null then room is destroyed
            return;
        }
        try
        {
            JObject room = JsonConvert.DeserializeObject<JObject>(roomJson);

            foreach (JProperty prop in room.Properties())
            {
                switch (prop.Name)
                {
                    case "roomHost":
                        {
                            currentRoom.roomHost = prop.Value.ToString();
                            continue;
                        }
                    case "roomName":
                        {
                            currentRoom.roomName = prop.Value.ToString();
                            continue;
                        }
                    case "maxPlayer":
                        {
                            currentRoom.maxPlayers = int.Parse(prop.Value.ToString());
                            continue;
                        }
                    case "activePlayers":
                        {
                            currentRoom.activePlayers = int.Parse(prop.Value.ToString());
                            continue;
                        }
                    case "difficulty":
                        {
                            currentRoom.difficulty = prop.Value.ToString();
                            continue;
                        }
                    case "roomId":
                        {
                            currentRoom.roomId = prop.Value.ToString();
                            continue;
                        }
                    case "players":
                        {
                            currentRoom.players = prop.Value.ToObject<Dictionary<string, JUser>>();
                            continue;
                        }
                }
            }

            Debug.Log(currentRoom.players.Keys.Count);
            if (currentRoom.roomHost == FirebaseManager.currentUser.uid)
            {
                isRoomHost = true;
            }
            Debug.Log(roomJson);
            SetUp();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error when deserialize room" + e.Message);
        }
    }


    public void SetUp()
    {
        foreach (PlayerSet pl in playerGroup.GetComponentsInChildren<PlayerSet>())
        {
            Debug.Log("child" + pl.playerNameField.text);
            Destroy(pl.gameObject);
        }

        foreach (KeyValuePair<string, JUser> player in currentRoom.players)
        {
            GameObject _temp = Instantiate(playerSetPrefab, playerGroup.transform.position, Quaternion.identity);
            if (player.Value.ready)
            {
                _temp.GetComponent<PlayerSet>().playerNameField.color = Color.green;
            }
            _temp.GetComponent<PlayerSet>().playerNameField.text = player.Value.displayName;
            _temp.transform.SetParent(playerGroup.transform);

        }


    }


    public void OnError(string errorMessage)
    {
        Debug.LogError("error: " + errorMessage);
    }

    public void LeaveRoom()
    {
        FirebaseDatabase.StopListeningForValueChanged("rooms/" + roomKey, gameObject.name, "OnStop", "OnError");
        if (isRoomHost)
        {
            FirebaseDatabase.DeleteJSON("rooms/" + roomKey, gameObject.name, "OnRoomDestroy", "OnError");

        }
    }

    public void OnRoomDestroy(string successMessage)
    {
        // Room destroyed
        roomsView.SetActive(true);
        // Maybe remove everything
        gameObject.SetActive(false);
    }

    public void OnStop(string successMessage)
    {
        Debug.Log("Stopped listening!");
        if (!isRoomHost)
        {
            // leave
            roomsView.SetActive(true);
            // Maybe remove everything
            gameObject.SetActive(false);
        }
    }

    public void Ready()
    {
        // get ready
        FirebaseDatabase.PostJSON("rooms/" + roomKey + "/players/" + FirebaseManager.currentUser.uid + "/ready",
        "true", gameObject.name, "OnReady", "OnError");

    }

    public void OnReady(string successMessage)
    {
        // Player ready
        PlayerSet[] plsets = playerGroup.GetComponentsInChildren<PlayerSet>();
        foreach (PlayerSet pll in plsets)
        {
            if (pll.playerNameField.text == FirebaseManager.currentUser.user.displayName)
            {
                pll.playerNameField.color = Color.green;
            }
        }
    }
}
