using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour {
    private float speed;
    private float explosionTimer;
    private bool isDead = false;
    public GameObject explosion;
    public AudioSource source;
    public AudioClip clip;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Wall") {
            Score("Miss");
            Death();
        }
        if (collision.collider.tag == "Ball") {
            source.PlayOneShot(clip);
            Score("Player");
            Death();
        }
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Friend" || collision.collider.tag == "Box") {
            source.PlayOneShot(clip);
            Score("Mate");
            Death();
        }
    }
    private void Start() {
        speed = UnityEngine.Random.Range(1, 6);
    }
    private void Update() {
        if (PlayerPrefs.GetInt("slowShipsPurchased") == 1) {
            speed = 1;
        }
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
    private void FixedUpdate() {
        if (isDead == false) {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
    }
    private void Score(string reason) {
        if (reason == "Player") {
            PlayerPrefs.SetInt("Kills", PlayerPrefs.GetInt("Kills") + 1);
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 0.25f);
        }
        else if (reason == "Miss") {
            PlayerPrefs.SetInt("Misses", PlayerPrefs.GetInt("Misses") + 1);
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
