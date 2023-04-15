using System.Collections;
using UnityEngine;
using System.Text.RegularExpressions;

public class EnemyMovementScript : MonoBehaviour
{
    public float E_MoveSpeed = 4.0f;
    
    void Start() {
        transform.localEulerAngles = Vector3.up * 180.0f;
        Regex regex = new Regex(("^2|^11"));
        if(regex.Match(transform.name).Success) {
            transform.localScale = new Vector3(0.080762f, 0.1f, 0.080762f);
        }
        else {
            transform.localScale = new Vector3(0.080762f, 0.2f, 0.080762f);
        }
    }
    void Update()
    {
        transform.Translate(Vector3.back * E_MoveSpeed * Time.deltaTime, Space.World);
        if(transform.position.z < PlayerMovement.ultimateZSizeBackwardLimit - 1) {
            Enemy.ReturnToPool(this);
        }
    }
    void OnTriggerEnter(Collider Other) {
        if(Other.tag == "Player") {
            Other.gameObject.GetComponent<PlayerShootAction>().Line.enabled = false;
            Other.gameObject.GetComponent<PlayerShootAction>().Line2.enabled = false;
            Other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Other.gameObject.GetComponent<MeshCollider>().enabled = false;
            Other.gameObject.GetComponent<PlayerShootAction>().enabled = false;
            Other.gameObject.GetComponent<PlayerMovement>().enabled = false;
    
            UIManager manage = GameObject.Find("Canvas").GetComponent<UIManager>();
            manage.GameOverScreen();
        }
        if(Other.tag == "Lazer") {
            UIManager.Score++;
            Enemy.ReturnToPool(this);
        }
    }
}
