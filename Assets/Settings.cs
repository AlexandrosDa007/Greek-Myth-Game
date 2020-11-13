using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Settings : MonoBehaviour
{
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        this.definedButton = this.gameObject;   
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
            {
                
                OpenSettings();
                OnClick.Invoke();
            }
        }
    }

    public void OpenSettings()
    {
        
    }
}
