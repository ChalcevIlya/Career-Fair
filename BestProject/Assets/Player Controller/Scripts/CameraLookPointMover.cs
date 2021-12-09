using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookPointMover : MonoBehaviour
{
    [SerializeField] private PlayerInputSystem _inputSystem;
    [SerializeField] private Transform _transform;

    [Header ("Point Move Parametrs")]
    [SerializeField] private float _topBorder;
    [SerializeField] private float _bottomBorder;

    private void Awake()
    {
        _inputSystem.MouseMoveY += PointMove;
    }

    private void PointMove(float mouseMove)
    {
        mouseMove *= Time.deltaTime;
        Vector3 move = Vector3.zero;
        if(Mathf.Sign(mouseMove) < 0)
        {
            if(mouseMove + _transform.localPosition.y < _bottomBorder) move.y = _bottomBorder - _transform.localPosition.y;
            else move.y = mouseMove;
        }
        else
        {
            if(mouseMove + _transform.localPosition.y > _topBorder) move.y = _topBorder - _transform.localPosition.y;
            else move.y = mouseMove;
        }
        _transform.position += move;
    }
}
