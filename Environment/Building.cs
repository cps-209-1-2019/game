﻿//--------------------------------------------------------------------------------------------
//File:   Building.cs
//Desc:   This is the class that contains logic for the Buildings the player will be in.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    //Added public accessibility modifier - Day
    public class Building : WorldObject, ISerialization<Building> 
    {
        public int Width { get; set; }
        public int Length { get; set; }

        public Dictionary<string, Items> Collection;


        //Adds the Item object in its params to the Collection
        public void AddItem(Items item)
        {
            Collection[item.Name] = item;
        }

        //Removes an item object from the Collection of items in the Building
        public void RmvItm(Items item)
        {
            Collection.Remove(item.Name);
        }

        //Moves the map with respect to the player position
        public void Move(int[] pPos)
        {

        }

        //Turn the object into a string
        public string Serialize()
        {
            throw new NotImplementedException();
        }

        //Take a string and turn it into a Building object
        public Building Deserialize(string obj)
        {
            string[] properties = obj.Split(',');

            Width = int.Parse(properties[1].Split('!')[1]);
            Length = int.Parse(properties[2].Split('!')[1]);

            string[] collectA = properties[3].Split('!');
            string[] collectB = collectA[1].Split(';');
            foreach (string s in collectB)
            {
                string[] item = s.Split();
                string value = item[1].Trim('[',']');
                string[] nextObj = value.Split(',');
                Items nextItem = new Items();
                switch (nextObj[0])
                {
                    case "INVENTORYITEM":
                        InventoryItem inven = new InventoryItem();
                        inven.Deserialize(value);
                        nextItem = inven as Items;
                        break;
                    case "DECOYITEM":
                        DecoyItem decoy = new DecoyItem();
                        decoy.Deserialize(value);
                        nextItem = decoy as Items;
                        break;
                }

                Collection.Add(item[0],nextItem);
            }



            return this;
        }

        
    }
}
