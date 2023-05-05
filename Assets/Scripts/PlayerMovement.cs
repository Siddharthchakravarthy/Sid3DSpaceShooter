using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed = 7.0f;
    public Joystick joystick;
    public Joystick joyStickVertical;
    public Camera Caamera;
    public Transform Lazer;
    public Transform HandleOfVerticalJoyStick;
    public float TimeBetweenShots = 1.0f;
    public float TimerBetweenShots;
    public bool canShootFromThisScript;

    //constant size of Camera
    public float ultimateZSizeForwardLimit = 2.0f;
    public static float ultimateZSizeBackwardLimit = -8.0f;
    public float ultimateYTopSize = 4.4f;
    


    //Dynamic size of the camera
    public float xDynamicSizeOfLeft;
    public float insideTheBoxLeft;
    public float xDynamicSizeOfRight;
    public float insideTheBoxRight;
    public float YBottomSize;

    //Debug
    public float heightofFrustum;
    public float aspectratio;
    public float yDynamicHeightOfTheFrustum;
    public float playerScale;

    public float yDynamicSizeOfBottom;
    public float DistanceOftheFCP;
    void Start() {
        playerScale = transform.localScale.x;
    }
    void Update()
    {

        heightofFrustum = 2 * (Caamera.transform.position - transform.position).z * Mathf.Tan((Caamera.fieldOfView/2) * Mathf.Deg2Rad);
        // xDynamicSizeOfRight = ((heightofFrustum * Caamera.aspect)/2) + (transform.localScale.x/2);
        xDynamicSizeOfRight = -((heightofFrustum/Caamera.pixelHeight) * Caamera.pixelWidth)/2 + playerScale;
        xDynamicSizeOfLeft = -xDynamicSizeOfRight;


        DistanceOftheFCP = (transform.position - Caamera.transform.position).z;
        yDynamicHeightOfTheFrustum = 2 * DistanceOftheFCP * Mathf.Tan((Caamera.fieldOfView/2) * Mathf.Deg2Rad);
        yDynamicSizeOfBottom = yDynamicHeightOfTheFrustum/2 - Caamera.transform.position.y;
        Movement();
        // if(Input.GetKey(KeyCode.Space) && canShootFromThisScript) {
        //     Shoot();
        // }
        
    }
    private void Movement() {
        float horizontalInput = 0;
        float forwardAndBackwardInput = 0;
        float VerticalInput = 0;
        if(joystick.Horizontal > 0.3f) {
            horizontalInput = 1;
        }
        if (joystick.Horizontal < -0.3f){
            horizontalInput = -1;
        }
        if(joystick.Vertical > 0.3f) {  
            forwardAndBackwardInput = 1;
        }
        if(joystick.Vertical < -0.3f){
            forwardAndBackwardInput = -1;
        }
        //this is done to keep the handle from moving sideways
        if(joyStickVertical.Horizontal > 0 || joyStickVertical.Horizontal < 0) {
            HandleOfVerticalJoyStick.position = Vector3.right * HandleOfVerticalJoyStick.parent.position.x + Vector3.up * HandleOfVerticalJoyStick.position.y;
        }

        if(joyStickVertical.Vertical > 0.1f) {
            VerticalInput = 1;
        }
        if(joyStickVertical.Vertical < -0.1f) {
            VerticalInput = -1;
        }
        // print(horizontalInput);
        // Vector3 Move = new Vector3(0,0,0);
        // if(horizontalInput == -1 || horizontalInput == 1 || Input.GetKey(KeyCode.W) || forwardAndBackwardInput == -1) {
            // Move = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");
            
        // }
        insideTheBoxRight = xDynamicSizeOfRight - 1.0f;
        insideTheBoxLeft = xDynamicSizeOfLeft + 1.0f;

        if  (   (transform.position.x >= insideTheBoxRight) && 
                (transform.position.x <= xDynamicSizeOfRight) &&
                ( (forwardAndBackwardInput == 1 || forwardAndBackwardInput == -1) && 
                    ( !(horizontalInput == 1) && !(horizontalInput == -1) )
                )
            )
        {
            if(forwardAndBackwardInput == 1) {
                transform.rotation = Quaternion.Euler(
                    Mathf.LerpAngle(transform.eulerAngles.x, 20.0f, 0.1f), 
                    0, 
                    Mathf.LerpAngle(transform.eulerAngles.z, -20.0f, 0.1f)
                );
            }
            else if (forwardAndBackwardInput == -1) {
                transform.rotation = Quaternion.Euler(
                    Mathf.LerpAngle(transform.eulerAngles.x, -20.0f, 0.1f), 
                    0, 
                    Mathf.LerpAngle(transform.eulerAngles.z, 20.0f, 0.1f)
                );
            }
            else {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.1f);
            }    
            transform.Translate((Vector3.up * VerticalInput + Vector3.forward * forwardAndBackwardInput) * Speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward * 20.0f * Time.deltaTime, Space.World);
            transform.position = new Vector3(insideTheBoxRight + 0.3f, Mathf.Clamp(transform.position.y, 1.00f, ultimateYTopSize), Mathf.Clamp(transform.position.z, ultimateZSizeBackwardLimit, ultimateZSizeForwardLimit));
        }
        else if( ( transform.position.x <= insideTheBoxLeft ) && 
            ( transform.position.x >= xDynamicSizeOfLeft ) &&
            ( (forwardAndBackwardInput == 1 || forwardAndBackwardInput == -1) && (!(horizontalInput == -1) && !(horizontalInput == 1)) )
        )
        {
            if(forwardAndBackwardInput == 1) {
                transform.rotation = Quaternion.Euler(
                    Mathf.LerpAngle(transform.eulerAngles.x, 20.0f, 0.1f), 
                    0, 
                    Mathf.LerpAngle(transform.eulerAngles.z, 20.0f, 0.1f)
                );
            }
            else if (forwardAndBackwardInput == -1) {
                transform.rotation = Quaternion.Euler(
                    Mathf.LerpAngle(transform.eulerAngles.x, -20.0f, 0.1f), 
                    0, 
                    Mathf.LerpAngle(transform.eulerAngles.z, -20.0f, 0.1f)
                );
            }
            else {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.1f);
            }
            transform.Translate((Vector3.up * VerticalInput + Vector3.forward * forwardAndBackwardInput) * Speed * Time.deltaTime, Space.World);
            transform.position = new Vector3(insideTheBoxLeft - 0.3f, Mathf.Clamp(transform.position.y, 1.00f, ultimateYTopSize), Mathf.Clamp(transform.position.z, ultimateZSizeBackwardLimit, ultimateZSizeForwardLimit));
        }
        else if(transform.position.x > xDynamicSizeOfRight + (playerScale + 1)) {
            transform.position = new Vector3(xDynamicSizeOfLeft - (playerScale - 0.5f), Mathf.Clamp(transform.position.y, 1.00f, ultimateYTopSize), Mathf.Clamp(transform.position.z, ultimateZSizeBackwardLimit, ultimateZSizeForwardLimit));
            transform.Translate((Vector3.right * horizontalInput + Vector3.up * VerticalInput + Vector3.forward * forwardAndBackwardInput) * Speed * Time.deltaTime, Space.World);
        }
        else if(transform.position.x < xDynamicSizeOfLeft - (playerScale + 1)) {
            transform.position = new Vector3(xDynamicSizeOfRight + (playerScale - 0.5f), Mathf.Clamp(transform.position.y,1.00f, ultimateYTopSize), Mathf.Clamp(transform.position.z, ultimateZSizeBackwardLimit, ultimateZSizeForwardLimit));
            transform.Translate((Vector3.right * horizontalInput + Vector3.up * VerticalInput + Vector3.forward * forwardAndBackwardInput) * Speed * Time.deltaTime, Space.World);
        }
        else {
            if(forwardAndBackwardInput == 1 && horizontalInput == 1) {
                transform.rotation = Quaternion.Euler(
                    Mathf.LerpAngle(transform.eulerAngles.x, 20.0f, 0.08f), 
                    0, 
                    Mathf.LerpAngle(transform.eulerAngles.z, -20.0f, 0.08f)
                );
            }
            else if(forwardAndBackwardInput == 1 && horizontalInput == -1) {
                transform.rotation = Quaternion.Euler(
                    Mathf.LerpAngle(transform.eulerAngles.x, 20.0f, 0.08f), 
                    0,
                    Mathf.LerpAngle(transform.eulerAngles.z, 20.0f, 0.08f)
                );
            }
            else if(forwardAndBackwardInput == -1 && horizontalInput == 1) {
                transform.rotation = Quaternion.Euler(
                    Mathf.LerpAngle(transform.eulerAngles.x, -20.0f, 0.08f), 
                    0, 
                    Mathf.LerpAngle(transform.eulerAngles.z, -20.0f, 0.08f)
                );
            }
            else if(forwardAndBackwardInput == -1 && horizontalInput == -1) {
                transform.rotation = Quaternion.Euler(
                    Mathf.LerpAngle(transform.eulerAngles.x, -20.0f, 0.08f), 
                    0, 
                    Mathf.LerpAngle(transform.eulerAngles.z, 20.0f, 0.08f)
                );
            }
            else if(horizontalInput == -1) {
                transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(transform.eulerAngles.z, 20.0f, 0.08f), Vector3.forward);
            }
            else if(horizontalInput == 1) {
                transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(transform.eulerAngles.z, -20.0f, 0.08f), Vector3.forward);
            }
            else if(forwardAndBackwardInput == 1) {
                transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(transform.eulerAngles.x, 20.0f, 0.08f), Vector3.right);
            }
            else if (forwardAndBackwardInput == -1) {
                transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(transform.eulerAngles.x, -20.0f, 0.08f), Vector3.right);
            }
            else {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.1f);
            }
            transform.Translate((Vector3.right * horizontalInput + Vector3.up * VerticalInput + Vector3.forward * forwardAndBackwardInput) * Speed * Time.deltaTime, Space.World);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 1.00f, ultimateYTopSize), Mathf.Clamp(transform.position.z, ultimateZSizeBackwardLimit, ultimateZSizeForwardLimit));
        }
        
    }

    // private void Shoot() {

    //     TimerBetweenShots = TimerBetweenShots + Time.deltaTime;
    //     if(TimerBetweenShots >= TimeBetweenShots){
    //         TimerBetweenShots = 0.0f;
    //         Instantiate(Lazer, transform.position, Lazer.rotation);
    //         // Invoke(nameof(ResetShot), TimeBetweenShots);
    //     }
        
    // }
    // private void ResetShot() {
    //     TimerBetweenShots = 0.0f;
    // }

}
