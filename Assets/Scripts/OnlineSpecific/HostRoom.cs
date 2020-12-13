using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
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
        if (roomJson == "" || roomJson == null || roomJson == "null")
        {
            Debug.Log("room is null");
            // If null then room is destroyed
            return;
        }
        try
        {
            JRoom room = JsonConvert.DeserializeObject<JRoom>(roomJson);
            currentRoom = room;
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

        foreach (JUser player in currentRoom.players)
        {
            GameObject _temp = Instantiate(playerSetPrefab, playerGroup.transform.position, Quaternion.identity);
            _temp.GetComponent<PlayerSet>().playerNameField.text = player.displayName;
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
        FirebaseDatabase.PushJSON("rooms/" + roomKey + "players",
        FirebaseManager.currentUser.uid, gameObject.name, "OnReady", "OnError");

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
