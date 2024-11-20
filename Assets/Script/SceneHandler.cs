using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject toggleGameObject;

    private bool isPaused = false;

    public void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void QuitNow()
    {
        Application.Quit();
    }

    public void SetGameObjectActive(GameObject target, bool isActive)
    {
        if (target != null)
        {
            target.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("SetGameObjectActive was called with a null GameObject reference.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameObject();
        }
    }

    private void ToggleGameObject()
    {
        if (toggleGameObject != null)
        {
            isPaused = !isPaused;
            toggleGameObject.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }
        else
        {
            Debug.LogWarning("ToggleGameObject is called, but toggleGameObject is not assigned.");
        }
    }
}
