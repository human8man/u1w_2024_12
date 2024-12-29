using System;
using UI;
using UnityEngine;

namespace Title
{
    public class TitleManager : MonoBehaviour
    {
        float _nowTime = 0;
        float _transitionTime = 0.7f;
        bool _loaded = false;
        void Update()
        {
            if (WordController.Instance.inactiveWord == "タイトル")
            {
                _nowTime += Time.deltaTime;
                if (_loaded) return;
                if (_nowTime > _transitionTime)
                {
                    FadeSystem.Instance.LoadScene("Game");
                    _loaded = true;
                }
            }
        }
    }
}