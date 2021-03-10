using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class SignInController : MonoBehaviour
{

    [Header("", order = 0)]
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _countdownCanvas;
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private ParticleSystem _successEffect;

    [Header("Debug", order = 1)]
    [SerializeField] private FirebaseController _firebaseController;
    [SerializeField] private bool _mockSignIn;
    [SerializeField] private bool _mockPut;

    public bool SignedIn { get; set; }

    public SignResponse LoginDetails { get; set; }
    public string Email { get; set; }

    private void Awake()
    {
        LoginDetails = new SignResponse
        {
            localId = "",
            idToken = ""
        };

        SignedIn = false;
        _mockSignIn = false;
        _mockPut = false;
    }

    private void Update()
    {
        if (_mockSignIn)
        {
            _mockSignIn = false;
            SignInUser("fake@fake.com", "fakefake");
        }

        if (_mockPut)
        {
            _mockPut = false;
            _firebaseController.OnClickAddTestUser();
        }
    }

    public void OnClickSignIn()
    {
        SignInUser(_emailInput.text, _passwordInput.text);
    }

    private void SignInUser(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + RESTConstants.API_KEY, userData).Then(
            response =>
            {
                LoginDetails = response;
                Email = email;
                HandleSuccessfulSignIn();
            }).Catch(Debug.Log);
    }

    private void HandleSuccessfulSignIn()
    {
        _successEffect.Play();
        _menuCanvas.SetActive(false);
        _countdownCanvas.SetActive(true);
        SignedIn = true;
        Debug.Log("Signed in successfully!");
    }
}
