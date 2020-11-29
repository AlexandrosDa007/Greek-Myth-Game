using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Scripts.Objects;
using UnityEngine.SceneManagement;



public class FirebaseManager : MonoBehaviour
{

    public static FirebaseUser currentUser;

    public GameObject email;
    public GameObject password;
    public GameObject passwordConfirm;

    public GameObject loginHandler;
    public GameObject registerError;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        FirebaseAuth.OnAuthStateChanged(gameObject.name, "OnUserSignedIn", "OnUserSignedOut");
        FirebaseDatabase.ListenForValueChanged("test", gameObject.name, "OnValueChanged", "ErrorValueChanged");
    }

    public void CreateUser()
    {
        string emailText = email.GetComponentInChildren<TMP_InputField>().text;
        string passwordText = password.GetComponentInChildren<TMP_InputField>().text;
        string passConfirmText = passwordConfirm.GetComponentInChildren<TMP_InputField>().text;

        if (passwordText != passConfirmText)
        {
            Debug.Log("Ta password dn einai idia");
            // Display error mpla mpla
            registerError.SetActive(true);
            registerError.GetComponent<TextMeshProUGUI>().text = "Passwords do not match";
            return;
        } else {
            registerError.SetActive(false);
        }

        FirebaseAuth.CreateUserWithEmailAndPassword(emailText, passwordText, gameObject.name, "Good", "DisplayError");

        
    }

    public void Logout()
    {
        FirebaseAuth.SignOut(gameObject.name, "OnSuccessLogout", "OnFailLogout");
    }

    public void Good(string data)
    {
        Debug.Log(data);
        Debug.Log("Good happened");
        registerError.SetActive(false);
    }

    public void DisplayError(string errorMessage)
    {
        Debug.Log(errorMessage);
        Debug.Log("Errorr!!! happened");
        registerError.SetActive(true);
        registerError.GetComponent<TextMeshProUGUI>().text = errorMessage;
    }


    public void OnUserSignedIn(string user)
    {
        FirebaseUser u = JsonUtility.FromJson<FirebaseUser>(user);
        // User signed in
        currentUser = u;
        Debug.Log("User signedIn" + currentUser.uid);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }


    public void OnUserSignedOut(string message)
    {
        
        // Sign in
        currentUser = null;
        // Logout??
        Debug.Log("log logout");
        Debug.LogError("Logerror LOgout");
    }

    public void OnSuccessLogout(string successMessage)
    {
        Debug.Log("User sucessfully logged out!" + successMessage);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnFailLogout(string errorMessage)
    {
        Debug.Log("Current user cannot logout!! error: " + errorMessage);
    }


    public void OnValueChanged(string value)
    {
        FirebaseTest parsedObj = JsonUtility.FromJson<FirebaseTest>(value);
        Debug.Log("test name is : " + parsedObj.name);
        Debug.Log("test power is : " + parsedObj.power);
    }

    public void ErrorValueChanged(string errorMessage)
    {
        Debug.LogError("Error change value! " + errorMessage);
    }
}
