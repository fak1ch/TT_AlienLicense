using App.Scripts.Scenes.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class CameraContainer : MonoBehaviour
    {
        public Camera MainCamera => _mainCamera;
        
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LevelSceneConfig _levelSceneConfig;

        private CameraContainerConfig _config => _levelSceneConfig.CameraContainerConfig;

        public void Initialize(Vector3 cameraPosition, BlockGrid blockGrid)
        {
            SetCameraPosition(cameraPosition);
            AutoSizeFieldOfView(blockGrid);
        }
        
        private void SetCameraPosition(Vector3 position)
        {
            position.y = _mainCamera.transform.position.y;
            _mainCamera.transform.position = position;
        }

        private void AutoSizeFieldOfView(BlockGrid blockGrid)
        {
            float mapWidth = blockGrid.Columns * blockGrid.CellSize;
            float fieldOfViewMultiplier = _mainCamera.fieldOfView / 100;

            Vector3 cameraPosition = _mainCamera.transform.position;
            cameraPosition.y = (mapWidth / fieldOfViewMultiplier) + _config.PositionYOffset;
            _mainCamera.transform.position = cameraPosition;
        }
    }
}