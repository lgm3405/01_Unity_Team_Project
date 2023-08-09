using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.Tilemaps;


//�׽�Ʈ �뵵�� Ȯ�� ������ ������
//�÷��� Ÿ���� �׽�Ʈ�� �ʿ��� �͵��� ��ư�� �����ؼ� ����� ����
public class TestWindow : EditorWindow
{
    /// <summary>
    /// Legacy�ڵ�
    /// </summary>
    //public EnemyCommon_Legacy obj;
    //Queue<GameObject> testList = new Queue<GameObject>();
    //===========================================================================================

    string[] groundTileNames;

    //�޴��� �߰�
    [MenuItem("CustomMenu/BuildTest")]
    static void OpenTestWindow()
    {
        TestWindow window = (TestWindow)EditorWindow.GetWindow(typeof(TestWindow));
        window.Show();
    }

    private void OnEnable()
    {

        /*string folderPath = Application.dataPath + "/Resources/Tiles/Ground";
        string[] groundTileStrings = System.IO.Directory.GetFiles(folderPath, "*.prefab");

        groundTileNames = new string[groundTileStrings.Length];

        for (int i = 0; i < groundTileStrings.Length; i++)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(groundTileStrings[i]);
            groundTileNames[i] = filename;
        }
*/

        //tilePrefab = Resources.Load<gameobject>(prefabPath);
    }

    void OnGUI()
    {
        if (GUILayout.Button("RandomEnemy"))
        {
            //�⺻���� 1 ,1
            int width = 1;
            int height = 1;

            //��������, ���� enum���� ����
            int stage = 1;

            GameObject map = new GameObject();
            map.name = "Stage"+(stage);

            GameObject tileGrid = new GameObject();
            tileGrid.AddComponent<Grid>();
            tileGrid.transform.parent = map.transform;
            tileGrid.name = "Grid";

            GameObject tmpTileMapObject = new GameObject();
            Tilemap tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
            tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
            tmpTileMapObject.AddComponent<TilemapRenderer>();
            Rigidbody2D rb = tmpTileMapObject.AddComponent <Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            TilemapCollider2D tilemapCollider = tmpTileMapObject.AddComponent<TilemapCollider2D>();
            tilemapCollider.usedByComposite = true;
            CompositeCollider2D compositeCollider = tmpTileMapObject.AddComponent<CompositeCollider2D>();
            compositeCollider.usedByEffector = true;
            PlatformEffector2D platformEffector = tmpTileMapObject.AddComponent<PlatformEffector2D>();
            platformEffector.useOneWay = false;

            tmpTileMapObject.transform.parent = tileGrid.transform;
            tmpTileMapObject.name = "BaseTile";

            GridPalette
            //tilePrefab = Resources.Load<gameobject>(prefabPath);
            TileBase tileBase = 
            //tmpTilemap.SetTile(new Vector3Int(0, 0, 0),   );

        }


        ///<summary>
        /// Legacy�ڵ�
        ///</summary>
            /* if (GUILayout.Button("RandomEnemy"))
             {
                 string[] enemiesName = EnemyPool_Legacy.instance.GetEnemiesName();
                 GameObject tmp = EnemyPool_Legacy.instance.GetEnemy(enemiesName[Random.Range(0, enemiesName.Length)]);
                 tmp.transform.position = Vector3.zero;
                 testList.Enqueue(tmp);
             }

             if (GUILayout.Button("RemoveObject"))
             {
                 GameObject tmp = testList.Dequeue();
                 EnemyPool_Legacy.instance.ReturnEnemy(tmp);
             }


             if (GUILayout.Button("Shoot"))
             {
                 EnemyCommon_Legacy tmp = AssetDatabase.LoadAssetAtPath<EnemyCommon_Legacy>("Assets/Game/Prefabs/Enemies/BigTomata.prefab");

                 Debug.Log(tmp);

                 obj = Instantiate(tmp, new Vector3(0,0,0), Quaternion.identity);
                 obj.Attack();
             }*/
    }
}
