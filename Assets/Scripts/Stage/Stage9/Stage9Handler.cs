using System;
using System.Collections;
using UnityEngine;

namespace Stage
{
    public class Stage9Handler : MonoBehaviour
    {
        [SerializeField] GameObject _playerObject;
        [SerializeField] float intervalTime;
        [SerializeField] GameObject beamPrefab;
        [SerializeField] Transform beamParent;
        [SerializeField] Vector2 beamStartPosition;
        void Start()
        {
            StartCoroutine(GenerateBeam());
        }

        void Update()
        {
            _playerObject.SetActive(WordController.Instance.inactiveWord != "石");
        }

        IEnumerator GenerateBeam()
        {
            while (true)
            {
                Instantiate(beamPrefab, beamStartPosition, Quaternion.identity, beamParent);
                yield return new WaitForSeconds(intervalTime);   
            }
        }
    }
}