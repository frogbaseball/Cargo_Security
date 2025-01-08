using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Vector3 mousePos;
    private Camera cam;
    private Rigidbody2D rb;
    public float force;
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
    private void Start() {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }
    private void Update() {
        if (Time.timeScale == 0) {
            Destroy(gameObject);
        }
    }
}
