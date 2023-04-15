using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer_Behaviour : MonoBehaviour
{
    public float Speed = 40;
    public float DestroyTime = 10.0f;
    public float timer = 0.0f;
    // public ParticleSystem particle;
    public int seconds;
    public void printer(float seconds) {
        print(seconds);
    }

    // void Start() {
    //     transform.GetComponent<ParticleSystem>().Stop();
    //     // ParticleSystem partic = Instantiate(particle);
    //     // partic.gameObject.SetActive(false);
    //     // partic.transform.SetParent(transform);
    //     // partic.transform.position = Vector3.zero;
    // }
    // void Update()
    // {
    //     transform.Translate(Vector3.forward * 40 * Time.deltaTime, Space.World);
    //     timer = timer + Time.deltaTime;
    //     seconds = Mathf.FloorToInt(timer % 60);
    //     if(seconds > DestroyTime) {
    //         Destroy(this.gameObject);
    //         Destroy(this);
    //     }
        // transform.GetComponent<ParticleSystem>().Stop();
    // }

    private void OnTriggerEnter(Collider Other) {
        if(Other.tag == "Enemy") {
            // ParticleSystem par = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
            // par.gameObject.SetActive(true);
            // // par.Play();
            // StartCoroutine(WaitForIt(par));
            ParticleSystem par = transform.GetComponent<ParticleSystem>();
            par.Play();
            // StartCoroutine(WaitForIt(par));
        }
    }
    // IEnumerator WaitForIt(ParticleSystem par) {
    //     yield return new WaitForSeconds(3.0f);
    //     par.Stop();
    // }
}
