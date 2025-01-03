/**
 * @file DeathCauseLog.cs.
 * @brief ボタン選択.
 * @author ふぅ.
 * @date 2024/12/29.
 */
using Stage;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
	public int StageIndex;                  //ステージ番号.
	public Button ButtonComponent;          //ボタンのコンポーネント.
	public string StageName;                //このステージの名前.
	private bool IsSelected = false;        //選択状態.
	[SerializeField]
	private bool IsClearedFlg = false;      //ステージがクリアされたかどうか.
	[SerializeField] StageInfo StageInfo;   //ステージ情報を保持するオブジェクト.
	[SerializeField] StageData stageData;   //ステージのデータを管理するオブジェクト.

	void Start()
	{
		if (stageData != null)
		{
			// StageDataを使って処理を行う
			Debug.Log($"現在のステージ: {stageData.GetCurrentStage()}");
		}
	}

	public void Update()
	{
        //ボタンのインタラクション状態を更新.
		UpdateInteractable();
	}

    public void OnButtonClick()
    {
        //SelectManagerを探す.
        SelectManager Manager = FindObjectOfType<SelectManager>();
        if (Manager == null)
        {
            Debug.LogError("StageSelectionManagerが見つかりません");
            return;
        }

        Manager.DeselectAll();
        //選択状態で再クリックされたらステージ遷移.
        Debug.Log($"ステージ{StageName}に移動します");
        //ほかのボタンの選択状態を解除.
        Manager.HideOtherButton(StageIndex);
        Manager.OnStageSelected(StageIndex);
        //ステージ情報のステージ番号を保存.
        StageInfo.LoadingStageNum = StageIndex;
        //シーンを読み込む.
        FadeSystem.Instance.LoadScene(StageName);
        //SceneManager.LoadScene(StageName);
        GetComponent<Image>().color = Color.gray;
        SoundManager.Instance.PlaySound("SwitchOn");
    }

	public void UpdateInteractable()
	{
		if (ButtonComponent != null)
		{
			//ステージクリア状態に基づいてボタンを有効化または無効化.
			if (StageIndex == 1 || IsCleared)
			{
				ButtonComponent.interactable = true;
			}
			else
			{
				ButtonComponent.interactable = false;
			}
		}
		else
		{
			Debug.LogWarning($"ButtonComponent is missing for Stage {StageIndex}.");
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
