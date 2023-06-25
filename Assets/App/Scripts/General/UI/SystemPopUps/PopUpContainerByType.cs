using System;
using App.Scripts.General.ObjectPool;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public class PopUpContainerByType : PoolContainerByType<PopUp>
    {
        public PopUpContainerByType(PoolObjectInformation<PopUp>[] poolObjectInfos, Transform poolContainer) : base(poolObjectInfos, poolContainer)
        {
            
        }

        public PopUp GetPopUpFromPoolByType(Type type)
        {
            return GetObjectFromPoolByType(type);
        }

        public void ReturnPopUpToPool(PopUp popUp)
        {
            ReturnObjectToPool(popUp);
        }
    }
}