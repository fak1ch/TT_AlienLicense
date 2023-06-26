using App.Scripts.Scenes.Level;
using UnityEngine;

namespace App.Scripts.General
{
    public static class Extensions
    {
        public static CustomVector3 ToCustomVector3(this Vector3 vector3)
        {
            return new CustomVector3()
            {
                X = vector3.x,
                Y = vector3.y,
                Z = vector3.z,
            };
        }

        public static Vector3 ToUnityVector3(this CustomVector3 customVector3)
        {
            return new Vector3(customVector3.X, customVector3.Y, customVector3.Z);
        }
    }
}