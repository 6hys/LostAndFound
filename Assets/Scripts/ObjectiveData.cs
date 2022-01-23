using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class ObjectiveData
{
    [XmlElement("ID")]
    public int _ID;

    [XmlElement("Title")]
    public string _title;

    [XmlElement("Description")]
    public string _description;

    [XmlArray("Conditions")]
    [XmlArrayItem("Condition")]
    public string[] _conditions;

    [XmlElement("NextStep")]
    public int _nextStep;
}

[System.Serializable]
[XmlRoot("QuestContainer")]
public class Objectives
{
    [XmlArray("Quests")]
    [XmlArrayItem("Quest")]
    public ObjectiveData[] _objectives;
}