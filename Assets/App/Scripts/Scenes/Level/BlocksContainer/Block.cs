using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockMovement _blockMovement;

        public void Initialize(InputSystem inputSystem, Camera mainCamera)
        {
            _blockMovement.Initialize(inputSystem, mainCamera);
        }
    }
}