using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
	public int StageIndex;              //ステージ番号.
	public Button ButtonComponent;
	public string StageName;            //このステージの名前.
	private bool IsSelected = false;    //選択状態.
	[SerializeField]
	private bool IsClearedFlg = false;

	public void Update()
	{
		UpdateInteractable();
	}

	public void OnButtonClick()
	{
		SelectManager Manager = FindObjectOfType<SelectManager>();
		if (Manager == null)
		{
			Debug.LogError("StageSelectionManagerが見つかりません");
			return;
		}

		if (!IsSelected)
		{
			//ほかのボタンを非選択状態にする.
			Manager.DeselectAll();

			//このボタンを選択状態にする.
			IsSelected = true;
			Debug.Log($"ステージ{StageName}が選択されました");
			Manager.OnStageSelected(StageIndex);
			GetComponent<Image>().color = Color.gray;
		}
		else
		{
			//選択状態で再クリックされたらステージ遷移.
			Debug.Log($"ステージ{StageName}に移動します");
			Manager.HideOtherButton(StageIndex);
			SceneManager.LoadScene(StageName);
			GetComponent<Image>().color = Color.white;
		}
	}

	public void UpdateInteractable()
	{
		//ステージクリア状態に基づいてボタンを有効化または無効化.
		if(StageIndex == 1 || IsClearedFlg)
		{
			ButtonComponent.interactable = true;
		}
		else
		{
			ButtonComponent.interactable = false;
		}
	}

	public void Deselect()
	{
		IsSelected = false;
		GetComponent<Image>().color = Color.white;
	}

	public bool IsCleared
	{
		get { return IsClearedFlg; }
		set
		{
			IsClearedFlg = value;
			UpdateInteractable();   //クリア状態が変更されたらボタンの状態を更新.
		}
	}
}
