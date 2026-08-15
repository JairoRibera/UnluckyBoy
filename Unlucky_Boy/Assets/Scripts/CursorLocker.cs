using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    public bool bloquearAlIniciar = true;
    public KeyCode teclaLiberar = KeyCode.Escape;

    private void Start()
    {
        if (bloquearAlIniciar)
        {
            BloquearCursor();
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(teclaLiberar))
        {
            LiberarCursor();
        }

        
        if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked)
        {
            BloquearCursor();
        }
    }

    public void BloquearCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LiberarCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
