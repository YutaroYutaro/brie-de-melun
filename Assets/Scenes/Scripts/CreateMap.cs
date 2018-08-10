using UnityEngine;
using FogDefine;

public class CreateMap : MonoBehaviour
{
    //生成するマップオブジェクト
    public GameObject[] MapObjectType;

    //生成するマップの大きさ
    public int maxPosX = 5;
    public int maxPosZ = 7;


    //マップ重み表
    private int[,] mapWeight;

    // Use this for initialization
    void Start()
    {
        //マップの座標
        mapWeight = new int[maxPosX, maxPosZ];

        //左下からマップ生成
        for (int posX = 0; posX < maxPosX; ++posX)
        {
            for (int posZ = 0; posZ < maxPosZ; ++posZ)
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
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(posX, posZ, Fog.FOG_FALSE);
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(posX, posZ, Fog.FOG_TRUE);
                }
                else if (posZ == 6 && (posX == 1 || posX == 2 || posX == 3))
                {
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(posX, posZ, Fog.FOG_FALSE);
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(posX, posZ, Fog.FOG_TRUE);
                }
                else
                {
                    mapObject.transform.SetParent(GameObject.Find("FoggyMapObjects").transform);
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(posX, posZ, Fog.FOG_TRUE);
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(posX, posZ, Fog.FOG_TRUE);
                }
            }
        }
    }

    //外部からマップの重み表を取得するメソッド
    public int[,] GetMapWeight()
    {
        return mapWeight;
    }
}