using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamangerAndDeath : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem par;
    public void Awake() {
        par = transform.GetComponent<ParticleSystem>();  
    } 
    public void PlayDeathSequence() {
        transform.gameObject.GetComponent<PlayerShootAction>().Line.enabled = false;
        transform.gameObject.GetComponent<PlayerShootAction>().Line2.enabled = false;
        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.gameObject.GetComponent<MeshCollider>().enabled = false;
        transform.gameObject.GetComponent<PlayerShootAction>().enabled = false;
        transform.gameObject.GetComponent<PlayerMovement>().enabled = false;

        UIManager manage = GameObject.Find("Canvas").GetComponent<UIManager>();
        manage.GameOverScreen();
    }
    public void TakeDamageSequence() {
        UIManager.Lives--;
        par.Play();
    }
}
