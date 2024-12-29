using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectManager : MonoBehaviour
{
	private SelectButton[] Buttons;

	private bool IsActiveFlg;
	public TMP_Text StageText;

	// Start is called before the first frame update
	void Start()
	{
		//すべてのステージボタンを取得.
		Buttons = FindObjectsOfType<SelectButton>();

		//ボタンの状態を初期化.
		foreach(var Button in Buttons)
		{
			if(Button.StageIndex == 1)
			{
				Button.IsCleared = true;
			}
			else
			{
				Button.IsCleared = false;
			}
			Button.UpdateInteractable();
		}
	}

	public void OnStageSelected(int StageIndex)
	{
		StageText.text = $"ステージ{StageIndex}以外ないゲーム";
	}

	public void OnStageCleared(int StageIndex)
	{
		Debug.Log($"ステージ{StageIndex}クリア!");

		//次のステージを解放.
		foreach (var Button in Buttons)
		{
			if (Button.StageIndex == StageIndex + 1)
			{
				Button.IsCleared = true;
			}
		}
	}

	public void DeselectAll()
	{
		Debug.Log("すべてのボタンを非選択状態にします");
		foreach (var Button in Buttons)
		{
			Button.Deselect();
		}
	}

	public void HideOtherButton(int StageIndex)
	{
		foreach(var Button in Buttons)
		{
			if(Button.StageIndex != StageIndex)
			{
				Button.gameObject.SetActive(false);
			}
		}
	}

	public bool IsActive
	{
		get { return IsActiveFlg; }
		set { IsActiveFlg = value; }
	}
}
