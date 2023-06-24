using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class CameraContainer : MonoBehaviour
    {
        public Camera MainCamera => _mainCamera;
        
        [SerializeField] private Camera _mainCamera;
        
        public void SetCameraPosition(Vector3 position)
        {
            position.y = _mainCamera.transform.position.y;
            _mainCamera.transform.position = position;
        }
    }
}