using UnityEngine;

[DisallowMultipleComponent]
[DefaultExecutionOrder(ORDER_EXECUTION)]
public class InterpolationFactorController : MonoBehaviour
{
    public const int ORDER_EXECUTION = -1000;

    private static InterpolationFactorController Instance;
    private float[] _lastFixedUpdates = new float[2];
    private int _lastIndex;

    public static float Factor { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            Debug.LogWarning($"The '{typeof(InterpolationFactorController).Name}' is a singleton!");
            return;
        }

        Instance = this;
        Factor = 1;
    }

    private void Start()
    {
        _lastFixedUpdates = new float[2] { Time.fixedTime, Time.fixedTime };
        _lastIndex = 0;
    }

    private void FixedUpdate()
    {
        _lastIndex = NextIndex();
        _lastFixedUpdates[_lastIndex] = Time.fixedTime;
    }

    private void Update()
    {
        float lastTime = _lastFixedUpdates[_lastIndex];
        float prevTime = _lastFixedUpdates[NextIndex()];

        if (lastTime == prevTime)
        {
            Factor = 1;
            return;
        }

        Factor = (Time.time - lastTime) / (lastTime - prevTime);
    }

    private int NextIndex()
    {
        return (_lastIndex == 0) ? 1 : 0;
    }
}
