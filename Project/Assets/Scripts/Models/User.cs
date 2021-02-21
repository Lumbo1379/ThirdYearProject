using System;
using System.Collections.Generic;


[Serializable]
public class Score
{
    public double doubleValue;
}

[Serializable]
public class Uid
{
    public string stringValue;
}

[Serializable]
public class Fields
{
    public Score score;
    public Uid uid;
}

[Serializable]
public class Document
{
    public Fields fields;
}

