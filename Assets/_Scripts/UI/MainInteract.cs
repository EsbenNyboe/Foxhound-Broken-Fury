using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrokenFury.Test
{
    public class MainInteract : MonoBehaviour
    {
        int minHorizontal;
        int minVertical;
        int maxHorizontal;
        int maxVertical;
        [RangeAttribute(1, 100)]
        public int cameraMoveSpeed;
        [RangeAttribute(1, 500)]
        public int cameraZoomSpeed;
        [RangeAttribute(.01f, .2f)]
        public float cameraMoveThreshold;
        public GameObject pfFlag;

        // Start is called before the first frame update
        void Start()
        {
            GetRange();

        }

        // Update is called once per frame
        void Update()
        {
            MoveCamera();

        }

        void GetRange()
        {
            // add way to calculate that
            minHorizontal = -10;
            minVertical = -10;
            maxHorizontal = 23;
            maxVertical = 10;
        }

        void MoveCamera()
        {
            // Add zoom in/out limits
            float mouseNormalX = Mathf.Clamp(Input.mousePosition.x / Screen.width, 0, 1);
            float mouseNormalY = Mathf.Clamp(Input.mousePosition.y / Screen.height, 0, 1);
            Vector3 moveAmount = new Vector3(0, 0, 0);
            Vector3 zoomAmount = new Vector3(0, 0, 0);
            if (mouseNormalX < cameraMoveThreshold)
            {
                moveAmount.z += cameraMoveSpeed * (1 - mouseNormalX / cameraMoveThreshold) * Time.deltaTime;
            }
            else if (mouseNormalX > (1 - cameraMoveThreshold))
            {
                moveAmount.z -= cameraMoveSpeed * ((mouseNormalX - (1 - cameraMoveThreshold)) / cameraMoveThreshold) * Time.deltaTime;
            }
            if (mouseNormalY < cameraMoveThreshold)
            {
                moveAmount.x -= cameraMoveSpeed * (1 - mouseNormalY / cameraMoveThreshold) * Time.deltaTime;
            }
            else if (mouseNormalY > (1 - cameraMoveThreshold))
            {
                moveAmount.x += cameraMoveSpeed * ((mouseNormalY - (1 - cameraMoveThreshold)) / cameraMoveThreshold) * Time.deltaTime;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Zoom In
            {
                zoomAmount.x += cameraZoomSpeed * Time.deltaTime * Mathf.Cos(30);
                zoomAmount.y -= cameraZoomSpeed * Time.deltaTime * Mathf.Cos(30);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // Zoom Out
            {
                zoomAmount.x -= cameraZoomSpeed * Time.deltaTime * Mathf.Cos(30);
                zoomAmount.y += cameraZoomSpeed * Time.deltaTime * Mathf.Cos(30);
            }

            transform.position += moveAmount;
            transform.position += zoomAmount;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minVertical, maxVertical), Mathf.Clamp(transform.position.y, 1, 10), Mathf.Clamp(transform.position.z, minHorizontal, maxHorizontal));
        }

    }
}
