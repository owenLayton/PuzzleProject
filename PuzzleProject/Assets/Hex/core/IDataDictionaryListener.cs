using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDataDictionaryListener : MonoBehaviour
{
    public DataDictionary m_dd;
    public string m_key;
    public abstract void OnVariableUpdated(object value);
}
