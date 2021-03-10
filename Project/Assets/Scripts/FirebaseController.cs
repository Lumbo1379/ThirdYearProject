using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseController : MonoBehaviour
{
    [SerializeField] private SignInController _signInController;

    public delegate void PutUserCallback();

    private void PutUserTest(PutUserCallback callback)
    {
        var score1 = new DoubleValue
        {
            doubleValue = 1.5
        };

        var score2 = new DoubleValue
        {
            doubleValue = 5.5
        };

        var score3 = new DoubleValue
        {
            doubleValue = 2.5
        };

        var score4 = new DoubleValue
        {
            doubleValue = 8.5
        };

        var score5 = new DoubleValue
        {
            doubleValue = 7.456
        };

        DoubleValue[] arr = { score1, score2, score3, score4, score5 };

        var values = new Values
        {
            values = arr
        };

        var arrayValue = new ArrayValue
        {
            arrayValue = values
        };

        var uid = new Uid
        {
            stringValue = _signInController.Email
        };

        var fields = new Fields
        {
            scores = arrayValue,
            uid = uid
        };

        var document = new Document
        {
            fields = fields
        };

        var requestHelper = new RequestHelper
        {
            Uri = $"{RESTConstants.BASE_URL}users?documentId={fields.uid.stringValue}",
            Method = "POST",
            Timeout = 10,
            Headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {_signInController.LoginDetails.idToken}" }
            },
            Body = document
        };

        RestClient.Post<Document>(requestHelper).Then(response => { callback(); }).Catch(Debug.LogError);
    }

    private void PutUser(PutUserCallback callback, Score[] scoresArr)
    {
        var uid = new Uid
        {
            stringValue = _signInController.Email
        };

        var score1 = new DoubleValue
        {
            doubleValue = scoresArr[0].doubleValue
        };

        var score2 = new DoubleValue
        {
            doubleValue = scoresArr[1].doubleValue
        };

        var score3 = new DoubleValue
        {
            doubleValue = scoresArr[2].doubleValue
        };

        var score4 = new DoubleValue
        {
            doubleValue = scoresArr[3].doubleValue
        };

        var score5 = new DoubleValue
        {
            doubleValue = scoresArr[4].doubleValue
        };

        DoubleValue[] arr = { score1, score2, score3, score4, score5 };

        var values = new Values
        {
            values = arr
        };

        var arrayValue = new ArrayValue
        {
            arrayValue = values
        };

        var fields = new Fields
        {
            scores = arrayValue,
            uid = uid
        };

        var document = new Document
        {
            fields = fields
        };

        var requestHelper = new RequestHelper
        {
            Uri = $"{RESTConstants.BASE_URL}users?documentId={fields.uid.stringValue}",
            Method = "POST",
            Timeout = 10,
            Headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {_signInController.LoginDetails.idToken}" }
            },
            Body = document
        };

        RestClient.Post<Document>(requestHelper).Then(response => { callback(); }).Catch(Debug.LogError);
    }

    public void OnClickAddTestUser()
    {
        PutUserTest(() =>
        {
            Debug.Log("Test user scores added successfully!");
        });
    }

    public void AddUser(Score[] scores)
    {
        PutUser(() =>
        {
            Debug.Log("User scores added successfully!");
        }, scores);
    }
}
