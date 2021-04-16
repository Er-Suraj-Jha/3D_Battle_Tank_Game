using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Tanks;

namespace BattleTank.Bullets
{
    public class BulletController
    {
        
        public BulletController(BulletModel bulletModel, BulletView bulletPrefab,string layer)
        {
            Debug.Log("BULLET CONTROLLER   1");
            BulletModel = bulletModel;
            BulletView = GameObject.Instantiate<BulletView>(bulletPrefab);
            BulletView.Initialize(this,layer);
        }

        public BulletModel BulletModel { get; }
        public BulletView BulletView { get; }
        public TankView TankView{get; }
       
        public BulletModel GetBulletModel()
        {
            return BulletModel;
        }

        //Function to calculate damage
        public float CalculateDamage(Vector3 targetPosition)
        {
            Vector3 explosionToTarget=targetPosition-BulletView.transform.position;
            float explosionDistance=explosionToTarget.magnitude;
            float relativedistance=(BulletView.ExplosionRadius-explosionDistance)/BulletView.ExplosionRadius;
            float damage=relativedistance*BulletModel.Damage;
            damage=Mathf.Max(0f,damage);
            return damage;
        }

        //Setting position for bullet
        public void SetPos(Transform Fire_Transform,float Current_Launch_Force)
        {
            BulletView.SetPos(Fire_Transform,Current_Launch_Force);
        }
    

        //Destroy logic.
        
        public void bulletDestroy()
        {
            BulletModel.DestroyBulletModel(GetBulletModel());
            BulletView.DestroyBulletView(this.BulletView);
        }
    }

}