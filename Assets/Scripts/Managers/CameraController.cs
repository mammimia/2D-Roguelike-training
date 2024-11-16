using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    public bool isBossRoom;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (isBossRoom)
        {
            target = PlayerController.instance.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.M) && !isBossRoom)
        {
            if (!LevelManager.instance.isPaused)
            {
                bool isMapCameraActive = bigMapCamera.enabled;

                bigMapCamera.enabled = !isMapCameraActive;
                mainCamera.enabled = isMapCameraActive;
                PlayerController.instance.canMove = !isMapCameraActive;
                Time.timeScale = !isMapCameraActive ? 1f : 0f;
                UIController.instance.miniMap.SetActive(!isMapCameraActive);
                UIController.instance.bigMapText.SetActive(isMapCameraActive);
            }
        }
    }

    public void setTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
