using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonaDataList_SO", menuName = "Data/PersonaDataList")]
public class PersonaDataList_SO : ScriptableObject
{
    public List<PersonaDetails> PersonaDetailsList;
}
