using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenager : MonoBehaviour
{
    private float spawnRate = 1.5f;

    public Button restartButton;

    public List<GameObject> targets;

    public TextMeshProUGUI scoreText;

    private int score;

    public TextMeshProUGUI gameOverText;

    public bool isGameActive;

    public GameObject titleScreen;

    public int lifes = 3;

    public TextMeshProUGUI lifesText;

    public AudioSource backgroundMusic;

    public Slider volumeScrollbar;

    public GameObject pausedScreen;

    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = backgroundMusic.GetComponent<AudioSource>();
        volumeScrollbar.value = backgroundMusic.volume;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeVolume();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void ChangeVolume()
    {
        backgroundMusic.volume = volumeScrollbar.value;
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLifes(int life)
    {
        lifes = life;
        lifesText.text = "Lifes: " + lifes;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        spawnRate /= difficulty;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLifes(lifes);
    }

    public void PauseGame()
    {
        if (isGameActive)
        {
            isPaused = !isPaused;
            pausedScreen.gameObject.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}
