using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[DisallowMultipleComponent]
public class PlayerShootAction : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction ShootingAction;
    private InputAction TouchPress;


    [SerializeField]
    public GunType Type;
    
    public Joystick FloatingJoystickForShooting;

    
    [SerializeField]
    public List<GunScriptableObject> Guns;

    public LineRenderer Line;
    public LineRenderer Line2;

    [Space]
    [Header("Runtime Filled")]
    public GunScriptableObject ActiveGun;
    public int PowerUpSeconds = 0;


    void Awake() {
        playerInput = GetComponent<PlayerInput>();
        ShootingAction = playerInput.actions.FindAction("Shooting");
        // TouchPress = playerInput.actions.FindAction("TouchPress");
    }
    void Start()
    {
        GunScriptableObject gun = Guns.Find(gun => gun.m_Type == Type);
        ActiveGun = gun;
        ActiveGun.Spawn(this);
        
    }

    // private void OnEnable() {
    //     TouchPress.performed += ShootingActionPosition;
    // }

    // private void OnDisable() {
    //     TouchPress.performed -= ShootingActionPosition; 
    // }

    // private void ShootingActionPosition(InputAction.CallbackContext context) {
    //     Vector3 sid = Camera.main.ScreenToWorldPoint(ShootingAction.ReadValue<Vector2>());
    //     sid.z = 0;
        
    // }


    void Update()
    {
        bool canShoot = false;
        // if(ShootingAction.ReadValue<Vector2>().x > 700.0f) {
        //     canShoot = true;
        // }
        if(FloatingJoystickForShooting.Horizontal > 0 || FloatingJoystickForShooting.Vertical < 0 
        || FloatingJoystickForShooting.Horizontal < 0 || FloatingJoystickForShooting.Vertical > 0) {
            canShoot = true;
        }
        if(canShoot) {
            ActiveGun.Shoot();
            canShoot = false;
        }
        
        

        if(PowerUpHelper.CanUsePowerUp && Time.time > PowerUpSeconds + PowerUpHelper.PowerUpUseTime) {
            PowerUpHelper.CanUsePowerUp = false;
            GunType b = GunType.LazerPrefab;
            // PlayerShootAction player = GameObject.Find("Player").GetComponent<PlayerShootAction>();
            // i did this to get practice in the Find function
            GunScriptableObject gun = GameObject.Find("Player").GetComponent<PlayerShootAction>().Guns.Find(gun => gun.m_Type == b);
            ActiveGun = gun;
            ActiveGun.Spawn(this);
        }
        

        if( Physics.Raycast(transform.position, 
        transform.forward.normalized, out RaycastHit hit, float.MaxValue, 
        ActiveGun.shootConfig.m_Hitmask
        ) )
        {
            Vector3 hit1 = hit.point - (Vector3.forward * 1) - (Vector3.right * 1);
            Vector3 hit2 = hit1 + (Vector3.right * 2.0f);

            float HalfWay = (hit1.x - hit2.x)/2;
            Vector3 hit3 = new Vector3( (hit1.x - HalfWay), hit1.y, hit1.z) + Vector3.up * 1;
            Vector3 hit4 = hit3 - (Vector3.up * 2.0f);

            Line.SetPosition(0, hit1);
            Line.SetPosition(1, hit2);
            Line2.SetPosition(0, hit3);
            Line2.SetPosition(1, hit4);
        }
        else {
            // Vector3 Direction = ( (10 * Vector3.forward + transform.position) - transform.position ).normalized;
            Vector3 DistanceForward = transform.position + Vector3.forward * 15.0f;
            
            Vector3 hit1 = DistanceForward - (Vector3.forward * 1) - (Vector3.right * 1);
            Vector3 hit2 = hit1 + (Vector3.right * 2.0f);

            float HalfWay = (hit1.x - hit2.x)/2;
            Vector3 hit3 = new Vector3( (hit1.x - HalfWay), hit1.y, hit1.z ) + Vector3.up * 1;
            Vector3 hit4 = hit3 - (Vector3.up * 2.0f);
            Line.SetPosition(0, hit1);
            Line.SetPosition(1, hit2);
            Line2.SetPosition(0, hit3);
            Line2.SetPosition(1, hit4);
        }
    }
}
