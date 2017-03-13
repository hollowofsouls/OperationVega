
namespace Assets.Scripts.Controllers
{
    using System;

    using UnityEngine;

    /// <summary>
    /// The camera controller class.
    /// Handles the camera movement functionality
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The pan with mouse reference.
        /// </summary>
        public static bool Panwithmouse;

        /// <summary>
        /// The mouse position x.
        /// </summary>
        private float mousePosX;

        /// <summary>
        /// The border offset reference.
        /// </summary>
        private float borderoffset;

        /// <summary>
        /// The speed at which the camera moves.
        /// </summary>
        /// [HideInInspector]
        public uint MoveSpeed;

        /// <summary>
        /// The rotate speed of the camera.
        /// </summary>
        /// [HideInInspector]
        public uint RotateSpeed;

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            Panwithmouse = false;
            this.borderoffset = 20;
        }

        /// <summary>
        /// The update function.
        /// </summary>
        private void Update()
        {
            this.ZoomCamera();
            this.PanCamera();

            this.MoveSpeed = (uint)Mathf.Clamp(this.MoveSpeed, 3, 5);
            this.RotateSpeed = (uint)Mathf.Clamp(this.RotateSpeed, 3, 5);
        }

        /// <summary>
        /// The late update function.
        /// </summary>
        private void LateUpdate()
        {
            this.RotateCamera();
            this.mousePosX = Input.mousePosition.x;
        }

        /// <summary>
        /// The pan camera function.
        /// Moves the camera based on key presses or mouse position.
        /// </summary>
        private void PanCamera()
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += this.transform.forward * this.MoveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.position -= this.transform.forward * this.MoveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position -= this.transform.right * this.MoveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += this.transform.right * this.MoveSpeed * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.transform.eulerAngles = Vector3.zero;
                this.transform.position = Vector3.zero;
            }

            if (Panwithmouse)
            {
                if (Input.mousePosition.x >= Screen.width - this.borderoffset && Input.mousePosition.x < Screen.width)
                {
                    this.transform.position += this.transform.right * this.MoveSpeed * Time.deltaTime;
                }
                if (Input.mousePosition.x <= this.borderoffset && Input.mousePosition.x > 0)
                {
                    this.transform.position -= this.transform.right * this.MoveSpeed * Time.deltaTime;
                }
                if (Input.mousePosition.y >= Screen.height - this.borderoffset && Input.mousePosition.y < Screen.height)
                {
                    this.transform.position += this.transform.forward * this.MoveSpeed * Time.deltaTime;
                }
                if (Input.mousePosition.y <= this.borderoffset && Input.mousePosition.y > 0)
                {
                    this.transform.position -= this.transform.forward * this.MoveSpeed * Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                Panwithmouse = !Panwithmouse;
            }
        }

        /// <summary>
        /// The zoom camera.
        /// </summary>
        private void ZoomCamera()
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                Camera.main.orthographicSize += 0.25f;
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                Camera.main.orthographicSize -= 0.25f;
            }

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 7, 10);
        }

        /// <summary>
        /// The rotate camera.
        /// </summary>
        private void RotateCamera()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.mousePosition.x != this.mousePosX)
                {
                    float camroty = (Input.mousePosition.x - this.mousePosX) * this.RotateSpeed * Time.deltaTime;
                    this.transform.Rotate(0.0f, camroty, 0.0f);
                    this.transform.eulerAngles = new Vector3(0, this.ClampAngle(this.transform.eulerAngles.y, -45.0f, 45.0f), 0);
                }
            }
        }

        /// <summary>
        /// The Clamp angle function.
        /// Clamps the angle to passed in arguments.
        /// <para></para>
        /// <remarks><paramref name="angle"></paramref> -The angle to rotate.</remarks>
        /// <para></para>
        /// <remarks><paramref name="min"></paramref> -The lowest angle allowed.</remarks>
        /// <para></para>
        /// <remarks><paramref name="max"></paramref> -The max angle allowed.</remarks>
        /// </summary>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        private float ClampAngle(float angle, float min, float max)
        {
            // Make sure the numbers are never less than 0 and greater than 360.
            angle = Mathf.Repeat(angle, 360);
            min = Mathf.Repeat(min, 360);
            max = Mathf.Repeat(max, 360);
            bool inverse = false;
            float tmin = min;
            float tangle = angle;
            if (min > 180)
            {
                inverse = !inverse;
                tmin -= 180;
            }
            if (angle > 180)
            {
                inverse = !inverse;
                tangle -= 180;
            }
            bool result = !inverse ? tangle > tmin : tangle < tmin;
            if (!result)
                angle = min;

            inverse = false;
            tangle = angle;
            float tmax = max;
            if (angle > 180)
            {
                inverse = !inverse;
                tangle -= 180;
            }
            if (max > 180)
            {
                inverse = !inverse;
                tmax -= 180;
            }

            result = !inverse ? tangle < tmax : tangle > tmax;
            if (!result)
                angle = max;

            return angle;
        }
    }
}