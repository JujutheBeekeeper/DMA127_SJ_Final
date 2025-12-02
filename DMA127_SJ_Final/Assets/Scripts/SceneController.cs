using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [SerializeField] Animator transitionAnim;

    public int nextScene;
    public int currentScene;
    public int Menu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // works because SceneController is a root object
        }
        else
        {
            Destroy(gameObject); // prevent duplicates
        }
    }

    public void NextScene()
    {
        transitionAnim.SetTrigger("End");
        StartCoroutine(LoadSceneAfterDelay(nextScene, 2f));
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenuAnim()
    {

        transitionAnim.SetTrigger("End");
        StartCoroutine(LoadSceneAfterDelay(Menu, 2f));
    }

    private IEnumerator LoadSceneAfterDelay(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}