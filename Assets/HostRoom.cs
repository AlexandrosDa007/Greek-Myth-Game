using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Scripts.GameModels;
using UnityEngine;
using UnityEngine.UI;

public class HostRoom : MonoBehaviour
{

    public string roomKey;
    public JRoom currentRoom;

    public GameObject playerGroup;
    public GameObject playerSetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("room key is : " + roomKey);
        FirebaseDatabase.ListenForValueChanged("rooms/"+roomKey, gameObject.name, "OnRoomGet", "OnError");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRoomGet(string roomJson)
    {
        if (roomJson == "" || roomJson == null || roomJson == "null") {
            Debug.Log("room is null");
            return;
        }
        try
        {
            JRoom room = JsonConvert.DeserializeObject<JRoom>(roomJson);
            currentRoom = room;
            Debug.Log(roomJson);
            SetUp();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error when deserialize room" +e.Message);
        }
    }


    public void SetUp()
    {
        foreach (JUser player in currentRoom.players)
        {

            GameObject _temp = Instantiate(playerSetPrefab, playerGroup.transform.position, Quaternion.identity);
            _temp.GetComponent<PlayerSet>().playerNameField.text = player.displayName;
            _temp.transform.SetParent(playerGroup.transform);
        }
    }


    public void OnError(string errorMessage)
    {
        Debug.LogError("error: " +errorMessage);
    }


}
