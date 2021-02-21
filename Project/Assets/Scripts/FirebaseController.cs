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
        var score = new Score
        {
            doubleValue = 5.5
        };

        var uid = new Uid
        {
            stringValue = "unity-test-user"
        };

        var fields = new Fields
        {
            score = score,
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

        //RestClient.Post<Document>($"{RESTConstants.BASE_URL}users?documentId={fields.uid.stringValue}", document).Then(response => { callback(); }).Catch(Debug.LogError);
        RestClient.Post<Document>(requestHelper).Then(response => { callback(); }).Catch(Debug.LogError);
    }

    public void OnClickAddTestUser()
    {
        PutUserTest(() =>
        {
            Debug.Log("User added successfully!");
        });
    }
}
