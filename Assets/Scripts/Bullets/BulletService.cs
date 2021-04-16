using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BattleTank.Tanks;

namespace BattleTank.Bullets
{
    
    public class BulletService : MonoSingletonGeneric<BulletService>
    {

        private BulletModel bulletModel;
        public BulletController bulletController;
        public BulletView bulletView;
        public BulletScriptableObjectList bulletList;
        private BulletScriptableObject bulletScriptableObject;

        public List<BulletController> bulletsCreated = new List<BulletController>();


        private void Start()
        {
            bulletScriptableObject = ScriptableObject.CreateInstance<BulletScriptableObject>();
            Debug.Log("This message is from bullet service.");
        }


        public BulletController CreateBullet(int index,string Layer)
        {
            bulletScriptableObject = bulletList.bullets[index];

            bulletModel = new BulletModel(bulletScriptableObject);

            bulletController = new BulletController(bulletModel, bulletView,Layer);
            bulletsCreated.Add(bulletController);

            return bulletController;
        }

       
        public void DestroyBullet(BulletController bulletController)
        {
            this.bulletController.bulletDestroy();
        }
 }
}
