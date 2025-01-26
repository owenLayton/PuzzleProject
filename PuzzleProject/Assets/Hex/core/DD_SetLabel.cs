using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DD_SetLabel : IDataDictionaryListener
{
    Text m_text;
    TextMeshProUGUI m_textMeshPro;

    public void Awake()
    {
        TryGetComponent(out m_text);
        TryGetComponent(out m_textMeshPro);
    }
    public override void OnVariableUpdated(object value)
    {
        if (m_text != null)
        {
            m_text.text = value.ToString();
        }
        else if (m_textMeshPro != null)
        {
            m_textMeshPro.text = value.ToString();
        }
        else
        {
            Debug.LogError("No Valid Text Format");
        }
    }
}
