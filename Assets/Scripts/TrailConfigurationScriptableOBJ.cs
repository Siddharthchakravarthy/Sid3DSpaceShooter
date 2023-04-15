using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Trail Configurations", menuName = "Guns/Trail Config", order = 3)]
public class TrailConfigurationScriptableOBJ : ScriptableObject
{
    public Material m_Material;
    public AnimationCurve m_WidthCurve;
    public float m_Duration = 0.5f;
    public float m_MinVertexDistance = 0.1f;
    public Gradient m_Color;
    public float m_MissDistance;
    public float m_SimulationSpeed = 100f;
}
