using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class PlayerControlNetwork : NetworkBehaviour
{
    [SerializeField]
        private float walkSpeed = 3.5f;

        [SerializeField]
        private float rotationSpeed = 3.5f;
        
        [SerializeField]
        private float runSpeedOffset = 2.0f;
    
        [SerializeField]
        private Vector2 defaultInitialPositionOnPlane = new Vector2(-20, 4);
    
        [SerializeField]
        private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();
    
        [SerializeField]
        private NetworkVariable<Vector3> networkRotationDirection = new NetworkVariable<Vector3>();

        // client caches positions
        private Vector3 oldInputPosition = Vector3.zero;
        private Vector3 oldInputRotation = Vector3.zero;
        
        private CharacterController characterController;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            if (IsClient && IsOwner)
            {
                transform.position = new Vector3(Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y), 0,
                       Random.Range(defaultInitialPositionOnPlane.x, defaultInitialPositionOnPlane.y));
            }
        }
    
        void Update()
        {
            if (IsClient && IsOwner)
            {
                ClientInput();
            }
    
            ClientMoveAndRotate();
        }
    
        private void ClientMoveAndRotate()
        {
            if (networkPositionDirection.Value != Vector3.zero)
            {
                characterController.SimpleMove(networkPositionDirection.Value);
            }
            if (networkRotationDirection.Value != Vector3.zero)
            {
                transform.Rotate(networkRotationDirection.Value, Space.World);
            }
        }
        
    
        private void ClientInput()
        {
            // left & right rotation
            Vector3 inputRotation = new Vector3(0, Input.GetAxis("Horizontal"), 0);
    
            // forward & backward direction
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            float forwardInput = Input.GetAxis("Vertical");
            Vector3 inputPosition = direction * forwardInput;
            
            if (ActiveRunningActionKey() && forwardInput > 0 && forwardInput <= 1)
            {
                inputPosition = direction * runSpeedOffset;
            }
            // let server know about position and rotation client changes
            if (oldInputPosition != inputPosition ||
                oldInputRotation != inputRotation)
            {
                oldInputPosition = inputPosition;
                UpdateClientPositionAndRotationServerRpc(inputPosition * walkSpeed, inputRotation * rotationSpeed);
            }
        }

        private static bool ActiveRunningActionKey()
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }
        
        [ServerRpc]
        public void UpdateClientPositionAndRotationServerRpc(Vector3 newPosition, Vector3 newRotation)
        {
            networkPositionDirection.Value = newPosition;
            networkRotationDirection.Value = newRotation;
        }
}
