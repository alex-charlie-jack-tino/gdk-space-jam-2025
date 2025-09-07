using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    [SerializeField] private string _targetLevelName;

    public void Load()
    {
        StartCoroutine(LoadRoutine());
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator LoadRoutine()
    {
        AsyncOperation sceneLoadOp = SceneManager.LoadSceneAsync(_targetLevelName, LoadSceneMode.Single);

        while (!sceneLoadOp.isDone)
        {
            print(sceneLoadOp.progress);
            yield return null;
        }
    }
}
