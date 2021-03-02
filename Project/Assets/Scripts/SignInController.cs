using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class SignInController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private ParticleSystem _successEffect;

    public SignResponse LoginDetails { get; set; }

    private void Awake()
    {
        LoginDetails = new SignResponse
        {
            localId = "",
            idToken = ""
        };
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
                _successEffect.Play();
                Debug.Log("Signed in successfully!");
            }).Catch(Debug.Log);
    }
}
