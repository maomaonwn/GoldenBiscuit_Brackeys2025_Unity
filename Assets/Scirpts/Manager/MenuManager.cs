using Scirpts.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scirpts.Manager
{
    public class MenuManager : SingletonBase<MenuManager>
    {

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
