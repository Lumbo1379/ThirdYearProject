using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController : MonoBehaviour
{
    [Header("Menus", order = 0)]
    [SerializeField] private Transform _signInMenu;

    [Header("Positioning", order = 1)]
    [SerializeField] private Transform _playerController;
    [SerializeField] private Vector3 _offset;

    [Header("Controllers", order = 2)]
    [SerializeField] private SignInController _signInController;
    [SerializeField] private Throw _throw;

    [Header("Debug", order = 3)]
    [SerializeField] private bool _testReposition;

    private void Awake()
    {
        _testReposition = false;
    }

    private void Start()
    {
        HandleOnClickButtonStart();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start) || _testReposition)
        {
            HandleOnClickButtonStart();

            _testReposition = false;
        }

        if (_signInController.SignedIn && !_throw.HasStartedThrowing)
            CheckFacingCorrectDirection();
    }

    private void HandleOnClickButtonStart()
    {
        if (_signInMenu.gameObject.activeSelf)
        {
            _signInMenu.gameObject.SetActive(false);
            Debug.Log("Menu hidden");
        }
        else if (!_signInController.SignedIn)
        {
            _signInMenu.position = _playerController.transform.forward * _offset.z + new Vector3(0, _offset.y, 0);
            _signInMenu.rotation = _playerController.transform.rotation;
            _signInMenu.gameObject.SetActive(true);
            Debug.Log($"Menu shown at -> Position: {_playerController.transform.position.x}, {_playerController.transform.position.y}, {_playerController.transform.position.z} | Rotation:  {_playerController.transform.rotation.eulerAngles.x}, {_playerController.transform.rotation.eulerAngles.y}, {_playerController.transform.rotation.eulerAngles.z}");
        }
    }

    private void CheckFacingCorrectDirection()
    {
        RaycastHit hit;

        if (Physics.Raycast(_playerController.transform.position, _playerController.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Throw Area")
                _throw.StartThrowing();
        }
    }
}
