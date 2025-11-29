using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameOverUI;
    public GameObject winUI;

    [Header("Win Condition")]
    public int coinRequired = 14;

    public bool isGameActive = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isGameActive = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (winUI != null) winUI.SetActive(false);
    }

    private void Update()
    {
        bool anyMenuOpen = (gameOverUI != null && gameOverUI.activeInHierarchy)
                        || (winUI != null && winUI.activeInHierarchy);

        if (anyMenuOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        if (gameOverUI != null) gameOverUI.SetActive(true);
    }

    public void Finish(int currentCoin)
    {
        if (!isGameActive) return;

        if (currentCoin < coinRequired)
        {
            Debug.Log($"ยังชนะไม่ได้ เหรียญ {currentCoin}/{coinRequired}");
            return;
        }

        isGameActive = false;
        Debug.Log("YOU WIN!");
        if (winUI != null) winUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
