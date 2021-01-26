using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FirestoreData]
public class User
{
    [FirestoreProperty]
    public string Uid { get; set; }
    public float Score { get; set; }
}
