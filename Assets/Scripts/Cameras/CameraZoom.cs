using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace MovementSystem
{
    public class CameraZoom : MonoBehaviour
    {
        [FormerlySerializedAs("defaultDistance")] [SerializeField] [Range(0f, 12f)] private float _defaultDistance = 6f;
        [FormerlySerializedAs("minimumDistance")] [SerializeField] [Range(0f, 12f)] private float _minimumDistance = 1f;
        [FormerlySerializedAs("maximumDistance")] [SerializeField] [Range(0f, 12f)] private float _maximumDistance = 6f;

        [FormerlySerializedAs("smoothing")] [SerializeField] [Range(0f, 20f)] private float _smoothing = 4f;
        [FormerlySerializedAs("zoomSensitivity")] [SerializeField] [Range(0f, 20f)] private float _zoomSensitivity = 1f;

        private CinemachineFramingTransposer _framingTransposer;
        private CinemachineInputProvider _inputProvider;

        private float _currentTargetDistance;

        private void Awake()
        {
            _framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            _inputProvider = GetComponent<CinemachineInputProvider>();

            _currentTargetDistance = _defaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            float zoomValue = _inputProvider.GetAxisValue(2) * _zoomSensitivity;

            _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, _minimumDistance, _maximumDistance);

            float currentDistance = _framingTransposer.m_CameraDistance;

            if (currentDistance == _currentTargetDistance)
            {
                return;
            }

            float lerpedZoomValue = Mathf.Lerp(currentDistance, _currentTargetDistance, _smoothing * Time.deltaTime);

            _framingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}