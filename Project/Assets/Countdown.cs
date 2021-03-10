using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private TMP_Text _text;
    private float _length;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void StartCountdown(float length)
    {
        _length = length - 0.5f;
        _text.text = ((int)length).ToString();

        CancelInvoke("Count");
        InvokeRepeating("Count", 1, 1);
    }

    private void Count()
    {
        if (_length <= -1)
        {
            CancelInvoke("Count");
            return;
        }

        _text.text = ((int)_length).ToString();

        _length--;
    }
}
