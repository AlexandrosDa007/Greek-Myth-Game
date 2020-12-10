using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayOffline()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Logout()
    {
        if (GameObject.Find("FirebaseManager"))
        {
            GameObject.Find("FirebaseManager").GetComponent<FirebaseManager>().Logout();
        }else {
            Debug.LogError("Couldn't load firebase manager");
        }
    }


    public void PlayOnline()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
