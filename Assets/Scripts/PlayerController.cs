using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static Action<Transform> onAlarmActivated;

    [SerializeField] private float speed=10f;

    private TMP_Text alarmText;
    private CharacterController characterController;
    private float alarmLevel;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        alarmText = GameObject.FindGameObjectWithTag("AlarmLevel").GetComponent<TMP_Text>();
        Debug.Log(characterController);
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        characterController.Move(moveVector*Time.fixedDeltaTime*speed);
        alarmText.text = alarmLevel.ToString();
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            alarmLevel += 0.1f;
        }
        else if (alarmLevel > 0f)
        {
            alarmLevel -= 0.1f;
        }
        if (alarmLevel > 10)
        {
            onAlarmActivated?.Invoke(gameObject.transform);
        }
    }
}
