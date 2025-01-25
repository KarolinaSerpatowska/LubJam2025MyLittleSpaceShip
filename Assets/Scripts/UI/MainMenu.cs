using UnityEngine;

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
        Application.LoadLevel(0);
    }

    public void OnAuthorsButton()
    {
        Debug.Log("authors");
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
