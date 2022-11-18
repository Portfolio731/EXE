using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>対戦中の管理 </summary>
public enum BattleState
{
    Start,
    BattleZone, //キャラクター操作時
    ChipSelect  //チップ選択画面
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject chipScreen;
    [SerializeField] ChipSelectScreen chipSelectScreen;
    [SerializeField] Opening opening;
    BattleState state = BattleState.Start;
    void Start()
    {
        //ゲーム開始時最初のチップ選択処理
        chipSelectScreen.chipDecision += ChipDecision;
        playerController.chipSelectDisplay += chipSelectDisplay;
    }

    void Update()
    {
        if (state == BattleState.BattleZone)
        {
            playerController.HandleUpdate();
        }
        else if (state == BattleState.ChipSelect)
        {
            //チップ選択画面
            chipSelectScreen.SetChipImage();
            chipSelectScreen.HandleUpdate();

        }
        else if (state == BattleState.Start)
        {

            //スタートアニメーション
            StartCoroutine(opening.DeleteTile());

            if (opening.openingEnd)
            {
                state = BattleState.ChipSelect;
                chipSelectDisplay();
                SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Battle);
            }

        }

    }

    public void ChipDecision(List<int> chipNum)
    {
        state = BattleState.BattleZone;
        if (chipNum.Count != 0)
        {
            for (int i = 0; i < chipNum.Count; i++)
            {
                playerController.chipNum.Add(chipNum[i]);
            }
        }
        chipScreen.SetActive(false);
        //当たり判定つける
        playerObj.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void chipSelectDisplay()
    {
        //state変更　チップ選択画面表示　その他諸々　当たり判定消す
        state = BattleState.ChipSelect;
        chipScreen.SetActive(true);
        playerObj.GetComponent<BoxCollider2D>().enabled = false;
    }
}
//創作堂さくら紅葉 彼方サーバー: destruct