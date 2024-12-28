using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = (T)FindObjectOfType(typeof(T));

            if (_instance == null)
            {
                Debug.LogError(typeof(T) + "は存在しません");
            }

            return _instance;
        }
    }

    public static T InstanceNullable => _instance;

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError(typeof(T) + "は複数存在します", this);
            return;
        }

        _instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}