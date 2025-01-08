using UnityEngine;
public class Player : MonoBehaviour {
    private Camera cam;
    public GameObject ballPrefab;
    public GameObject cursor;
    public GameObject[] cooldown0;
    public GameObject[] cooldown1;
    public AudioSource source;
    public AudioClip clip;
    private bool canShoot;
    private float timer;
    private string pickedBuff;
    private float slowShipsTimer;
    private float fasterFirerateTimer;
    private bool fasterFireratePurchased = false;
    private void Start() {
        PlayerPrefs.SetInt("slowShipsPurchased", 0);
        cam = Camera.main;
        pickedBuff = "fasterFirerate";
    }
    private void Update() {
        if (Input.GetKey(KeyCode.Mouse0) && canShoot) {
            source.PlayOneShot(clip);
            var ball = Instantiate(ballPrefab, transform.position + new Vector3(0f, 0f, 1f), transform.rotation);
            if (fasterFireratePurchased) {
                timer = 0.1f;
            } else {
                timer = 0.7f;
            }
        }
        if (slowShipsTimer > 0) {
            slowShipsTimer -= Time.deltaTime;
        } else {
            PlayerPrefs.SetInt("slowShipsPurchased", 0);
        }
        if (fasterFirerateTimer > 0) {
            fasterFirerateTimer -= Time.deltaTime;
        } else {
            fasterFireratePurchased = false;
        }
        if (slowShipsTimer > 7.5f) {
            cooldown0[0].SetActive(true);
            cooldown0[1].SetActive(false);
            cooldown0[2].SetActive(false);
            cooldown0[3].SetActive(false);
        }
        else if (slowShipsTimer > 5f) {
            cooldown0[0].SetActive(false);
            cooldown0[1].SetActive(true);
            cooldown0[2].SetActive(false);
            cooldown0[3].SetActive(false);
        }
        else if (slowShipsTimer > 2.5f) {
            cooldown0[0].SetActive(false);
            cooldown0[1].SetActive(false);
            cooldown0[2].SetActive(true);
            cooldown0[3].SetActive(false);
        }
        else if (slowShipsTimer > 0.1f) {
            cooldown0[0].SetActive(false);
            cooldown0[1].SetActive(false);
            cooldown0[2].SetActive(false);
            cooldown0[3].SetActive(true);
        }
        else {
            cooldown0[0].SetActive(false);
            cooldown0[1].SetActive(false);
            cooldown0[2].SetActive(false);
            cooldown0[3].SetActive(false);
        }
        if (fasterFirerateTimer > 7.5f) {
            cooldown1[0].SetActive(true);
            cooldown1[1].SetActive(false);
            cooldown1[2].SetActive(false);
            cooldown1[3].SetActive(false);
        } else if (fasterFirerateTimer > 5f) {
            cooldown1[0].SetActive(false);
            cooldown1[1].SetActive(true);
            cooldown1[2].SetActive(false);
            cooldown1[3].SetActive(false);
        } else if (fasterFirerateTimer > 2.5f) {
            cooldown1[0].SetActive(false);
            cooldown1[1].SetActive(false);
            cooldown1[2].SetActive(true);
            cooldown1[3].SetActive(false);
        } else if (fasterFirerateTimer > 0.1f) {
            cooldown1[0].SetActive(false);
            cooldown1[1].SetActive(false);
            cooldown1[2].SetActive(false);
            cooldown1[3].SetActive(true);
        } else {
            cooldown1[0].SetActive(false);
            cooldown1[1].SetActive(false);
            cooldown1[2].SetActive(false);
            cooldown1[3].SetActive(false);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if (cursor.transform.position.x < -5f) {
                pickedBuff = "fasterFirerate";
                cursor.transform.position += new Vector3(1f, 0f, 0f);
            } else { 
                pickedBuff = "slowShips";
                cursor.transform.position += new Vector3(-1f, 0f, 0f);            
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            if (cursor.transform.position.x > -5f) {
                pickedBuff = "slowShips";
                cursor.transform.position += new Vector3(-1f, 0f, 0f);
            }
            else {
                pickedBuff = "fasterFirerate";
                cursor.transform.position += new Vector3(1f, 0f, 0f);
            }
        }
        if (Input.GetKey(KeyCode.Mouse1) && PlayerPrefs.GetFloat("Cash") >= 10f) {
            if (pickedBuff == "fasterFirerate" && fasterFirerateTimer <= 0) {
                PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - 10f);
                fasterFirerateTimer = 10f;
                fasterFireratePurchased = true;
            } else {
                if (pickedBuff == "slowShips" && slowShipsTimer <= 0) {
                    PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - 10f);
                    slowShipsTimer = 10f;
                    PlayerPrefs.SetInt("slowShipsPurchased", 1);
                }
            }
        }
        Vector3 mousePosition = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this.transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        Debug.DrawLine(transform.position, mousePosition, Color.white, Time.deltaTime);
        if (timer <= 0f) {
            canShoot = true;
        } else {
            canShoot = false;
            timer -= Time.deltaTime;
        }
    }
}