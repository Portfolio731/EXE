using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject cursorWindow;
    [SerializeField] Image practiceCursor;
    [SerializeField] Image onlineCursor;
    [SerializeField] GameObject titleText;
    [SerializeField] GameObject fadeScreen;
    public int cursor = 0;
    bool cursorFlg;
    bool titleFlg;
    void Update()
    {
        TitleStart();
        CursorUpdate(cursorFlg);
    }
    void TitleStart()
    {
        if (Input.GetKeyDown(KeyCode.Q) && titleFlg == false)
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.TitleWindowOpen);
            titleText.SetActive(false);
            cursorWindow.SetActive(true);
            titleFlg = true;
            cursorFlg = true;
        }
    }

    void CursorUpdate(bool cursorFlg)
    {
        if (cursorFlg)
        {
            if (Input.GetKeyDown(KeyCode.S) && cursor == 0)
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.TitleMoveCuror);
                cursor += 1;
            }
            if (Input.GetKeyDown(KeyCode.W) && cursor == 1)
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.TitleMoveCuror);
                cursor -= 1;
            }
            if (cursor == 0)
            {
                practiceCursor.color = new Color(255 / 255, 0, 0);
                onlineCursor.color = new Color(255 / 255, 255 / 255, 255 / 255);
            }
            else if (cursor == 1)
            {
                onlineCursor.color = new Color(255 / 255, 0, 0);
                practiceCursor.color = new Color(255 / 255, 255 / 255, 255 / 255);
            }
            StartCoroutine(CursorDecision());
        }
    }

    IEnumerator CursorDecision()
    {
        if (cursorFlg)
        {
            if (cursor == 0 && Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.TitleDecision);
                fadeScreen.SetActive(true);
                cursorFlg = false;
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(1);
            }
            else if (cursor == 1 && Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.Instance.PlaySE(SESoundData.SE.TitleError);
                Debug.Log("ƒIƒ“ƒ‰ƒCƒ“");
            }
        }

    }
}
