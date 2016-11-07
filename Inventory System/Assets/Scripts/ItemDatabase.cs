using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    private XDocument xml;

    GameObject Panel;
    GameObject slotPanel;
    public GameObject shopSlot;
    public GameObject shopItem;
    public int slotAmount;
    public List<GameObject> DBslots = new List<GameObject>();

    void Start()
    {
        LoadItemstoDB();
        Cursor.SetCursor(Resources.Load<Texture2D>("Item icons/drop_cursor"), Vector2.zero, CursorMode.ForceSoftware);


        //Drawing Shop slots
        Panel = GameObject.Find("Shop");
        slotPanel = Panel.transform.FindChild("Shop panel").gameObject;

        for (int i = 0; i < slotAmount; i++)
        {

            DBslots.Add(Instantiate(shopSlot));
            DBslots[i].transform.SetParent(slotPanel.transform);
        }
////////////////////////////////////////////////////////


    }

    void LoadItemstoDB()
    {
        xml = XDocument.Load(Application.dataPath + "/StreamingAssets/ItemDataBase.xml");

        foreach (XElement el in xml.Root.Elements())
        {
            string name = el.Attribute("name").Value;
            int id = int.Parse(el.Attribute("id").Value);
            string desc = el.Attribute("desc").Value;
            int power = int.Parse(el.Attribute("power").Value);
            int speed = int.Parse(el.Attribute("speed").Value);
            string type = el.Attribute("type").Value;
            int maxQuantity = int.Parse(el.Attribute("maxQuantity").Value);
            int price = int.Parse(el.Attribute("price").Value);
            bool quest = bool.Parse(el.Attribute("quest").Value);

            items.Add(new Item(name, id, desc, power, speed, type, maxQuantity, price, quest));
        }
    }
}
