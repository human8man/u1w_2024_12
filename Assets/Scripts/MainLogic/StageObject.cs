using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageObject : MonoBehaviour
{
   [System.Serializable]
    private struct WordList
    {
        public string mainWord;// �I�u���W�F�N�g�̃��C���ƂȂ�P��.
        public List<string> containedWords;// �I�u�W�F�N�g���Ɋ܂܂��P��.
    }

    [SerializeField, Tooltip("�P�ꃊ�X�g")]
    private WordList wordList;

    [SerializeField,Tooltip("�G�ꂽ�玀�ʂ��̐ݒ�")]
    private bool isDanger;

    private bool isActive = true; // �I�u�W�F�N�g���A�N�e�B�u���ǂ���

    [SerializeField,Tooltip("�e�L�X�g�̃v���n�u")]
    private Text textPrefab;                // �e�L�X�g�I�u�W�F�N�g�𐶐����邽�߂�Prefab.
    private Text mainWordTextInstance;      // ���C���P��̃e�L�X�g�C���X�^���X.
    private Text wordsCountTextInstance;    // �܂܂�Ă���P��̐���\������e�L�X�g�C���X�^���X.


    void Start()
    {
        // �V�[������Canvas���擾.
        var _canvas = FindObjectOfType<Canvas>().transform.Find("ObjectWord");

        // ���[���h���W���X�N���[�����W�ɕϊ�.
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);


        // ���C���P��e�L�X�g�ƒP�ꐔ�e�L�X�g�̃C���X�^���X����.
        mainWordTextInstance = Instantiate(textPrefab, screenPosition, Quaternion.identity);
        wordsCountTextInstance = Instantiate(textPrefab, screenPosition, Quaternion.identity);


        // �e�L�X�g�̐e��Canvas�ɐݒ�.
        mainWordTextInstance.transform.SetParent(_canvas.transform);
        wordsCountTextInstance.transform.SetParent(_canvas.transform);


        // �e�L�X�g���e��ݒ�.
        mainWordTextInstance.text = wordList.mainWord;
        wordsCountTextInstance.text = wordList.containedWords.Count.ToString();


        // �I�u�W�F�N�g�Ɋ܂܂��P�ꐔ�e�L�X�g�̈ʒu��ݒ�.
        SetWordCountTextPosition();
    }


    // �I�u�W�F�N�g�Ɋ܂܂��P�ꐔ�e�L�X�g�̈ʒu��ݒ�.
    private void SetWordCountTextPosition()
    {
        var sRenderer = GetComponent<SpriteRenderer>();
        Vector3 size = sRenderer.bounds.size;


        // �I�u�W�F�N�g�̍���̈ʒu���v�Z.
        Vector3 topLeftWorldPosition    = transform.position + new Vector3(-size.x * 0.5f, size.y * 0.5f, 0.0f);
        Vector3 topLeftScreenPosition   = Camera.main.WorldToScreenPoint(topLeftWorldPosition);

        // �P�ꐔ�e�L�X�g�̈ʒu���X�V.
        wordsCountTextInstance.transform.position = topLeftScreenPosition;
    }


    // �w�肵���P�ꂪ�܂܂�Ă��邩����.
    public bool IsWordContained(string targetWord)
    {
        return wordList.containedWords.Contains(targetWord);
    }


    // �I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�ݒ�E�擾����v���p�e�B.
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }
}
