using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    [SerializeField] Text openingTextW;
    [SerializeField] Text openingTextB;
    [SerializeField] GameObject openingObj;
    [SerializeField] GameObject[] openingTileObj;
    public bool openingFlg = false;
    public bool openingEnd = false;
    void Start()
    {
        openingFlg = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator DeleteTile()
    {
        for (int i = 0; i < openingTileObj.Length; i++)
        {
            openingTileObj[i].SetActive(false);
            yield return new WaitForSeconds(0.05f);
        }
        if (!openingFlg)
        {
            openingFlg = true;
            openingObj.SetActive(true);
            yield return new WaitForSeconds(0.8f);
            openingTextW.text = "Ready";
            openingTextB.text = "Ready";
            yield return new WaitForSeconds(0.8f);
            openingTextW.text = "Fight!";
            openingTextB.text = "Fight!";
            yield return new WaitForSeconds(0.5f);
            openingTextW.text = "";
            openingTextB.text = "";
            openingObj.SetActive(false);
            openingEnd = true;
        }

    }
}