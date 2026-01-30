using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    int score;
    public Text scoreText;
    bool isGamePaused;

    public GameObject deathUI;
    public TextMeshProUGUI deathScore;
    public TextMeshProUGUI highScore;
    public int highestScore;

    public GameObject gameUI;
    public GameObject pauseUI;

    public AudioSource backgroundMusic;
    public Slider bgSlider;
    public AudioSource sound;
    public AudioClip destroySound;
    public Slider soundSlider;

    private void Start()
    {
        highestScore = PlayerPrefs.GetInt("HighScore");
        backgroundMusic.volume = PlayerPrefs.GetInt("BgSound");
        sound.volume = PlayerPrefs.GetInt("OtherSound");
        highScore.text = highestScore.ToString();
    }
    public void ScoreManager()
    {
        score += 10;
        scoreText.text = score.ToString();
    }

    public void GamePaused()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0f;
            pauseUI.SetActive(true);
            gameUI.SetActive(false);
            isGamePaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
            gameUI.SetActive(true);
            isGamePaused = false;
        }
    }

    public void Death()
    {
        Time.timeScale = 0f;
        CheckHighScore();
        StartCoroutine(DeathWait());
        
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
    
    public void HomeButton()
    {
        SceneManager.LoadScene("Home");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CheckHighScore()
    {
        if(score > highestScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = score.ToString();
        }
    }

    public void BgMusic()
    {
        backgroundMusic.volume = bgSlider.value;
        PlayerPrefs.SetInt("BgSound", (int)bgSlider.value);
        if(bgSlider.value == 1)
        {
            Color green;
            if (ColorUtility.TryParseHtmlString("#08FF00", out green))
            {
                bgSlider.transform.GetChild(0).GetComponent<Image>().color = green;
            }
        }
        else
        {
            Color red;
            if (ColorUtility.TryParseHtmlString("#FF0000", out red))
            {
                bgSlider.transform.GetChild(0).GetComponent<Image>().color = red;
            }
        }
    }

    public void OtherSounds()
    {
        sound.volume = soundSlider.value;
        PlayerPrefs.SetInt("OtherSound", (int)soundSlider.value);
        if (soundSlider.value == 1)
        {
            Color green;
            if (ColorUtility.TryParseHtmlString("#08FF00", out green))
            {
                soundSlider.transform.GetChild(0).GetComponent<Image>().color = green;
            }
        }
        else
        {
            Color red;
            if (ColorUtility.TryParseHtmlString("#FF0000", out red))
            {
                soundSlider.transform.GetChild(0).GetComponent<Image>().color = red;
            }
        }
    }
    IEnumerator DeathWait()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(3f);
        gameUI.SetActive(false);
        deathUI.SetActive(true);
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        deathScore.text = score.ToString();
    }
}
