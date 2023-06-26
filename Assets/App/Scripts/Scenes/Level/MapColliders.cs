using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class MapColliders : MonoBehaviour
    {
        [SerializeField] private BoxCollider _leftBoxCollider;
        [SerializeField] private BoxCollider _rightBoxCollider;
        [SerializeField] private BoxCollider _topBoxCollider;
        [SerializeField] private BoxCollider _bottomBoxCollider;

        public void Initialize(BlockGrid blockGrid, Vector3 centerMapPosition)
        {
            float cellSize = blockGrid.CellSize;
            int rows = blockGrid.Rows;
            int columns = blockGrid.Columns;

            Vector3 colliderSize = new Vector3(columns * cellSize, cellSize, rows * cellSize);

            Vector3 leftColliderCenter = centerMapPosition;
            leftColliderCenter.x -= colliderSize.x;
            
            Vector3 rightColliderCenter = centerMapPosition;
            rightColliderCenter.x += colliderSize.x;
            
            Vector3 topColliderCenter = centerMapPosition;
            topColliderCenter.z += colliderSize.z; 
            
            Vector3 bottomColliderCenter = centerMapPosition;
            bottomColliderCenter.z -= colliderSize.z;
            
            InitializeBoxCollider(_leftBoxCollider, leftColliderCenter, colliderSize);
            InitializeBoxCollider(_rightBoxCollider, rightColliderCenter, colliderSize);
            InitializeBoxCollider(_topBoxCollider, topColliderCenter, colliderSize);
            InitializeBoxCollider(_bottomBoxCollider, bottomColliderCenter, colliderSize);
        }

        private void InitializeBoxCollider(BoxCollider boxCollider, Vector3 center, Vector3 size)
        {
            boxCollider.center = center;
            boxCollider.size = size;
        }
    }
}