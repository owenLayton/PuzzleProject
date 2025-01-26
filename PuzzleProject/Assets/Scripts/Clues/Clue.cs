using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    //Store information about a clue
    public string m_clueId;
    public ClueParams.ClueData m_clueData;
    public bool m_collected; // player has seen the clue

    [SerializeField] string m_imageLocation = "Resources/Images/Clues";
    DataDictionary m_dd;

    public void Awake()
    {
        TryGetComponent(out m_dd);
    }

    void Start()
    {
        if (!ClueParams.Instance().m_clueData.TryGetValue(m_clueId, out m_clueData))
        {
            Debug.Log($"Clue with ID {m_clueId} does not exist");
        }

        ClueManager.Instance().AddClue(this);

        //send down parts of ClueData that we need
        if (!Resources.Load<Texture>($"{m_imageLocation}/{m_clueId}"))
            Debug.LogWarning($"Image for {m_clueId} could not be found");

        m_dd.Set("image", $"{m_imageLocation}/{m_clueId}");
        m_dd.Set("name", m_clueData.m_name);
        m_dd.Set("is_collected", false);
    }

    public void Collect()
    {
        if (m_collected)
            return;

        m_collected = true;
        Debug.Log($"{m_clueId} collected");
        m_dd.Set("is_collected", true);
    }

    public void OpenPopup()
    {
        if (!m_clueData.m_hasPopup)
            return;

        Debug.Log($"Popup opened for {m_clueId}");
    }
}
