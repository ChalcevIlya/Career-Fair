using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private PlayerInputSystem _inputSystem;
    [SerializeField] private Transform _transform;

    private void Awake()
    {
        _inputSystem.MouseMoveX += RotatePlayer;
    }

    private void RotatePlayer(float rotate)
    {
        rotate *= Time.deltaTime;
        _transform.Rotate(Vector3.up * rotate);
    }
}
