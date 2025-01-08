using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoxScript : MonoBehaviour {
    //The reason why this is called "BoxScript" instead of "Box" is because of a bug in unity, it just doesn't let me :/
    //Error message: Can't add script component 'Box' because the script class cannot be found. Make sure that there are
    //no compile errors and that the file name and class name match.
    private float speed = 1;
    private float explosionTimer;
    private bool isDead = false;
    public GameObject explosion;
    public GameObject sprite;
    private int random;
    private int randomCash;
    public AudioSource source;
    public AudioClip clip;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Wall") {
            Death();
        }
        if (collision.collider.tag == "Ball") {
            source.PlayOneShot(clip);
            GetCash();
            Death();
        }
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Friend") {
            source.PlayOneShot(clip);
            Score();
            Death();
        }
    }
    void Start() {
        random = UnityEngine.Random.Range(0, 2);
        randomCash = UnityEngine.Random.Range(1, 7);
        var renderer = sprite.GetComponent<SpriteRenderer>();
        if (random == 1) {
            renderer.color = new Color32(101, 74, 48, 255); //color of friendly ship
        } else {
            renderer.color = new Color32(149, 82, 49, 255); //color of enemy ship
        }
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
    private void Score() {
        PlayerPrefs.SetFloat("Crashes", PlayerPrefs.GetFloat("Crashes") + 0.5f);
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
    private void GetCash() {
        float realCash = randomCash;
        PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + realCash);
    }
}
