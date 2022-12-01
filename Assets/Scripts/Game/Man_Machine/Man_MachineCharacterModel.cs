using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Man_MachineCharacterModel_SO", menuName = "Model/Man_MachineCharacterModel_SO")]
public class Man_MachineCharacterModel : ScriptableObject
{
    public List<CharacterDetails> CharacterList = new List<CharacterDetails>();
    public CharacterDetails GetCharacterDetailsWithId(int id)
    {
        return CharacterList.Find(i => i.id == id);
    }
    public CharacterDetails GetCharacterDetailsWithName(CharacterName name)
    {
        return CharacterList.Find(i => i.characterName == name);
    }
    
}
[System.Serializable]
public class CharacterDetails
{
    public CharacterName characterName;
    public int id;
    public Sprite characterSprite;
    public int attackNum;
    public string description;//½ÇÉ«µÄÃèÊö
    public GameObject characterGo;
}
public enum CharacterName
{
    ZhiZhe,CiKe,HuoYanNvWu
}
