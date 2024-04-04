using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    GameObject settingsPanel;

    public void ResumeGame()
    {
        transform.parent.gameObject.SetActive(false);
        FindObjectOfType<Movement>().UnfreezeMovement();
        FindObjectOfType<Rotation>().enabled = true;
        FindObjectOfType<MineInteraction>().enabled = true;
        FindObjectOfType<GameManager>().IsPauseMenuOpened = false;
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
