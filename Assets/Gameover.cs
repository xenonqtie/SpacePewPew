using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover: MonoBehaviour
{
    public GameObject gameOverUI;
    public AudioClip gameoverSound;
    private AudioSource audioSource;

    public Image blackBackGround;
    public float fadeDuration = 1f;

    private bool isGameOver = false;

    void Start()
    {
        gameOverUI.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (blackBackGround != null)
        {
            blackBackGround.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOverSequence());
        }
    }

    IEnumerator GameOverSequence()
    {
        if (gameoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameoverSound);
        }

        if (blackBackGround != null)
        {
            blackBackGround.gameObject.SetActive(true);
            Color color = blackBackGround.color;
            float timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.unscaledDeltaTime;
                color.a = Mathf.Clamp01(timer / fadeDuration);
                blackBackGround.color = color;
                yield return null;
            }

            blackBackGround.color = new Color(color.r, color.g, color.b, 1f);

        }

        gameOverUI.SetActive(true);
        Time.timeScale = 0f; 
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
