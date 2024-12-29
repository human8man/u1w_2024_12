using System;
using UnityEngine;

namespace StageSelect
{
    public class StageSelectInitialSoundPlayer : MonoBehaviour
    {
        void Start()
        {
            SoundManager.Instance.PlaySound("BGM_Title");
        }
    }
}