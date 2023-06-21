using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockMovement _blockMovement;
        private InputSystem _inputSystem;
        
        public void Initialize(InputSystem inputSystem)
        {
            _blockMovement.Initialize(inputSystem);
        }
    }
}