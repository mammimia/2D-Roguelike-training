using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!LevelManager.instance.isPaused)
            {
                bool isMapCameraActive = bigMapCamera.enabled;

                bigMapCamera.enabled = !isMapCameraActive;
                mainCamera.enabled = isMapCameraActive;
                PlayerController.instance.canMove = !isMapCameraActive;
                Time.timeScale = !isMapCameraActive ? 1f : 0f;
            }
        }
    }

    public void setTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
