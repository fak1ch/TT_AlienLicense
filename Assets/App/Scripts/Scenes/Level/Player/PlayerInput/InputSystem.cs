using System;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class InputSystem : MonoBehaviour
    {
        public event Action OnMouseDown;
        public event Action OnMouseUp;
        
        public Vector2 MouseInput { get; private set; }
        public Vector2 MousePosition => Input.mousePosition;
        
        private void Update()
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");
            MouseInput = new Vector2(horizontalInput, verticalInput);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnMouseDown?.Invoke();
            }
            
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                OnMouseUp?.Invoke();
            }
        }
    }
}