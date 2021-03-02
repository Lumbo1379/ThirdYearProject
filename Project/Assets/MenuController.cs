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

    [Header("Debug", order = 2)]
    [SerializeField] private bool _testReposition;

    private void Awake()
    {
        _testReposition = false;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start) || _testReposition)
        {
            HandleOnClickButtonStart();

            _testReposition = false;
        }
    }

    private void HandleOnClickButtonStart()
    {
        if (_signInMenu.gameObject.activeSelf)
        {
            _signInMenu.gameObject.SetActive(false);
            Debug.Log("Menu hidden");
        }
        else
        {
            _signInMenu.position = _playerController.transform.forward * _offset.z + new Vector3(0, _offset.y, 0);
            _signInMenu.rotation = _playerController.transform.rotation;
            _signInMenu.gameObject.SetActive(true);
            Debug.Log($"Menu shown at -> Position: {_playerController.transform.position.x}, {_playerController.transform.position.y}, {_playerController.transform.position.z} | Rotation:  {_playerController.transform.rotation.eulerAngles.x}, {_playerController.transform.rotation.eulerAngles.y}, {_playerController.transform.rotation.eulerAngles.z}");
        }
    }
}
