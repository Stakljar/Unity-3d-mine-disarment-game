using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Canvas gameEndCanvas;

    [SerializeField]
    Canvas pauseMenuCanvas;

    [SerializeField]
    GameObject settingsPanel;

    [SerializeField]
    TMP_Text endGameText;

    bool isPauseMenuOpened = false;

    public bool IsPauseMenuOpened
    {
        get => isPauseMenuOpened;
        set => isPauseMenuOpened = value;
    }

    bool isGameOver = false;

    public bool IsGameOver
    {
        get => isGameOver;
        set => isGameOver = value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (!isPauseMenuOpened)
            {
                FindObjectOfType<Movement>().FreezeMovement();
                FindObjectOfType<MineInteraction>().enabled = false;
                FindObjectOfType<Rotation>().enabled = false;
                isPauseMenuOpened = true;
                DisplayPauseMenu();
            }
            else
            {
                FindObjectOfType<PauseManager>().ResumeGame();
            }
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame(string endGameText)
    {
        isGameOver = true;
        Cursor.lockState = CursorLockMode.Confined;
        this.endGameText.text = endGameText;
        gameEndCanvas.gameObject.SetActive(true);
    }

    public void DisplayPauseMenu()
    {
        pauseMenuCanvas.gameObject.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
