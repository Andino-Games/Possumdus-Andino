using System;
using _scripts.Audio;
using _scripts.Interfaces;
using _scripts.Managers;
using _scripts.Player.Context;
using UnityEngine;


namespace _scripts.Player
{
    [Serializable]
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        [Header("Values Configuration")]
        [SerializeField] public float normalSpeed;
        [SerializeField] public float _currentSpeed;
        [SerializeField] private float rotationX;
        public Transform _playerCamera;
        private Rigidbody _rb;
       
        #region PlayerMovement
        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;                     
            Cursor.lockState = CursorLockMode.Locked;
            _playerCamera = Camera.main?.transform;
            _currentSpeed = normalSpeed;
        }
    
        public void Rotation(float mouseX, float mouseY)
        {
            if(CursorManager.instance.CursorState == CursorState.HideCursor) return;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            _playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        public void Move(float horizontal, float vertical)
        {
            if(CursorManager.instance.CursorState == CursorState.HideCursor) return;

            Vector3 movement = transform.right * horizontal + transform.forward * vertical;
            movement.Normalize();
            movement *= _currentSpeed;
            
            Vector3 newSpeed = new Vector3(movement.x, _rb.velocity.y, movement.z);
            _rb.velocity = newSpeed;
        }
        #endregion
    }
}
