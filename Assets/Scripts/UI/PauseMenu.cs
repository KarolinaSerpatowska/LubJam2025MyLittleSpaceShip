using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    private void Start()
    {
        InputHandler.Instance.OnPauseAction.AddListener(EnablePause);
    }

    private void EnablePause()
    {
        Debug.Log("HERE");
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void DisablePause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
