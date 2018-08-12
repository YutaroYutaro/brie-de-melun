using UnityEngine;
using FogDefine;

public class CreateMap : MonoBehaviour
{
    //生成するマップオブジェクト
    public GameObject[] MapObjectType;

    public Material FogMaterial;

    //生成するマップの大きさ
    public int maxPosX = 5;
    public int maxPosZ = 7;


    //マップ重み表
    private int[,] mapWeight;
    private int[,] mapObjectTypeTable = new int[5, 7];

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
                    mapObjectTypeTable[posX, posZ] = 0;
                }
                else if (MapObjectType[objectNumber].name == "Forest")
                {
                    mapWeight[posX, posZ] = 2;
                    mapObjectTypeTable[posX, posZ] = 1;
                }
                else if (MapObjectType[objectNumber].name == "GoldMine")
                {
                    mapWeight[posX, posZ] = 1;
                    mapObjectTypeTable[posX, posZ] = 2;
                }
                else if (MapObjectType[objectNumber].name == "Mount")
                {
                    mapWeight[posX, posZ] = 5;
                    mapObjectTypeTable[posX, posZ] = 3;
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
                        .SetPlayerOneFogMapState(posX, posZ, Fog.FOG_NOT_EXIST);
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(posX, posZ, Fog.FOG_EXIST);

                }
                else if (posZ == 6 && (posX == 1 || posX == 2 || posX == 3))
                {
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(posX, posZ, Fog.FOG_NOT_EXIST);
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(posX, posZ, Fog.FOG_EXIST);
                    mapObject.GetComponent<Renderer>().material.color = FogMaterial.color;
                }
                else
                {
                    mapObject.transform.SetParent(GameObject.Find("FoggyMapObjects").transform);
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(posX, posZ, Fog.FOG_EXIST);
                    GameObject.Find("FogManager").GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(posX, posZ, Fog.FOG_EXIST);
                    mapObject.GetComponent<Renderer>().material.color = FogMaterial.color;
                }
            }
        }
    }

    //外部からマップの重み表を取得するメソッド
    public int[,] GetMapWeight()
    {
        return mapWeight;
    }

    public int GetMapObjectTypeTable(int posX, int posZ)
    {
        return mapObjectTypeTable[posX, posZ];
    }
}