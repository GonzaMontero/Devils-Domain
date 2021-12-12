using System.Collections.Generic;
using UnityEngine;

public static class CharacterSOSorter
{
    public static int Compare(BattleCharacterSO x, BattleCharacterSO y)
    {
        return x.index.CompareTo(y.index);
    }
}
public class CharacterDataManager : MonoBehaviourSingleton<CharacterDataManager>
{
    public List<BattleCharacterSO> characters { get; private set; }

    //Unity Events 
    public override void Awake()
    {
        base.Awake();
        characters = new List<BattleCharacterSO>();
        LoadCharacters();
    }
    private void OnDestroy()
    {
        UnloadCharacters();
    }

    //Methods 
    void LoadCharacters()
    {
        characters.AddRange(Resources.LoadAll<BattleCharacterSO>("Scriptable Objects/Characters/Allies"));
        characters.Sort(CharacterSOSorter.Compare);
    }
    void UnloadCharacters()
    {
        foreach (var so in characters)
        {
            Resources.UnloadAsset(so);
        }
        characters.Clear();
    }
}