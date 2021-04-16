using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleTank.Tanks
{
    public class TankModel
    {
        
        public TankModel(TankScriptableObjects tankScriptableObjects)
        {
            this.TankType = tankScriptableObjects.TankType;
            this.Speed = (int)tankScriptableObjects.Speed;
            this.Health = (int)tankScriptableObjects.Health;
            this.BulletType = tankScriptableObjects.bullet;
            this.colorType = tankScriptableObjects.color;
        }

        public TankType TankType { get; }
        public int Speed { get; }
        public int Health { get; set; }
        public BulletType BulletType { get; }
        public Color colorType { get; }


        public void destroyModel(TankModel tankModel)
        {
            tankModel = null;
        }

    }   
}

