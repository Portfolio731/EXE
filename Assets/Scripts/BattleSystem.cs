using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�ΐ풆�̊Ǘ� </summary>
public enum BattleState
{
    Start,
    BattleZone, //�L�����N�^�[���쎞
    ChipSelect  //�`�b�v�I�����
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
        //�Q�[���J�n���ŏ��̃`�b�v�I������
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
            //�`�b�v�I�����
            chipSelectScreen.SetChipImage();
            chipSelectScreen.HandleUpdate();

        }
        else if (state == BattleState.Start)
        {

            //�X�^�[�g�A�j���[�V����
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
        //�����蔻�����
        playerObj.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void chipSelectDisplay()
    {
        //state�ύX�@�`�b�v�I����ʕ\���@���̑����X�@�����蔻�����
        state = BattleState.ChipSelect;
        chipScreen.SetActive(true);
        playerObj.GetComponent<BoxCollider2D>().enabled = false;
    }
}
//�n�쓰������g�t �ޕ��T�[�o�[: destruct