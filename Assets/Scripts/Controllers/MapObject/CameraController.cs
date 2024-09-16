using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [Header("카메라 lerp")]
    [SerializeField] Transform targetTransfrom;
    [SerializeField] float currentMoveRatio;

    [Header("카메라 흔들리는 연출")]
    [SerializeField] float shakeDuration = 0.5f;  // 흔들림 지속 시간
    [SerializeField] float shakeMagnitude = 0.1f; // 흔들림 강도 (카메라가 이동하는 범위)

    [Header("카메라 범위 밖으로 안나감")]

    public Vector2 minBounds;          // 카메라가 이동할 수 있는 최소 좌표 (x, y)
    public Vector2 maxBounds;          // 카메라가 이동할 수 있는 최대 좌표 (x, y)
    public float cameraHalfHeight;     // 카메라 높이의 절반
    public float cameraHalfWidth;      // 카메라 너비의 절반

    private Camera cam;

    private void Awake()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
        SmoothMove();
        moveClamp();
    }

    void Start()
    {
        // 카메라 컴포넌트 가져오기
        this.cam = GetComponent<Camera>();

        // 카메라 높이 및 너비 계산 (2D orthographic 카메라 기준)
        this.cameraHalfHeight = this.cam.orthographicSize;
        this.cameraHalfWidth = this.cameraHalfHeight * this.cam.aspect;  // 카메라의 종횡비(aspect ratio)를 사용해 너비 계산
    }

    void SmoothMove()
    {
        if (this.targetTransfrom.position != this.transform.position)
        {
            Vector3 t_pos = Vector3.zero;
            float distance = Vector2.Distance(this.transform.position, this.targetTransfrom.position);
            float t = currentMoveRatio * Time.fixedDeltaTime / distance;
            t_pos = Vector2.Lerp(this.transform.position, this.targetTransfrom.position, t);
            t_pos.z = -10f;
            this.transform.position = t_pos;
        }
    }

    #region 흔들림 기능
    public void TriggerShake(float _shakeDuration, float _ratio = 1f)
    {
        // 흔들림 시작
        StartCoroutine(Shake(_shakeDuration, _ratio));
    }

    IEnumerator Shake(float _shakeDuration, float _ratio)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.fixedDeltaTime;

            // 랜덤한 위치 생성 (initialPosition을 기준으로)
            float offsetX = Random.Range(-1f, 1f) * this.shakeMagnitude * _ratio;
            float offsetY = Random.Range(-1f, 1f) * this.shakeMagnitude * _ratio;

            // 카메라의 위치를 랜덤한 값으로 조정
            this.transform.localPosition = new Vector3(this.transform.position.x + offsetX, this.transform.position.y + offsetY, -10f);

            // 흔들림이 점차 줄어들도록 처리 (damping)
            this.shakeMagnitude = Mathf.Lerp(this.shakeMagnitude, 0, elapsedTime / this.shakeDuration);

            yield return new WaitForFixedUpdate();
        }
        this.shakeMagnitude = 1f;

    }
    #endregion

    void moveClamp()
    {

        // 카메라가 화면에 보이는 범위 내에서만 이동할 수 있도록 제한
        float clampedX = Mathf.Clamp(targetTransfrom.position.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(targetTransfrom.position.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

        // 카메라 위치 갱신
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void SetMapBoundary(Vector2 _mapsize)
    {
        this.minBounds.x = -_mapsize.x / 2;
        this.minBounds.y = -_mapsize.y / 2;

        this.maxBounds.x = _mapsize.x / 2;
        this.maxBounds.y = _mapsize.y / 2;
    }


}
