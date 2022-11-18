using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>�`�b�v�I�𒆂̏��� </summary>
public class ChipSelectScreen : MonoBehaviour
{
    //�`�b�v�f�b�L����Q��
    [SerializeField] public ChipDeck chipDeck;
    [SerializeField] List<GameObject> selectedChip;
    [SerializeField] List<GameObject> selectChip;
    [SerializeField] Text chipName;
    [SerializeField] Text damage;
    [SerializeField] GameObject chipImage;
    [SerializeField] Text description;
    [SerializeField] List<GameObject> selectCursor;
    [SerializeField] GameObject okCursor;
    [SerializeField] CustomGauge customGauge;
    public UnityAction<List<int>> chipDecision;
    public List<int> selectList = new List<int>();
    int selection = 0;
    public int selectCount = 0;
    public void SetChipImage()
    {
        for (int i = 0; i < selectChip.Count; i++)
        {
            selectChip[i].GetComponent<Image>().sprite = chipDeck.AttackChips[i].Image;
        }
    }
    /// <summary>�J�[�\�������킹���`�b�v�̃f�[�^��\������ </summary>
    /// <param name="selection"></param>
    public void ChipDisplay(int selection)
    {
        if (selection <= 8)
        {
            chipName.text = chipDeck.AttackChips[selection].Name;
            damage.text = "" + chipDeck.AttackChips[selection].Damage;
            chipImage.GetComponent<Image>().sprite = chipDeck.AttackChips[selection].Image;
            description.text = chipDeck.AttackChips[selection].Description;
        }

    }

    /// <summary>�I����ʃJ�[�\���ړ� </summary>
    public void HandleUpdate()
    {
        var prevSelection = selection;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (selection == 0 || selection == 4)
                return;
            if (selection == 9)
            {
                selection = 3;
                SoundManager.Instance.PlaySE(SESoundData.SE.ChipCursorMove);
                return;
            }
            selection--;
            SoundManager.Instance.PlaySE(SESoundData.SE.ChipCursorMove);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (selection == 9)
                return;
            if (selection == 3 || selection == 7)
            {
                selection = 9;
                SoundManager.Instance.PlaySE(SESoundData.SE.ChipCursorMove);
                return;
            }
            selection++;
            SoundManager.Instance.PlaySE(SESoundData.SE.ChipCursorMove);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (selection <= 3 || selection == 9)
                return;
            selection -= 4;
            SoundManager.Instance.PlaySE(SESoundData.SE.ChipCursorMove);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selection >= 4)
                return;
            selection += 4;
            SoundManager.Instance.PlaySE(SESoundData.SE.ChipCursorMove);
        }
        UpdateChipSelection(selection);
        //����
        if (Input.GetKeyDown(KeyCode.E) && selection <= chipDeck.AttackChips.Count)
        {
            SelectChip(selection);
        }
        if (Input.GetKeyDown(KeyCode.E) && selection == 9)
        {//OK����
            //�I�������`�b�v��ێ����đΐ펞�Ɏg�p�ł���悤�ɂ���
            ChipDecision();
        }
        //�L�����Z��
        if (Input.GetKeyDown(KeyCode.R))
        {
            //�`�b�v��I�����Ă������񕪃L�����Z������
            CancelChipSelect();
        }
        ChipDisplay(selection);
    }
    /// <summary>�J�[�\���̈ړ� </summary>
    /// <param name="selectetdChip"></param>
    public void UpdateChipSelection(int selectetdChip)
    {
        for (int i = 0; i < 8; i++)
        {
            if (i == selectetdChip)
            {
                selectCursor[i].SetActive(true);
            }
            else
            {
                selectCursor[i].SetActive(false);
                okCursor.GetComponent<Image>().color = new Color(255, 0, 0);
            }
            if (selectetdChip == 9)
            {
                okCursor.GetComponent<Image>().color = new Color(255, 255, 0);
            }
        }
    }
    void SelectChip(int selection)
    {
        if (selection != 9 && selectCount < 5)
        {
            bool selectCheck = selectList.Contains(selection);
            if (!selectCheck)
            {
                selectedChip[selectCount].SetActive(true);
                selectedChip[selectCount].GetComponent<Image>().sprite = chipDeck.AttackChips[selection].Image;
                selectChip[selection].GetComponent<Image>().color = new Color(0, 0, 0);
                selectCount += 1;
                selectList.Add(selection);
                SoundManager.Instance.PlaySE(SESoundData.SE.CbipSelect);
            }

        }
    }
    /// <summary>�I�������`�b�v����񕪃L�����Z�� </summary>
    void CancelChipSelect()
    {
        if (selectCount < 0)
        {
            selectCount = 0;
        }
        if (selectList.Count != 0 && selectCount <= 5)
        {
            selectCount -= 1;
            selectedChip[selectCount].SetActive(false);
            selectChip[selectList[selectCount]].GetComponent<Image>().color = new Color(255, 255, 255);
            selectList.RemoveAt(selectList.Count - 1);
            SoundManager.Instance.PlaySE(SESoundData.SE.ChipCancel);
        }
    }
    ///<summary>ok�{�^�����������Ƃ��ɑI�������`�b�v��n�� </summary>
    void ChipDecision()
    {
        List<int> chipId = new List<int>();
        List<int> usedNum = new List<int>();
        if (selectList.Count != 0)
        {
            for (int i = 0; i < selectList.Count; i++)
            {
                chipId.Add(chipDeck.AttackChips[selectList[i]].Number);
                chipDeck.AttackChips.Add(chipDeck.AttackChips[selectList[i]]);
            }
            selectList.Sort();
            selectList.Reverse();
            for (int i = 0; i < selectList.Count; i++)
            {
                chipDeck.AttackChips.RemoveAt(selectList[i]);
            }
            selectList.Clear();
            for (int i = 0; i < selectedChip.Count; i++)
            {
                selectedChip[i].GetComponent<Image>().sprite = null;
                selectedChip[i].SetActive(false);
            }
            for (int i = 0; i < selectChip.Count; i++)
            {
                selectChip[i].GetComponent<Image>().color = new Color(255, 255, 255);
            }
        }
        selection = 0;
        customGauge.OkSelect();
        chipDecision(chipId);
        chipId.Clear();
        selectCount = 0;
        SoundManager.Instance.PlaySE(SESoundData.SE.OK);
    }
}
/*
    OK�{�^������������퓬��ʂ֑J��
    �I�������`�b�v�̔ԍ���BattleSystem����PlayerController�֓n��
    ���X�g�̕ύX

 */