using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClueManager : SingletonMonoBehaviour<ClueManager>
{
    List<Clue> CollectedClues { get { return m_cluesInLevel.Where(a => a.m_collected).ToList(); } }
    List<Clue> m_cluesInLevel;

    public new void Awake()
    {
        base.Awake();
        m_cluesInLevel = new List<Clue>();
    }

    public void InitLevel()
    {
        m_cluesInLevel.Clear();
    }

    public void AddClue(Clue clue)
    {
        m_cluesInLevel.Add(clue);
    }
}
