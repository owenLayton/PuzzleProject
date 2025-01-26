using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueParams : SingletonMonoBehaviour<ClueParams>
{
    [System.Serializable]
    public class ClueData
    {
        public string m_id;
        public string m_name;
        public string m_imageId;
        public bool m_hasPopup;
        public string m_popupText;
    }

    public Dictionary<string, ClueData> m_clueData;
    [SerializeField] List<ClueData> m_clues;

    new void Awake()
    {
        base.Awake();
        m_clueData = new Dictionary<string, ClueData>();
        foreach(ClueData clue in m_clues)
            m_clueData.Add(clue.m_id, clue);
    }
}
