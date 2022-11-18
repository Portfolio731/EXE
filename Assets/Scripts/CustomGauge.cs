using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGauge : MonoBehaviour
{
    [SerializeField] public GameObject customGauge;
    [SerializeField] public GameObject gaugeMaxText;
    public bool chipSelect = false;
    public bool chargeGauge = true;
    public float seconds;
    public float gauge;

    void Start()
    {

    }

    void Update()
    {
        AddGauge(chargeGauge);
    }

    void AddGauge(bool chargeGauge)
    {
        if (chargeGauge)
        {
            if (chipSelect)
                seconds += Time.deltaTime;
            if (customGauge.transform.localScale.x < 1)
                customGauge.transform.localScale = new Vector3(seconds / 12, 1, 1);

            if (seconds >= 12)
                gaugeMaxText.SetActive(true);
        }
    }
    public void OkSelect()
    {
        chargeGauge = true;
        chipSelect = true;
        gaugeMaxText.SetActive(false);
        customGauge.transform.localScale = new Vector3(0, 1, 1);
    }
}
