using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class FadeSystem : SingletonMonoBehaviour<FadeSystem>
    {
        [SerializeField] CinemaScope _cs;
        public void LoadScene(string sceneName)
        {
            _cs.OnFadeOutEnd = () =>
            {
                string previousSceneName = SceneManager.GetSceneAt(0).name;
                if (previousSceneName == "FadeScene")
                {
                    previousSceneName = SceneManager.GetSceneAt(1).name;
                }

                Debug.Log(previousSceneName);
                SceneManager.LoadScene(sceneName,LoadSceneMode.Additive);
                SceneManager.UnloadScene(previousSceneName);
            };
            _cs.FadeInAndOut();
        }
    }
}