using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.Level;
using UnityEngine;

namespace App.Scripts.Installers
{
    public class LevelSceneInstaller : Installer
    {
        [SerializeField] private BlockGrid _blockGrid;
        [SerializeField] private Block _movableBlockPrefab;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private Camera _mainCamera;
        
        private void Awake()
        {
            PopUpSystem.Instance.enabled = true;
            
            _blockGrid.Initialize(5,10);

            Block block1 = Instantiate(_movableBlockPrefab);
            Block block2 = Instantiate(_movableBlockPrefab);
            Block block3 = Instantiate(_movableBlockPrefab);
            Block block4 = Instantiate(_movableBlockPrefab);
            Block block5 = Instantiate(_movableBlockPrefab);
            
            block1.Initialize(_inputSystem, _mainCamera);
            block2.Initialize(_inputSystem, _mainCamera);
            block3.Initialize(_inputSystem, _mainCamera);
            block4.Initialize(_inputSystem, _mainCamera);
            block5.Initialize(_inputSystem, _mainCamera);

            _blockGrid.SetBlock(4, 4, block1);
            _blockGrid.SetBlock(1, 7, block2);
            _blockGrid.SetBlock(4, 8, block3);
            _blockGrid.SetBlock(2, 4, block4);
            _blockGrid.SetBlock(1, 1, block5);
        }
    }
}