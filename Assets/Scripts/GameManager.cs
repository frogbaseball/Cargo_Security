using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject game;
    public GameObject player;
    private Player playerScript;
    public GameObject enemySpawners;
    private Spawner spawnerScript;
    public GameObject menu;
    public Image menuBackground;
    public GameObject tutorialMenu;
    public GameObject tutorialMenuPart1;
    public GameObject tutorialMenuPart2;
    public GameObject tutorialMenuPart3;
    public GameObject go;
    public TextMeshProUGUI goText;
    public TextMeshProUGUI shipsKilledMenu;
    public TextMeshProUGUI shipsCrashedMenu;
    public TextMeshProUGUI highestScoreMenu;
    public Button startButton;
    public Button resetButton;
    public Button tutorialToggle;
    public GameObject tutorialToggleSprite;
    public TextMeshProUGUI shipsKilled;
    public TextMeshProUGUI shipsCrashed;
    public TextMeshProUGUI shipsMissed;
    public TextMeshProUGUI friendlyShipsKilled;
    public TextMeshProUGUI cash;
    public GameObject gameOver;
    public Image gameOverBackground;
    public TextMeshProUGUI gameOverTotalScoreText;
    public TextMeshProUGUI gameOverUpperText;
    public TextMeshProUGUI gameOverBottomText;
    public TextMeshProUGUI gameOverKillsCount;
    public TextMeshProUGUI gameOverCrashCount;
    public TextMeshProUGUI gameOverCashCount;
    public Button gameOverMenuButton;
    private bool tutorial = true;
    private bool isGameOver = false;
    private float goCooldown;
    private float transitionCooldown;
    private float transitionGameOverCooldown;
    private int goTurn;
    private int transitionTurn;
    private int transitionGameOverTurn;
    private void Awake() {
        spawnerScript = enemySpawners.GetComponent<Spawner>();
        spawnerScript.enabled = false;
        playerScript = player.GetComponent<Player>();
        playerScript.enabled = false;
        game.SetActive(false);
        tutorialMenu.SetActive(false);
        menu.SetActive(true);
        go.SetActive(false);
        Time.timeScale = 1f;
    }
    private void Update() {
        shipsKilled.text = "Kills: " + PlayerPrefs.GetInt("Kills");
        shipsCrashed.text = "Crashes: " + PlayerPrefs.GetFloat("Crashes");
        friendlyShipsKilled.text = "Friends killed: " + PlayerPrefs.GetInt("FriendKills") + "/5";
        shipsMissed.text = "Misses: " + PlayerPrefs.GetInt("Misses") + "/20";
        cash.text = PlayerPrefs.GetFloat("Cash") + "$";
        if ((PlayerPrefs.GetInt("Misses") >= 20 || PlayerPrefs.GetInt("FriendKills") >= 5) && !isGameOver) {
            Time.timeScale = 0f;
            playerScript.enabled = false;
            GameOver();
        }
        if (goCooldown > 0) {
            goCooldown -= Time.deltaTime;
        }
        if (transitionCooldown > 0) {
            transitionCooldown -= Time.deltaTime;
        }
        if (transitionGameOverCooldown > 0) {
            transitionGameOverCooldown -= Time.unscaledDeltaTime;
        }
        if (goCooldown <= 0f && goTurn == 1) {
            playerScript.enabled = true;
            goText.text = "3";
            goTurn++;
            goCooldown = 0.5f;
        }
        if (goCooldown <= 0f && goTurn == 2) {
            goText.text = "2";
            goCooldown = 0.5f;
            goTurn++;
        }
        if (goCooldown <= 0f && goTurn == 3) {
            goText.text = "1";
            goCooldown = 0.5f;
            goTurn++;
        }
        if (goCooldown <= 0f && goTurn == 4) {
            goText.text = "Start!";
            goCooldown = 0.5f;
            goTurn++;
        }
        if (goCooldown <= 0f && goTurn >= 5) {
            spawnerScript.enabled = true;
            go.SetActive(false);
        }
        if (transitionCooldown <= 0 && transitionTurn == 1) {
            CoolTransition(170, "menu");
            transitionCooldown = 1f;
            transitionTurn++;
        }
        if (transitionCooldown <= 0 && transitionTurn == 2) {
            CoolTransition(85, "menu");
            transitionCooldown = 1f;
            transitionTurn++;
        }
        if (transitionCooldown <= 0 && transitionTurn == 3) {
            CoolTransition(255, "menu");
            StartGame();
            transitionTurn++;
        }
        if (transitionGameOverCooldown <= 0 && transitionGameOverTurn == 1) {
            transitionGameOverCooldown = 1.5f;
            transitionGameOverTurn++;
        }
        if (transitionGameOverCooldown <= 0 && transitionGameOverTurn == 2) {
            gameOver.SetActive(true);
            CoolTransition(85, "gameOver");
            transitionGameOverCooldown = 1.5f;
            transitionGameOverTurn++;
        }
        if (transitionGameOverCooldown <= 0 && transitionGameOverTurn == 3) {
            CoolTransition(170, "gameOver");
            transitionGameOverCooldown = 1.5f;
            transitionGameOverTurn++;
        }
        if (transitionGameOverCooldown <= 0 && transitionGameOverTurn == 4) {
            CoolTransition(255, "gameOver");
            transitionGameOverTurn++;
        }
    }
    private void Start() {
        Score();
    }
    public void SwitchTutorialOnOff() { //for toggle button
        if (tutorial) {
            tutorialToggleSprite.SetActive(true);
            tutorial = false;
        } else {
            tutorialToggleSprite.SetActive(false);
            tutorial = true;
        }
    }
    public void TransitionEnabler() { //Start Button uses this function
        transitionTurn = 1;
        game.SetActive(true);
    }
    public void NextPart(int switchTo) { //for switching parts of tutorial menu
        if (switchTo == 0) {
            tutorialMenuPart1.SetActive(true);
            tutorialMenuPart2.SetActive(false);
            tutorialMenuPart3.SetActive(false);
        } else if (switchTo == 1) {
            tutorialMenuPart1.SetActive(false);
            tutorialMenuPart2.SetActive(true);
            tutorialMenuPart3.SetActive(false);
        } else if (switchTo == 2) {
            tutorialMenuPart1.SetActive(false);
            tutorialMenuPart2.SetActive(false);
            tutorialMenuPart3.SetActive(true);
        } else {
            tutorial = false;
            StartGame();
        }
    }
    private void CoolTransition(byte trans, string target) {
        if (target == "menu") {
            startButton.image.color = new Color32(0, 0, 0, trans);
            resetButton.image.color = new Color32(0, 0, 0, trans);
            tutorialToggle.image.color = new Color32(0, 0, 0, trans);
            shipsKilledMenu.color = new Color32(255, 0, 0, trans);
            shipsCrashedMenu.color = new Color32(255, 0, 0, trans);
            highestScoreMenu.color = new Color32(255, 0, 0, trans);
            menuBackground.color = new Color32(255, 255, 255, trans);
        } else if (target == "gameOver") {
            gameOverBackground.color = new Color32(255, 255, 255, trans);
            gameOverTotalScoreText.color = new Color32(0, 255, 0, trans);
            gameOverUpperText.color = new Color32(0, 0, 0, trans);
            gameOverBottomText.color = new Color32(0, 0, 0, trans);
            gameOverKillsCount.color = new Color32(0, 255, 0, trans);
            gameOverCrashCount.color = new Color32(0, 255, 0, trans);
            gameOverCashCount.color = new Color32(0, 255, 0, trans);
            gameOverMenuButton.image.color = new Color32(0, 0, 0, trans);
        } else {
            Debug.Log("invalid target");
        }
    }
    public void GameOver() {
        PlayerPrefs.SetInt("TotalScore", (PlayerPrefs.GetInt("Kills") * 10));
        isGameOver = true;
        transitionGameOverTurn = 1;
        if (PlayerPrefs.GetInt("FriendKills") >= 5 && PlayerPrefs.GetInt("Misses") >= 15) {
            gameOverUpperText.text = "You have been arrested for: ";
            gameOverBottomText.text = "Being an Enemy Spy";
        } else {
            gameOverUpperText.text = "You have been fired for: ";
            gameOverBottomText.text = "Poor Performance";
        }
        gameOverTotalScoreText.text = "Total Score: " + PlayerPrefs.GetInt("TotalScore");
        gameOverKillsCount.text = "Kills: " + PlayerPrefs.GetInt("Kills");
        gameOverCrashCount.text = "Crashes: " + PlayerPrefs.GetFloat("Crashes");
        gameOverCashCount.text = "Cash: " + PlayerPrefs.GetFloat("Cash") + "$";
    }
    private void StartGame() {
        spawnerScript.enabled = false;
        menu.SetActive(false);
        if (tutorial) {
            tutorialMenu.SetActive(true);
            NextPart(0);
        } else {
            tutorialMenu.SetActive(false);
            go.SetActive(true);
            goTurn = 1;
        }
    }
    public void DestroyScore() { //Reset Button uses this function
        PlayerPrefs.SetInt("Kills", 0);
        PlayerPrefs.SetFloat("Crashes", 0f);
        PlayerPrefs.SetInt("FriendKills", 0);
        PlayerPrefs.SetInt("Misses", 0);
        PlayerPrefs.SetFloat("Cash", 0f);
        Time.timeScale = 1f;
        Score();
    }
    private void Score() {
        shipsKilledMenu.text = "Kills: " + PlayerPrefs.GetInt("Kills");
        shipsCrashedMenu.text = "Crashes: " + PlayerPrefs.GetFloat("Crashes");
        highestScoreMenu.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore");
    }
    public void RestartGame() {
        Debug.Log("Restarting Game");
        if (PlayerPrefs.GetInt("TotalScore") > PlayerPrefs.GetInt("HighestScore")) {
            PlayerPrefs.SetInt("HighestScore", PlayerPrefs.GetInt("TotalScore"));
        }
        DestroyScore();
        gameOver.SetActive(false);
        game.SetActive(false);
        menu.SetActive(true);
        spawnerScript.enabled = false;
        playerScript.enabled = false;
        isGameOver = false;
        goTurn = 0;
        transitionTurn = 0;
        transitionGameOverTurn = 0;
        tutorial = true;
        tutorialToggleSprite.SetActive(false);
    }
}