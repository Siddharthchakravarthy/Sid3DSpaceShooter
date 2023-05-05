using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]
public class GunScriptableObject : ScriptableObject
{
    //idu type of the gun
    public GunType m_Type;

    public ShootConfigurationScriptableOBJ shootConfig;
    public TrailConfigurationScriptableOBJ trailConfig;
    public List<Transform> theAttackPoint;
    private MonoBehaviour ActiveMonoBeh;


    private float LastShootTime;
    private ObjectPool<Lazer_Behaviour> PoolOfLazerPrefabs;
    private ObjectPool<TrailRenderer> PoolOfTR;
    //put in the player only here yaake andre naanu player olage inda ne shoot maadtideeni no theAttackPoint[i]
    // but might add later in the future
    

    public void Spawn(MonoBehaviour ActiveMB) {
        ActiveMonoBeh = ActiveMB;

        theAttackPoint[0] = ActiveMonoBeh.transform.Find("AttackPointLeft");
        theAttackPoint[1] = ActiveMonoBeh.transform.Find("AttackPointRight");

        LastShootTime = 0f;

        PoolOfTR = new ObjectPool<TrailRenderer>(CreateTrail);

        if(shootConfig.isHitScan == false) {
            PoolOfLazerPrefabs = new ObjectPool<Lazer_Behaviour>(CreateLazerPrefabs);
        }

    }
    bool attackPointLeftDone = false;
    public void Shoot() {
        for(int i = 0; i < theAttackPoint.Count; i++) {
            if(Time.time > shootConfig.m_fireRate + LastShootTime &&  i == 0 && attackPointLeftDone == false){
                attackPointLeftDone = true;
                LastShootTime = Time.time;
                Vector3 shootDirection = theAttackPoint[i].transform.forward + new Vector3(
                    Random.Range(-shootConfig.m_Spread.x, shootConfig.m_Spread.x),
                    Random.Range(-shootConfig.m_Spread.y, shootConfig.m_Spread.y),
                    Random.Range(-shootConfig.m_Spread.z, shootConfig.m_Spread.z)
                );
                shootDirection.Normalize();
                if(shootConfig.isHitScan) {
                    if(Physics.Raycast(theAttackPoint[i].transform.position, shootDirection, out RaycastHit hit, 
                        float.MaxValue, shootConfig.m_Hitmask)) {
                        ActiveMonoBeh.StartCoroutine(PlayTrail(theAttackPoint[i].transform.position, hit.point, hit));
                    }
                    else {
                        ActiveMonoBeh.StartCoroutine(PlayTrail(theAttackPoint[i].transform.position, 
                        theAttackPoint[i].transform.position + shootDirection * trailConfig.m_MissDistance,
                        new RaycastHit() ));
                    }
                }
                else if(shootConfig.tripleShot) {
                    ShootTheOddNumber(theAttackPoint[i]);
                }
                else {
                    ShootThePrefab(theAttackPoint[i]);
                } 
            }
            else if(Time.time > shootConfig.m_fireRate + LastShootTime &&  i == 1 && attackPointLeftDone == true){
                attackPointLeftDone = false;
                LastShootTime = Time.time;
                Vector3 shootDirection = theAttackPoint[i].transform.forward + new Vector3(
                    Random.Range(-shootConfig.m_Spread.x, shootConfig.m_Spread.x),
                    Random.Range(-shootConfig.m_Spread.y, shootConfig.m_Spread.y),
                    Random.Range(-shootConfig.m_Spread.z, shootConfig.m_Spread.z)
                );
                shootDirection.Normalize();
                if(shootConfig.isHitScan) {
                    if(Physics.Raycast(theAttackPoint[i].transform.position, shootDirection, out RaycastHit hit, 
                        float.MaxValue, shootConfig.m_Hitmask)) {
                        ActiveMonoBeh.StartCoroutine(PlayTrail(theAttackPoint[i].transform.position, hit.point, hit));
                    }
                    else {
                        ActiveMonoBeh.StartCoroutine(PlayTrail(theAttackPoint[i].transform.position, 
                        theAttackPoint[i].transform.position + shootDirection * trailConfig.m_MissDistance,
                        new RaycastHit() ));
                    }
                }
                else if(shootConfig.tripleShot) {
                    ShootTheOddNumber(theAttackPoint[i]);
                }
                else {
                    ShootThePrefab(theAttackPoint[i]);
                } 
            }
        }
        
    }

    public void ShootThePrefab(Transform theAttackPoint) {
        Lazer_Behaviour lazerPrefab = PoolOfLazerPrefabs.Get();
        lazerPrefab.gameObject.SetActive(true);
        lazerPrefab.seconds = 0;
        lazerPrefab.transform.position = theAttackPoint.transform.position;

        TrailRenderer trail = PoolOfTR.Get();
        trail.transform.SetParent(lazerPrefab.transform, false);
        trail.transform.localPosition = Vector3.zero;
        trail.gameObject.SetActive(true);
        trail.emitting = true;

        ActiveMonoBeh.StartCoroutine(ShootPlusExtra(lazerPrefab, trail));
        
        
    }

    private void ShootTheOddNumber(Transform theAttackPoint) {
        List<Lazer_Behaviour> LazerArr = new List<Lazer_Behaviour>();
        List<TrailRenderer> TrailArr = new List<TrailRenderer>();
        for(int i = 0; i < 5; i++) {
            Lazer_Behaviour lazerPrefab = PoolOfLazerPrefabs.Get();
            TrailRenderer trail = PoolOfTR.Get();

            lazerPrefab.gameObject.SetActive(true);
            lazerPrefab.seconds = 0;
            lazerPrefab.transform.position = theAttackPoint.transform.position;

            trail.transform.SetParent(lazerPrefab.transform, false);
            trail.transform.localPosition = Vector3.zero;
            trail.gameObject.SetActive(true);
            trail.emitting = true;

            LazerArr.Add(lazerPrefab);
            TrailArr.Add(trail);
        }

        ActiveMonoBeh.StartCoroutine(ShootTheOddNumberShot(LazerArr, TrailArr));
    }  

    private IEnumerator ShootTheOddNumberShot(List<Lazer_Behaviour> LazerArr, List<TrailRenderer> TrailArr) {
        float angle = 5.0f;
        for(int i = 0; i < LazerArr.Count; i++) {
            if(i == 0) {
                ActiveMonoBeh.StartCoroutine(ShootPlusExtraOddNumber(LazerArr[i], TrailArr[i], i * angle));
                angle = -angle;
            }
            else {
                ActiveMonoBeh.StartCoroutine(ShootPlusExtraOddNumber(LazerArr[i], TrailArr[i], i % 2 == 0 ? (i-1) * angle : i * angle));
                angle = -angle;
            }
        }
        yield return null;
    }

    private IEnumerator ShootPlusExtraOddNumber(Lazer_Behaviour lazerPrefab, TrailRenderer trail, float angle) {
        //Rotate
        lazerPrefab.transform.Rotate(Vector3.up, angle, Space.World);
        while(lazerPrefab.seconds < lazerPrefab.DestroyTime) {
            lazerPrefab.transform.Translate(Vector3.up * 10 * Time.deltaTime, Space.Self);
            
            // MonoBehaviour.print(lazerPrefab.seconds);
            lazerPrefab.timer = lazerPrefab.timer + Time.deltaTime;
            lazerPrefab.seconds = Mathf.FloorToInt(lazerPrefab.timer % 60);
            yield return null;
            
        }
        lazerPrefab.transform.Rotate(Vector3.up, -angle, Space.World);

        lazerPrefab.timer = 0.0f;
        trail.emitting = false;
        
        lazerPrefab.gameObject.SetActive(false);
        trail.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(4.0f);
        PoolOfTR.Release(trail);
        PoolOfLazerPrefabs.Release(lazerPrefab);
    }

    private IEnumerator ShootPlusExtra(Lazer_Behaviour lazerPrefab, TrailRenderer trail){
        while(lazerPrefab.seconds < lazerPrefab.DestroyTime) {
            
            lazerPrefab.transform.Translate(Vector3.up * 10 * Time.deltaTime, Space.Self);
            
            // MonoBehaviour.print(lazerPrefab.seconds);
            lazerPrefab.timer = lazerPrefab.timer + Time.deltaTime;
            lazerPrefab.seconds = Mathf.FloorToInt(lazerPrefab.timer % 60);
            yield return null;
            
        }
        lazerPrefab.timer = 0.0f;
        trail.emitting = false;

        lazerPrefab.gameObject.SetActive(false);
        trail.gameObject.SetActive(false);
        yield return new WaitForSeconds(4.0f);
        
        PoolOfTR.Release(trail);
        PoolOfLazerPrefabs.Release(lazerPrefab);
    }

    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit hit) {
        TrailRenderer instance = PoolOfTR.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;

        yield return null;
        
        instance.emitting = true;

        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;

        while(remainingDistance > 0) {
            instance.transform.position = Vector3.Lerp(StartPoint, EndPoint, 
                                            Mathf.Clamp01(1 - remainingDistance/distance));
            remainingDistance = remainingDistance - trailConfig.m_SimulationSpeed * Time.deltaTime;
            yield return null;
        }

        instance.transform.position = EndPoint;
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        PoolOfTR.Release(instance);
        
    }
    // private void ResetShot() {
    //     TimerBetweenShots = 0.0f;
    // }

    private TrailRenderer CreateTrail() {
        GameObject instance = new GameObject();
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();

        trail.colorGradient = trailConfig.m_Color;
        trail.material = trailConfig.m_Material;
        trail.time = trailConfig.m_Duration;
        trail.widthCurve = trailConfig.m_WidthCurve;
        trail.minVertexDistance = trailConfig.m_MinVertexDistance;

        trail.emitting = false;

        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        return trail;
    }

    private Lazer_Behaviour CreateLazerPrefabs() {
        return Instantiate(shootConfig.LazerPrefab);
    }
}
