using UnityEngine;
using UnityEngine.InputSystem;

public class SeguimientoRaton : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Mouse.current == null) return;
        transform.position = Mouse.current.position.ReadValue();
    }
}