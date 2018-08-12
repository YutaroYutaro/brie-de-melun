using UnityEngine;
using FogDefine;

public class CreateMap : MonoBehaviour
{
    //生成するマップオブジェクト
    public GameObject[] MapObjectType;

    public Material FogMaterial;

    //生成するマップの大きさ
    public int MaxPosX = 5;
    public int MaxPosZ = 7;


    //マップ重み表
    private int[,] _mapWeight;
    private int[,] _mapObjectTypeTable;

    // Use this for initialization
    void Start()
    {
        //マップの座標
        _mapWeight = new int[MaxPosX, MaxPosZ];
        _mapObjectTypeTable = new int[MaxPosX, MaxPosZ];

        //左下からマップ生成
        for (int posX = 0; posX < MaxPosX; ++posX)
        {
            for (int posZ = 0; posZ < MaxPosZ; ++posZ)
            {
                //生成するマップオブジェクトを選択
                int objectNumber = Random.Range(0, MapObjectType.Length);

                //マップオブジェクトごとの重みを保存
                if (MapObjectType[objectNumber].name == "Field")
                {
                    _mapWeight[posX, posZ] = 1;
                    _mapObjectTypeTable[posX, posZ] = 0;
                }
                else if (MapObjectType[objectNumber].name == "Forest")
                {
                    _mapWeight[posX, posZ] = 2;
                    _mapObjectTypeTable[posX, posZ] = 1;
                }
                else if (MapObjectType[objectNumber].name == "GoldMine")
                {
                    _mapWeight[posX, posZ] = 1;
                    _mapObjectTypeTable[posX, posZ] = 2;
                }
                else if (MapObjectType[objectNumber].name == "Mount")
                {
                    _mapWeight[posX, posZ] = 5;
                    _mapObjectTypeTable[posX, posZ] = 3;
                }

                //オブジェクトの生成
                GameObject mapObject = Instantiate(
                    MapObjectType[objectNumber],
                    new Vector3(posX, 0, posZ),
                    Quaternion.identity
                );

                if (posZ == 0 && (posX == 1 || posX == 2 || posX == 3))
                {
                    mapObject.transform.SetParent(GameObject.Find("ClearMapObjects").transform);
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_NOT_EXIST
                        );
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_EXIST
                        );
                }
                else if (posZ == 6 && (posX == 1 || posX == 2 || posX == 3))
                {
                    mapObject.transform.SetParent(GameObject.Find("FoggyMapObjects").transform);
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_NOT_EXIST
                        );
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_EXIST
                        );
                    mapObject.GetComponent<Renderer>().material.color = FogMaterial.color;
                }
                else
                {
                    mapObject.transform.SetParent(GameObject.Find("FoggyMapObjects").transform);
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_EXIST
                        );
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_EXIST
                        );
                    mapObject.GetComponent<Renderer>().material.color = FogMaterial.color;
                }
            }
        }
    }

    //外部からマップの重み表を取得するメソッド
    public int[,] GetMapWeight()
    {
        return _mapWeight;
    }

    public int GetMapObjectTypeTable(int posX, int posZ)
    {
        return _mapObjectTypeTable[posX, posZ];
    }
}