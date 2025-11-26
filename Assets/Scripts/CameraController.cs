using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        /*三、潜在问题（必看！）
        这段代码看似简洁，但存在几个关键问题，不建议直接在性能敏感的逻辑中使用：
        性能开销大FindObjectOfType<>() 会遍历场景中所有游戏对象和组件，场景对象越多，开销越大。如果在 Update() 等每帧执行的方法中调用，会严重拖慢帧率。
        查找结果不确定若场景中没有 PlayerController，会返回 null，后续访问 target.position 等属性会直接报 空引用错误（NullReferenceException）。若场景中有多个 PlayerController（如双人游戏），只会返回第一个找到的，可能不符合预期。
        耦合性高代码直接依赖 PlayerController 脚本的存在，若后续重构脚本名称（如改为 PlayerCtrl），会导致代码失效。*/
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
    }
}
