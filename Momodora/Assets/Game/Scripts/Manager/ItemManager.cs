using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public GameObject inventoryUi;

    public bool lookAtInventory = false;
    public bool inventoryCheckTime = false;

    // ������ ���� ���̽�
    Dictionary<string, Items> itemDataBase = new Dictionary<string, Items>();

    void Awake()
    {
        if (instance == null || instance == default)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(InventoryCheck());

        itemDataBase.Add("[None]", new None());
        itemDataBase.Add("��� ����", new Items1());
        itemDataBase.Add("���� ����", new Items2());
        itemDataBase.Add("�ƽ�Ʈ�� ����", new Items3());
        itemDataBase.Add("�ʷղ�", new Items4());
    }

    // ������ ȹ�� �� ���� �ޱ� (2)
    public Items ItemData(string name)
    {
        if (itemDataBase.ContainsKey(name))
        {
            Items item = itemDataBase[name];

            return item;
        }
        else
        {
            return null;
        }
    }

    IEnumerator InventoryCheck()
    {
        inventoryCheckTime = true;
        ItemManager.instance.GetComponent<Inventory>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        ItemManager.instance.GetComponent<Inventory>().enabled = false;
        inventoryCheckTime = false;

    }
}
