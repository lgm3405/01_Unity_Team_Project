using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject inventoryUi;
    public GameObject[] noneSlot = new GameObject[5];
    public GameObject[] actSlot = new GameObject[5];
    public GameObject[] inventorySlot = new GameObject[5];
    public GameObject[] inventoryColor = new GameObject[5];
    
    //ȹ���� �������� �� �̰��� ����
    public List<Items> activeItems;
    public List<Items> durationItems;

    public Items[] equipItems;

    private int selectSlot = default;
    private int selectInventory = default;
    private int selectInventoryIndex = default;

    private bool lookAtInventorySlot = false;
    
    void Awake()
    {
        equipItems = new Items[5];
        activeItems = new List<Items>();
        durationItems = new List<Items>();
        inventoryUi = GetComponent<GameObject>();

        activeItems.Add(/*�̸��� none�� ������*/null);
        durationItems.Add(/*�̸��� none�� ������*/null);
        
        selectInventoryIndex = 0;
    }

    //������ ȹ��� �ش� �Լ��� �θ���.
    public void GetItem(Items item)
    {
        if(item.type == ItemType.ACTIVE)
        {
            activeItems.Add(item);
        }
        else if (item.type == ItemType.DURATION)
        {
            durationItems.Add(item);
        }
    }

    public void SelectItems()
    {
        //Ȱ�� ������ ����
        if (selectSlot <= 2)
        {
            for (int i = 0; i < activeItems.Count; i++)
            {
                //������ �κ��丮�� ���̰� �Ѵ�.(�̸���)                
            }
        }
        else
        {
            for (int i = 0; i < durationItems.Count; i++)
            {
                //������ �κ��丮�� ���̰� �Ѵ�.(�̸���)                
            }
        }
    }

    //�ش� �κ��丮 ��Ͽ��� Ȯ��Ű�� ������� ������ ����
    void EquipItem()
    {
        List<Items> list;
        //Ȱ�� ������ ����
        if (selectSlot <= 2)
        {
            list = activeItems;
        }
        else
        {
            list = durationItems;
        }

        if (selectInventoryIndex == 0)
        {
            list.Add(equipItems[selectSlot]);
            equipItems[selectSlot] = null;
        }
        else
        {
            equipItems[selectSlot] = list[selectInventoryIndex];
            list.RemoveAt(selectInventoryIndex);
        }

        selectInventoryIndex = 0;
        lookAtInventorySlot = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && GameManager.instance.lookAtInventory == true)
        {
            if (lookAtInventorySlot == false)
            {
                Time.timeScale = 1f;
                actSlot[selectSlot].SetActive(false);
                noneSlot[selectSlot].SetActive(true);
                StartCoroutine(InventoryClosed());
                GameManager.instance.inventoryUi.SetActive(false);
                GetComponent<ItemManager>().enabled = false;
            }
            else
            {
                lookAtInventorySlot = false;
                inventoryColor[selectInventory].SetActive(false);
                noneSlot[selectSlot].SetActive(false);
                actSlot[selectSlot].SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && lookAtInventorySlot == false)
        {
            actSlot[selectSlot].SetActive(false);
            noneSlot[selectSlot].SetActive(true);

            if (selectSlot == 0) { selectSlot = 4; }
            else { selectSlot -= 1; }

            noneSlot[selectSlot].SetActive(false);
            actSlot[selectSlot].SetActive(true);

            SelectItems();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && lookAtInventorySlot == false)
        {
            actSlot[selectSlot].SetActive(false);
            noneSlot[selectSlot].SetActive(true);

            if (selectSlot == 4) { selectSlot = 0; }
            else { selectSlot += 1; }

            noneSlot[selectSlot].SetActive(false);
            actSlot[selectSlot].SetActive(true);

            SelectItems();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (lookAtInventorySlot == false)
            {
                actSlot[selectSlot].SetActive(false);
                noneSlot[selectSlot].SetActive(true);

                if (selectSlot == 2) { selectSlot = 4; }
                else if (selectSlot <= 1) { selectSlot += 3; }
                else if (selectSlot >= 3) { selectSlot -= 3; }

                noneSlot[selectSlot].SetActive(false);
                actSlot[selectSlot].SetActive(true);

                SelectItems();
            }
            else
            {
                if (selectInventory == 0)
                {
                    inventoryColor[selectInventory].SetActive(false);

                    selectInventory = 4;

                    inventoryColor[selectInventory].SetActive(true);
                }
                else
                {
                    inventoryColor[selectInventory].SetActive(false);

                    selectInventory -= 1;

                    inventoryColor[selectInventory].SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (lookAtInventorySlot == false)
            {
                actSlot[selectSlot].SetActive(false);
                noneSlot[selectSlot].SetActive(true);

                if (selectSlot == 2) { selectSlot = 4; }
                else if (selectSlot <= 1) { selectSlot += 3; }
                else if (selectSlot >= 3) { selectSlot -= 3; }

                noneSlot[selectSlot].SetActive(false);
                actSlot[selectSlot].SetActive(true);

                SelectItems();
            }
            else
            {
                if (selectInventory == 4)
                {
                    inventoryColor[selectInventory].SetActive(false);

                    selectInventory = 0;

                    inventoryColor[selectInventory].SetActive(true);
                }
                else
                {
                    inventoryColor[selectInventory].SetActive(false);

                    selectInventory += 1;

                    inventoryColor[selectInventory].SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && lookAtInventorySlot == false)
        {
            lookAtInventorySlot = true;
            actSlot[selectSlot].SetActive(false);
            noneSlot[selectSlot].SetActive(true);

            inventoryColor[selectInventory].SetActive(true);
        }
    }

    IEnumerator InventoryClosed()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.lookAtInventory = false;
    }

    void OnEnable()
    {
        selectSlot = 0;
        selectInventory = 0;
        noneSlot[selectSlot].SetActive(false);
        actSlot[selectSlot].SetActive(true);
        lookAtInventorySlot = false;
    }

    private void OnDisable()
    {
        Debug.Log("�κ��丮�� ��Ȱ��ȭ �Ǿ���.");
    }
}
