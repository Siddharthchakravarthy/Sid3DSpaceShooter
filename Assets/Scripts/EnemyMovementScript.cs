using UnityEngine;
using System.Text.RegularExpressions;

public class EnemyMovementScript : MonoBehaviour
{
    public float E_MoveSpeed = 4.0f;
    [SerializeField] private AudioClip BlastAudio;

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
            UIManager.RemainingShipsLetIntoTheAtmosphere--;
            if(UIManager.RemainingShipsLetIntoTheAtmosphere == 0) {
                UIManager manage = GameObject.Find("Canvas").GetComponent<UIManager>();
                manage.GameOverScreen();
            }
        }
    }
    void OnTriggerEnter(Collider Other) {
        if(Other.tag == "Player" && UIManager.Lives <= 0) {
            Other.GetComponent<PlayerDamangerAndDeath>().PlayDeathSequence();
        }
        else if(Other.tag == "Player"){
            Other.GetComponent<PlayerDamangerAndDeath>().TakeDamageSequence();
            AudioSource.PlayClipAtPoint(BlastAudio, Other.transform.position);
            Enemy.ReturnToPool(this);
        }
        if(Other.tag == "Lazer") {
            UIManager.Score++;
            AudioSource.PlayClipAtPoint(BlastAudio, transform.position);
            AudioSource.PlayClipAtPoint(BlastAudio, transform.position);
            AudioSource.PlayClipAtPoint(BlastAudio, transform.position);
            AudioSource.PlayClipAtPoint(BlastAudio, transform.position);
            AudioSource.PlayClipAtPoint(BlastAudio, transform.position);
            Enemy.ReturnToPool(this);
        }
    }
}
