using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class AutoSceneLoader : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnGameStart()
        {
            int count = SceneManager.sceneCount;
            string[] sceneNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                sceneNames[i] = SceneManager.GetSceneAt(i).name;
            }
            if (sceneNames.All(s => s != "FadeScene"))
            {
                SceneManager.LoadScene("FadeScene", LoadSceneMode.Additive);
            }
           
        }
    }
}