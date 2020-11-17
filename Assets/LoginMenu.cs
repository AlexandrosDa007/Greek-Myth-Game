using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour
{

    public GameObject email;
    public GameObject password;

    public void LogIn()
    {
        // Try to log in
        if (email.GetComponentInChildren<TMP_InputField>().text == "alex@kati.com" && password.GetComponentInChildren<TMP_InputField>().text == "123456")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // Maybe show snackBar or something
            Debug.Log("lathos");
        }
    }
}
