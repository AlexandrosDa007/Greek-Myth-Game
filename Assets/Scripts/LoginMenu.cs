using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Scripts.Objects;

public class LoginMenu : MonoBehaviour
{

    public GameObject email;
    public GameObject password;

    public GameObject errorText;

    public void LogIn()
    {
        // Try to log in
        string emailText = email.GetComponentInChildren<TMP_InputField>().text;
        string passwordText = password.GetComponentInChildren<TMP_InputField>().text;

        FirebaseAuth.SignInWithEmailAndPassword(emailText, passwordText,gameObject.name, "SuccessLogin", "FailLogin");
        
    }

    public void SuccessLogin(string successMessage)
    {
        // Go to next scene 
        Debug.Log("Succsslogin!!" + successMessage);

    }

    public void FailLogin(string error)
    {
        // Display error
        errorText.SetActive(true);
        errorText.GetComponent<TextMeshProUGUI>().text = error;
    }

}
