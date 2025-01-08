using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour {
    private float speed;
    private float explosionTimer;
    private bool isDead = false;
    public GameObject explosion;
    public AudioSource source;
    public AudioClip clip;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Wall") {
            Death();
        }
        if (collision.collider.tag == "Ball") {
            source.PlayOneShot(clip);
            Score("Player");
            Death();
        }
        if (collision.collider.tag == "Enemy") {
            source.PlayOneShot(clip);
            Score("Enemy");
            Death();
        }
        if (collision.collider.tag == "Box") {
            source.PlayOneShot(clip);
            Score("Box");
            Death();
        }
        if (collision.collider.tag == "Friend") {
            source.PlayOneShot(clip);
            Score("Friend");
            Death();
        }
    }
    void Start() {
        speed = UnityEngine.Random.Range(1, 6);
    }
    void Update() {
        if (explosionTimer >= 0) {
            explosionTimer -= Time.deltaTime;
        }
        if (explosionTimer <= 0 && isDead) {
            Destroy(gameObject);
        }
        if (Time.timeScale == 0) {
            Destroy(gameObject);
        }
    }
    void FixedUpdate() {
        if (isDead == false) {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
    }
    private void Score(string reason) {
        if (reason == "Player") {
            PlayerPrefs.SetInt("FriendKills", PlayerPrefs.GetInt("FriendKills") + 1);
            PlayerPrefs.SetInt("Kills", PlayerPrefs.GetInt("Kills") - 1);
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - 2.25f);
        }
        else if (reason == "Enemy") {
            PlayerPrefs.SetFloat("Crashes", PlayerPrefs.GetFloat("Crashes") + 0.5f);
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - 2.25f);
        }
        else {
            PlayerPrefs.SetFloat("Crashes", PlayerPrefs.GetFloat("Crashes") + 0.5f);
        }
    }
    private void Death() {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        var col = gameObject.GetComponent<Collider2D>();
        Destroy(col);
        Destroy(rb);
        explosion.SetActive(true);
        isDead = true;
        explosionTimer = 2f;
    }
}
