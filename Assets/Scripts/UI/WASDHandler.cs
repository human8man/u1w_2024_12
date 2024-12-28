using System;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class WASDHandler : MonoBehaviour
    {
        [System.Serializable]
        class KeyNameAndObject
        {
            public string Name;
            public GameObject Obj;
        }

        [SerializeField] KeyNameAndObject[] _keyNameAndObjects;

        void Update()
        {
            foreach (var tuple in _keyNameAndObjects)
            {
                tuple.Obj.SetActive(tuple.Name != WordController.Instance.inactiveWord);
            }
        }
    }
}