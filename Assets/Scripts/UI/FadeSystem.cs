using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class FadeSystem : SingletonMonoBehaviour<FadeSystem>
    {
        string _fadeScene = "FadeScene";
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(_fadeScene, LoadSceneMode.Additive);
            CinemaScope cs = FindObjectOfType<CinemaScope>();
            cs.OnFadeOutEnd = () =>
            {
                SceneManager.LoadScene(sceneName);
            };
        }
    }
}