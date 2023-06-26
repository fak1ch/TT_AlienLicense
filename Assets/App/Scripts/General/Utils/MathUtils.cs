using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.General.Utils
{
    public static class MathUtils
    {
        public static float GetPercent(float min, float max, float value)
        {
            value = Mathf.Clamp(value, min, max);
            
            if (max - min == 0) return 0;
            
            return (value - min) / (max - min);
        }
        
        public static float GetPercentUnclamped(float min, float max, float value)
        {
            if (max - min == 0) return 0;

            return (value - min) / (max - min);
        }

        public static bool IsProbability(float percent)
        {
            return Random.Range(0f, 101f) <= percent;
        }
        
        public static Vector3 RandomRangeVector3(Vector3 firstVector3, Vector3 secondVector3)
        {
            Vector3 resultVector = new Vector3();

            resultVector.x = Random.Range(firstVector3.x, secondVector3.x);
            resultVector.y = Random.Range(firstVector3.y, secondVector3.y);
            resultVector.z = Random.Range(firstVector3.z, secondVector3.z);

            return resultVector;
        }

        public static float RandomVector2MinMax(Vector2 vector2)
        {
            return Random.Range(vector2.x, vector2.y);
        }

        public static Vector3 GetMouseWorldPosition(Camera mainCamera, float targetY)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector3 vector3MousePosition = new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.y);
            
            Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(vector3MousePosition);
            worldMousePosition.y = targetY;

            return worldMousePosition;
        }

        public static Vector3 ClampVector3(Vector3 vector, Vector3 min, Vector3 max)
        {
            vector.x = Math.Clamp(vector.x, min.x, max.x);
            vector.y = Math.Clamp(vector.y, min.y, max.y);
            vector.z = Math.Clamp(vector.z, min.z, max.z);

            return vector;
        }
    }
}