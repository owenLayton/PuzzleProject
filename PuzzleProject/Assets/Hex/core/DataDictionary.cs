using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class DataDictionary : MonoBehaviour
{
    [System.Serializable]
    class KeyValuePairs
    {
        public string m_key;
        [Serialize]public object m_value;

        public KeyValuePairs(string key, object value)
        {
            m_key = key;
            m_value = value;
        }
    }
    
    [SerializeField] List<KeyValuePairs> m_dataDictionary = new();
    [SerializeField] Dictionary<string, List<IDataDictionaryListener>> m_listeners = new();

    public void Awake()
    {
        List<IDataDictionaryListener> allListeners = FindObjectsOfType<IDataDictionaryListener>().Where(a => a.m_dd == this).ToList();
        foreach (IGrouping<string, IDataDictionaryListener> keyPairing in allListeners.GroupBy(a => a.m_key))
        {
            m_listeners.Add(keyPairing.Key, keyPairing.ToList());
        }
    }

    public void Set(string key, object value)
    {
        KeyValuePairs data = m_dataDictionary.Find(a => a.m_key == key);
        if (data == null)
            m_dataDictionary.Add(new KeyValuePairs(key, value));
        else
            data.m_value = value;

        foreach (IDataDictionaryListener listener in m_listeners.GetValueOrDefault(key, new List<IDataDictionaryListener>()))
            listener.OnVariableUpdated(value);
    }
}
