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
    public ArrayValue scores;
    public Uid uid;
}

[Serializable]
public class Document
{
    public Fields fields;
}

[Serializable]
public class ArrayValue
{
    public Values arrayValue;
}

[Serializable]
public class Values
{
    public DoubleValue[] values;
}

[Serializable]
public class DoubleValue
{
    public double doubleValue;
}

