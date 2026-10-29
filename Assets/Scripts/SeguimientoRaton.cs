using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeguimientoRaton : MonoBehaviour
{

    [SerializeField] private float longitudRayo = 10000f;

    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ControlDisparo();
    }
    
    public void ControlDisparo()
    {
        GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray rayo = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(rayo, out hit, longitudRayo))
            {
                
                hit.transform.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
