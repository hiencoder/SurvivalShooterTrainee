using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWindow : MonoBehaviour
{
    public Vector2 range = new Vector2(5f, 3f);
    Transform mTransform;
    Quaternion mStart;
    Vector2 mRot = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        mTransform = transform;
        mStart = mTransform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        float halfWidth = Screen.width;
        float halfHeight = Screen.height;
        float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
        float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, 1f, 1f);
        mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime);

        mTransform.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, mRot.x * range.x, 0f);
    }
}
