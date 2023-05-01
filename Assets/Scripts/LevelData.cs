using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataScriptableObject", order = 1)]
public class LevelData : ScriptableObject // klase, kurioje galime irasyti levelio kintamuosius
{
    public int[] wordLengths;
    public int[] wordCounts;
}