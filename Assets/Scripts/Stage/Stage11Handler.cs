using System;
using System.Collections;
using UI;
using Unity.Loading;
using UnityEngine;

namespace Stage
{
    public class Stage11Handler : MonoBehaviour
    {
        bool _loaded = false;

        void Update()
        {
            if (WordController.Instance.inactiveWord != "ENDING" && !_loaded)
            {
                _loaded = true;
                StartCoroutine(Delay());
            }
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(2);
            FadeSystem.Instance.LoadScene("EndCredits");
        }
    }
}