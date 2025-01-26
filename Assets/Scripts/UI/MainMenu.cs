using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject authorsPanel;
    [SerializeField] private GameObject mainMenuPanel;

    private void Awake()
    {
        authorsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnAuthorsButton()
    {
        authorsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnBackButton()
    {
        authorsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
