using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameOverUI;   // หน้าตาย
    public GameObject winUI;        // หน้า Win (Finish แล้วเหรียญครบ)

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

    // ---------- GAME OVER ----------
    public void GameOver()
    {
        isGameActive = false;
        if (gameOverUI != null) gameOverUI.SetActive(true);
    }

    // ---------- WIN ----------
    public void Finish(int currentCoin)
    {
        if (!isGameActive) return;

        // ถ้าเหรียญไม่ถึงยังไม่ให้ชนะ
        if (currentCoin < coinRequired)
        {
            Debug.Log($"ยังชนะไม่ได้ เหรียญ {currentCoin}/{coinRequired}");
            return;
        }

        isGameActive = false;
        Debug.Log("YOU WIN!");
        if (winUI != null) winUI.SetActive(true);
    }

    // ---------- ปุ่ม UI ----------
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // เปลี่ยนชื่อให้ตรงกับฉากจริง
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
