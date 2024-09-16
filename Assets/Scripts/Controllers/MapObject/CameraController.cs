using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [Header("ī�޶� lerp")]
    [SerializeField] Transform targetTransfrom;
    [SerializeField] float currentMoveRatio;

    [Header("ī�޶� ��鸮�� ����")]
    [SerializeField] float shakeDuration = 0.5f;  // ��鸲 ���� �ð�
    [SerializeField] float shakeMagnitude = 0.1f; // ��鸲 ���� (ī�޶� �̵��ϴ� ����)

    [Header("ī�޶� ���� ������ �ȳ���")]

    public Vector2 minBounds;          // ī�޶� �̵��� �� �ִ� �ּ� ��ǥ (x, y)
    public Vector2 maxBounds;          // ī�޶� �̵��� �� �ִ� �ִ� ��ǥ (x, y)
    public float cameraHalfHeight;     // ī�޶� ������ ����
    public float cameraHalfWidth;      // ī�޶� �ʺ��� ����

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
        // ī�޶� ������Ʈ ��������
        this.cam = GetComponent<Camera>();

        // ī�޶� ���� �� �ʺ� ��� (2D orthographic ī�޶� ����)
        this.cameraHalfHeight = this.cam.orthographicSize;
        this.cameraHalfWidth = this.cameraHalfHeight * this.cam.aspect;  // ī�޶��� ��Ⱦ��(aspect ratio)�� ����� �ʺ� ���
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

    #region ��鸲 ���
    public void TriggerShake(float _shakeDuration, float _ratio = 1f)
    {
        // ��鸲 ����
        StartCoroutine(Shake(_shakeDuration, _ratio));
    }

    IEnumerator Shake(float _shakeDuration, float _ratio)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.fixedDeltaTime;

            // ������ ��ġ ���� (initialPosition�� ��������)
            float offsetX = Random.Range(-1f, 1f) * this.shakeMagnitude * _ratio;
            float offsetY = Random.Range(-1f, 1f) * this.shakeMagnitude * _ratio;

            // ī�޶��� ��ġ�� ������ ������ ����
            this.transform.localPosition = new Vector3(this.transform.position.x + offsetX, this.transform.position.y + offsetY, -10f);

            // ��鸲�� ���� �پ�鵵�� ó�� (damping)
            this.shakeMagnitude = Mathf.Lerp(this.shakeMagnitude, 0, elapsedTime / this.shakeDuration);

            yield return new WaitForFixedUpdate();
        }
        this.shakeMagnitude = 1f;

    }
    #endregion

    void moveClamp()
    {

        // ī�޶� ȭ�鿡 ���̴� ���� �������� �̵��� �� �ֵ��� ����
        float clampedX = Mathf.Clamp(targetTransfrom.position.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(targetTransfrom.position.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

        // ī�޶� ��ġ ����
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
