using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirebaseManager : MonoBehaviour
{


    public GameObject email;
    public GameObject password;
    public GameObject passwordConfirm;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CreateUser()
    {
        
        string emailText = email.GetComponentInChildren<TMP_InputField>().text;
        string passwordText = password.GetComponentInChildren<TMP_InputField>().text;
        string passConfirmText = passwordConfirm.GetComponentInChildren<TMP_InputField>().text;


        Debug.Log(emailText);
        Debug.Log(passwordText);
        Debug.Log(passwordText);
        if (passwordText != passConfirmText)
        {
            Debug.Log("Ta password dn einai idia");
            // Display error mpla mpla
            return;
        }

        FirebaseAuth.CreateUserWithEmailAndPassword(emailText, passwordText, gameObject.name, "Good", "DisplayError");
    }

    public void Good(string data)
    {
        Debug.Log(data);
        Debug.Log("Good happened");
        email.GetComponentInChildren<TMP_InputField>().text = "mpravoo";
    }

    public void DisplayError(string error)
    {
        Debug.Log(error);
        Debug.Log("Errorr!!! happened");
        email.GetComponentInChildren<TMP_InputField>().text = "skatoules";
    }

}
