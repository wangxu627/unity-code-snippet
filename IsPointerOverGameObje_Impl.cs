public static bool IsPointerOverGameObject() {
    //if (Input.touchCount > 0) {
                    
    //    int id = Input.GetTouch(0).fingerId;
    //    return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(id);//安卓机上不行
    //}
    //else {
        //return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;

        List<RaycastResult> list = new List<RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, list);
        //Debug.Log(list.Count);
        return list.Count > 0;
    // }
}
//方法二 通过UI事件发射射线
//是 2D UI 的位置，非 3D 位置
public static bool IsPointerOverGameObject(Vector2 screenPosition) {
    //实例化点击事件
    PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
    //将点击位置的屏幕坐标赋值给点击事件
    eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

    List<RaycastResult> results = new List<RaycastResult>();
    //向点击处发射射线
    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

    return results.Count > 0;
}
//方法三 通过画布上的 GraphicRaycaster 组件发射射线
public bool IsPointerOverGameObject(Canvas canvas, Vector2 screenPosition) {
    //实例化点击事件
    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //将点击位置的屏幕坐标赋值给点击事件
    eventDataCurrentPosition.position = screenPosition;
    //获取画布上的 GraphicRaycaster 组件
    GraphicRaycaster uiRaycaster = canvas.gameObject.GetComponent<GraphicRaycaster>();

    List<RaycastResult> results = new List<RaycastResult>();
    // GraphicRaycaster 发射射线
    uiRaycaster.Raycast(eventDataCurrentPosition, results);

    return results.Count > 0;
}