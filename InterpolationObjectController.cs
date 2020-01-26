using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[DefaultExecutionOrder(ORDER_EXECUTION)]
public class InterpolationObjectController : MonoBehaviour
{
    public const int ORDER_EXECUTION = InterpolationFactorController.ORDER_EXECUTION - 1;

    private TransformData[] _transforms;
    private int _index;

    private void Awake()
    {
        StartCoroutine(WaitForEndOfFrame());
        StartCoroutine(WaitForFixedUpdate());
    }

    private void OnEnable()
    {
        ResetTransforms();
    }

    private void BeforeFixedUpdate()
    {
        // Restoring actual transform for the FixedUpdate() cal where it could be change by the user.
        RestoreActualTransform();
    }

    private void AfterFixedUpdate()
    {
        // Saving actual transform for being restored in the BeforeFixedUpdate() method.
        SaveActualTransform();
    }

    private void Update()
    {
        // Set interpolated transform for being rendered.
        SetInterpolatedTransform();
    }

    #region Helpers

    private void RestoreActualTransform()
    {
        var latest = _transforms[_index];
        transform.localPosition = latest.position;
        transform.localScale = latest.scale;
        transform.localRotation = latest.rotation;
    }

    private void SaveActualTransform()
    {
        _index = NextIndex();
        _transforms[_index] = CurrentTransformData();
    }

    private void SetInterpolatedTransform()
    {
        var prev = _transforms[NextIndex()];
        float factor = InterpolationFactorController.Factor;
        transform.localPosition = Vector3.Lerp(prev.position, transform.localPosition, factor);
        transform.localRotation = Quaternion.Slerp(prev.rotation, transform.localRotation, factor);
        transform.localScale = Vector3.Lerp(prev.scale, transform.localScale, factor);
    }

    public void ResetTransforms()
    {
        _index = 0;
        var td = CurrentTransformData();
        _transforms = new TransformData[2] { td, td };
    }

    private TransformData CurrentTransformData()
    {
        return new TransformData(transform.localPosition, transform.localRotation, transform.localScale);
    }

    private int NextIndex()
    {
        return (_index == 0) ? 1 : 0;
    }

    private IEnumerator WaitForEndOfFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            BeforeFixedUpdate();
        }
    }

    private IEnumerator WaitForFixedUpdate()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            AfterFixedUpdate();
        }
    }

    private struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }

    #endregion
}
