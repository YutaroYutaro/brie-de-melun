using UnityEngine;

public class CreateMap : MonoBehaviour
{
    //生成するマップオブジェクト
    public GameObject[] MapObjectType;

    //生成するマップの大きさ
    public int maxPosX = 5;
    public int maxPosZ = 7;

    //マップオブジェクトを生成する座標
    private int posX = 0;
    private int posZ = 0;

    //マップ重み表
    private int[,] mapWeight;

    // Use this for initialization
    void Start()
    {
        //マップの座標
        mapWeight = new int[maxPosX, maxPosZ];

        //左下からマップ生成
        for (; posX < maxPosX; posX++)
        {
            for (; posZ < maxPosZ; posZ++)
            {
                //生成するマップオブジェクトを選択
                int objectNumber = Random.Range(0, MapObjectType.Length);

                //マップオブジェクトごとの重みを保存
                if (MapObjectType[objectNumber].name == "Field")
                {
                    mapWeight[posX, posZ] = 1;
                }
                else if (MapObjectType[objectNumber].name == "Forest")
                {
                    mapWeight[posX, posZ] = 2;
                }
                else if (MapObjectType[objectNumber].name == "GoldMine")
                {
                    mapWeight[posX, posZ] = 1;
                }
                else if (MapObjectType[objectNumber].name == "Mount")
                {
                    mapWeight[posX, posZ] = 5;
                }

                //オブジェクトの設置位置を設定
                Vector3 position = new Vector3(posX, 0, posZ);
                Quaternion q = new Quaternion();
                q = Quaternion.identity;

                //オブジェクトの生成
                GameObject mapObject = Instantiate(MapObjectType[objectNumber], position, q);

                if (posZ == 0 && (posX == 1 || posX == 2 || posX == 3))
                {
                    mapObject.transform.SetParent(GameObject.Find("ClearMapObjects").transform);
                }
                else
                {
                    mapObject.transform.SetParent(GameObject.Find("FoggyMapObjects").transform);
                }
            }

            posZ = 0;
        }

        posX = 0;
    }

    //外部からマップの重み表を取得するメソッド
    public int[,] GetMapWeight()
    {
        return mapWeight;
    }
}