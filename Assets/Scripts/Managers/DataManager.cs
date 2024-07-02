using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Data data { get; private set; }
    private string SavePath => $"{Application.persistentDataPath}/save.json";

    [SerializeField] private List<InventoryItem> startingItems;

    private void Awake()
    {        
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        data.inventorySlots = InventoryManager.instance.inventorySlots.Select(slot => new SerializableInventorySlot(slot)).ToList();
        SaveData();
    }      

    private void Start()
    {       
        LoadData();
    }

    public void SetPlayerHealth(int amount)
    {
        data.playerHealth = amount;
    }
    public int GetPlayerHealth()
    {
        return data.playerHealth;
    }

    public void SetEnemyHealth(int amount)
    {
        data.enemyHealth = amount;
    }
    public int GetEnemyHealth()
    {
        return data.enemyHealth;
    }

    public void SetPlayerBodyArmor(ClothesItem item)
    {

        data.playerBodyArmor = item;
    }
    public ClothesItem GetPlayerBodyArmor()
    {
        return data.playerBodyArmor;
    }
    public void SetPlayerHeadArmor(ClothesItem item)
    {

        data.playerHeadArmor = item;
    }
    public ClothesItem GetPlayerHeadArmor()
    {
        return data.playerHeadArmor;
    }

    public void SetInventory(List<InventorySlot> playerInventory)
    {
        data.inventorySlots = playerInventory.Select(slot => new SerializableInventorySlot(slot)).ToList();
    }

    public List<InventorySlot> GetInventory()
    {
        return data.inventorySlots.Select(serializableSlot => new InventorySlot(serializableSlot)).ToList();
    }

    private void LoadData()
    {
        if (!File.Exists(SavePath))
        {
            SaveData();
        }

        string json = File.ReadAllText(SavePath);
        data = JsonUtility.FromJson<Data>(json);

        EventBus.OnDataLoaded();
    }

    private void SaveData()
    {
        if (!File.Exists(SavePath))
        {
            data = new Data()
            {
                playerHealth = 100,
                enemyHealth = 100,
                playerBodyArmor = null,
                playerHeadArmor = null,
                playerCurrentWeapon = null,
                inventorySlots = new List<SerializableInventorySlot>()
            };

            data.inventorySlots = startingItems.Select(item => new SerializableInventorySlot(item, item.maximumAmount)).ToList();
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SavePath, json);
    }

    [Serializable]
    public class Data
    {
        public int playerHealth;
        public int enemyHealth;

        public ClothesItem playerBodyArmor;
        public ClothesItem playerHeadArmor;

        public WeaponItem playerCurrentWeapon;

        public List<SerializableInventorySlot> inventorySlots;
    }
}
