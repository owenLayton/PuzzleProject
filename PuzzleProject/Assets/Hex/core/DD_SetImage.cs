using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DD_SetImage : IDataDictionaryListener
{
    Image m_image;
    public void Awake()
    {
        TryGetComponent(out m_image);
    }

    public override void OnVariableUpdated(object value)
    {
        Debug.Log($"Setting Image from location: {value}");
    }
}
