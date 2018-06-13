using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    //NavMeshAgent agent;

    float distance = 0.0f;
    CharacterController controller;

    void Start()
    {
        //メインカメラとオブジェクトの距離を測定
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Debug.Log("distance ->" + distance);

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //クリックしたスクリーン座標を取得
            Vector3 mousePos = Input.mousePosition;
            Debug.Log(mousePos);

            //メインカメラとオブジェクトの距離をクリックした座標の高さに代入
            mousePos.z = distance;
            Debug.Log(mousePos);

            //ワールド座標に変換
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

            //座標の四捨五入
            worldPoint.x = Mathf.RoundToInt(worldPoint.x);
            worldPoint.z = Mathf.RoundToInt(worldPoint.z);

            //オブジェクトの移動
            transform.position = worldPoint;
            //controller.SimpleMove(worldPoint);
            Debug.Log(worldPoint);


        }
    }
}