﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Attributes

    public GameObject camera2D;
    public GameObject cameraTransition;
    public GameObject camera3D;
    public Transform lastCheckPoint;

    public static LevelManager Instance;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        StartCoroutine(waitForReferences());
    }

    private IEnumerator waitForReferences()
    {
        while(camera2D == null && cameraTransition == null && camera3D == null)
        {
            yield return null;
        }
        CameraManager.Instance.UpdateCameraReferences(camera2D, cameraTransition, camera3D, GameObject.FindObjectOfType<Cinemachine.CinemachineBrain>().gameObject);
        if (lastCheckPoint == null)
        {
            lastCheckPoint = GameObject.FindObjectOfType<CheckPoint>().transform;
        }
    }

    #endregion
}
