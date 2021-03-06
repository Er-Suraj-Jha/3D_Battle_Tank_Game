using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObjects", menuName = "ScriptableObjects/NewTankScriptableObjects")]
public class TankScriptableObjects: ScriptableObject
{ 
    public TankType TankType;
    public String TankName;
    public float Speed;
    public float Health;
    public Color color;
    public BulletType bullet;
    public BulletScriptableObject bulletDetails;
   //[] public int myID;
}