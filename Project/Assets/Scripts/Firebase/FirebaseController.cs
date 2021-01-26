
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseController : MonoBehaviour
{
    public Firebase.FirebaseApp App { get; set; }
    public FirebaseFirestore Database { get; set; }
    public Firebase.Auth.FirebaseAuth Auth { get; set; }
    public bool IsReady { get; set; }

    private const string USERS_COLLECTION = "users";

    private void Awake()
    {
        IsReady = false;
    }

    private void Start()
    {
        HandleGooglePlayServices();

        if (IsReady)
        {
            InitialiseDatabase();
            InitialiseAuthentication();
        }
    }

    private void HandleGooglePlayServices()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                App = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                IsReady = true;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void InitialiseDatabase()
    {
        Database = FirebaseFirestore.DefaultInstance;
    }

    private void InitialiseAuthentication()
    {
        Auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void AddUser(string uid, float score)
    {
        var documentReference = Database.Collection(USERS_COLLECTION).Document(uid);

        var user = new User
        {
            Uid = uid,
            Score = score
        };

        documentReference.SetAsync(user);
    }
}
