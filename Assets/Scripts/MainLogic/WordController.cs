using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    const int BUTTON_MAX = 3;//�{�^���ő吔.

    [SerializeField] private List<string> words;// �{�^���ɕ\������P�ꃊ�X�g.
    [SerializeField] private Button[] buttons = new Button[BUTTON_MAX];// �{�^��.
    [SerializeField] private Text nonWordText;// �����������Q�[���ƕ\������TextUI.

    private string inactiveWord = "����"; // ��A�N�e�B�u��Ԃ̒P��.
    private string originalText = "�����������Q�[��";// �e�L�X�g�t�H�[�}�b�g.
    private int lastClickedButtonIndex;// �Ō�ɃN���b�N���ꂽ�{�^���ԍ�.

    private void Start()
    {
        // �{�^���Ƀ��X�i�[��ǉ�.
        for(int i = 0;i < BUTTON_MAX; i++ )
        {
            int index = i;
            buttons[index].onClick.AddListener(() => OnClickButton(index));
        }

    }

    private void Update()
    {
        // �{�^���̃e�L�X�g���X�V.
        for (int i = 0; i < BUTTON_MAX; i++)
        {
            UpdateButtonText(i, words[i]);
        }

        // ��A�N�e�B�u��Ԃ̒P���UI�ɔ��f.
        nonWordText.text = originalText.Replace("����", inactiveWord);
    }

   
    // �{�^���N���b�N���̏���.
    private void OnClickButton(int index)
    {
        Debug.Log("�����ꂽ�{�^��:" + index);

        Text currentText = GetButtonText(index);
        Text lastText = GetButtonText(lastClickedButtonIndex);

        StageObject[] stageObjects = FindObjectsOfType<StageObject>(true);

        // �O��ƍ���̃{�^�����قȂ�ꍇ.
        if (currentText.text != lastText.text)
        {
            ToggleObjects(stageObjects, lastText.text, true);       // �O��̒P����A�N�e�B�u��.
            ToggleObjects(stageObjects, currentText.text, false);   // ����̒P���؂�ւ�.
            inactiveWord = currentText.text;
        }
        else
        {
            // �����{�^���������ꂽ�ꍇ.
            ToggleObjects(stageObjects, currentText.text, false);
            inactiveWord = stageObjects[0].IsActive ? "����" : currentText.text;
        }

        // �Ō�ɃN���b�N���ꂽ�{�^���ԍ���ޔ�.
        lastClickedButtonIndex = index;
    }


    //  �I�u�W�F�N�g�̏�Ԑ؂�ւ�.
    private void ToggleObjects(StageObject[] objects, string word, bool activate)
    {
        foreach (var obj in objects)
        {
            if (obj.IsWordContained(word))
            {
                obj.IsActive = activate ? true : !obj.IsActive;
                obj.gameObject.SetActive(obj.IsActive);
            }
        }
    }


    // �{�^���̃e�L�X�g�X�V.
    private void UpdateButtonText(int index, string text)
    {
        Text buttonText = GetButtonText(index);
        buttonText.text = text;
    }


    // �{�^���̃e�L�X�g���擾.
    private Text GetButtonText(int index)
    {
        return buttons[index].transform.GetChild(0).GetComponent<Text>();
    }


    // �P�ꃊ�X�g�ɒǉ�.
    public void AddWord(string word)
    {
        if (words.Contains(word))
        {
            Debug.Log("�P��" + word + "�͊��Ƀ��X�g�ɑ��݂��Ă��邽�߁A�ǉ����܂���ł����B");
            return;
        }

        // �P��̒ǉ�.
        words.Add(word);
        Debug.Log("�P��" + word + "�����X�g�ɒǉ����܂����B");
    }
}
