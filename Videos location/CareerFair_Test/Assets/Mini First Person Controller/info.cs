using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info : MonoBehaviour
{
    public Transform PlayerCamera;
    public float MaxDistance = 5;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Pressed();
            Debug.Log("You Press F");
        }
    }

    void Pressed()
    {
        RaycastHit interact;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out interact, MaxDistance))
        {
            if (interact.transform.tag == "info1")
            {
                Application.OpenURL("http://unity3d.com/");
            }
        }
    }
}
