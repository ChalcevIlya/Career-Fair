using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    [Header ("Mouse Parametrs")]
    [SerializeField] [Range (1, 100)] private float _mouseXSensitivity;
    [SerializeField] [Range (1, 100)] private float _mouseYSensitivity;
    private bool inputLocked = false;
    
    public delegate void DirectionInput(Vector3 direction);
    public delegate void ValueInput(float value);
    public delegate void ButtonInput();

    public DirectionInput OnMoveInput;
    public ValueInput MouseMoveX;
    public ValueInput MouseMoveY;

    private void Start()
    {
        LockMouse();
    }

    private void Update() 
    {
        if(inputLocked == false)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 inputMoveDirection = new Vector3(x, 0, z);

            float mouseX = Input.GetAxis("Mouse X") * _mouseXSensitivity;
            MouseMoveX?.Invoke(mouseX);
            float mouseY = Input.GetAxis("Mouse Y") * _mouseYSensitivity;
            MouseMoveY?.Invoke(mouseY);
            
            OnMoveInput?.Invoke(inputMoveDirection);
        }
    }

    public void LockInput()
    {
        inputLocked = true;
    }

    public void UnlockInput()
    {
        inputLocked = false;
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
