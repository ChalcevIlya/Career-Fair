using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private PlayerInputSystem _inputSystem;
    [SerializeField] private Transform _transform;

    [Header ("Move parametrs")]
    [SerializeField] private float _speed;

    private void Awake() 
    {
        _inputSystem.OnMoveInput += Move;
    }

    private void Move(Vector3 direction)
    {
        float moveStep = _speed * Time.deltaTime;
        _transform.position += (Vector3)(_transform.localToWorldMatrix * direction.normalized * moveStep);
    }
}
