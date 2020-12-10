using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.GameModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Room : MonoBehaviour
{

    public JRoom room;
    public Button joinButton;
    public TextMeshProUGUI serverName;
    public TextMeshProUGUI currentPlayers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRoom(JRoom room)
    {
        this.room = room;
        serverName.text = this.room.roomName;
        currentPlayers.text = this.room.activePlayers + "/" + this.room.maxPlayers;
    }

    public void JoinRoom()
    {
        // TODO: join this room
        try
        {
            string userJson = JsonConvert.SerializeObject(FirebaseManager.currentUser.user);
            FirebaseDatabase.JoinRoom("rooms", room.roomUid, userJson, gameObject.name, "OnJoinRoom", "OnError");

        }
        catch (System.Exception)
        {
            Debug.LogError("Error when serialize! user");
        }



    }


    public void OnJoinRoom(string successMessage)
    {
        Debug.Log("Joined room succesffully");
    }
}
