using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cameraController;
    [SerializeField] private CinemachineVirtualCameraBase[] allCameras;
    [SerializeField] private GameObject SceneAnimation;
    private CinemachineVirtualCameraBase targetCamera;
    private const string SCENE_ANIMATION = "SceneAnimationCamera";
    private const string PLAYER_CAMERA = "PlayerCamera";
    public static CameraControl Ins;

    private void Awake()
    {
        Ins = this;
        foreach (var virtualCamera in allCameras)
        {
            if (virtualCamera.Name == PLAYER_CAMERA)
            {
                virtualCamera.VirtualCameraGameObject.SetActive(true);
            }
            else
            {
                virtualCamera.VirtualCameraGameObject.SetActive(false);
            }
        }
    }

    public void PlayTurning()
    {
        Instantiate(SceneAnimation);
        foreach (var virtualCamera in allCameras)
        {
            if (virtualCamera.Name == SCENE_ANIMATION)
            {
                targetCamera = virtualCamera;
                break;
            }
        }
        if (targetCamera != null)
        {
            cameraController.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            targetCamera.VirtualCameraGameObject.SetActive(true);
        }
    }

    public void ReturnTheView()
    {
        foreach (var virtualCamera in allCameras)
        {
            if (virtualCamera.Name == PLAYER_CAMERA)
            {
                targetCamera = virtualCamera;
                break;
            }
        }
        if (targetCamera != null)
        {
            cameraController.ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            targetCamera.VirtualCameraGameObject.SetActive(true);
        }
    }
}
