using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shoot Configurations", menuName = "Guns/Shoot Config", order = 2)]
public class ShootConfigurationScriptableOBJ : ScriptableObject
{
   public Lazer_Behaviour LazerPrefab;
   public bool isHitScan = false;
   public LayerMask m_Hitmask;
   public Vector3 m_Spread;
   public float m_fireRate = 0.25f;

   public bool tripleShot;
}
