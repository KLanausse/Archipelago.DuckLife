using System;
using System.Reflection;
using UnityEngine;
using MelonLoader;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;

namespace Archipelago.DuckLife
{
    public class ArchipelagoMod : MelonMod
    {
        public static ArchipelagoMod instance;

        public int currentGame = 0;
        //1 = DuckLife 1
        //2 = DuckLife 2
        //3 = DuckLife 3


        public override void OnEarlyInitializeMelon()
        {
            instance = this;
            var thisAssembly = Assembly.GetExecutingAssembly();

        }

        public override void OnLateInitializeMelon()
        {
            Application.LoadLevel("RetroMenu");

            //ARCHIPELAGO
            var session = ArchipelagoSessionFactory.CreateSession("localhost", 38281);
            LoginResult result = session.TryConnectAndLogin("Duck Life: Retro Pack", "Duck Life", ItemsHandlingFlags.AllItems);
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            instance.LoggerInstance.Msg($"Scene {sceneName} with build index {buildIndex} has been loaded!");
            if ( sceneName == "RetroMenu" )
            {
                CreateArchipelagoMenu();
                CreateArchipelagoButton();
            }
        }

        public override void OnLateUpdate()
        {
            //if (Input.GetKeyDown(freezeToggleKey))
            //{
            //    ToggleFreeze();
            //}
        }

        //Taken from the MelonLoader Discord https://discord.com/channels/663449315876012052/733305783177183335/1145726297050456208
        public static Texture2D LoadImage(string filename)
        {
            var a = Assembly.GetExecutingAssembly();
            var spriteData = a.GetManifestResourceStream(filename);
            var rawData = new byte[spriteData.Length];
            spriteData.Read(rawData, 0, rawData.Length);
            var tex = new Texture2D(1, 1);
            ImageConversion.LoadImage(tex, rawData);
            tex.filterMode = FilterMode.Bilinear;
            return tex;
        }
        public static Sprite CreateSprite(Texture2D texture) => Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);

        private static void CreateArchipelagoMenu() 
        {
            GameObject ArchipelagoMenu = new GameObject("Archipelago Menu");
            ArchipelagoMenu.layer = LayerMask.NameToLayer("MenuFG");
            ArchipelagoMenu.transform.SetParent(GameObject.Find("Menus").transform);
            ArchipelagoMenu.transform.position = new Vector3(-19.2f, 0, 0);
        }

        private static void CreateArchipelagoButton()
        {
            GameObject ArchipelagoButton = new GameObject("Archipelago button");
            ArchipelagoButton.layer = LayerMask.NameToLayer("MenuFG");
            ArchipelagoButton.transform.SetParent(GameObject.Find("Menus").transform.Find("MainMenu"));
            ArchipelagoButton.AddComponent<SpriteRenderer>().sprite = CreateSprite(LoadImage("Archipelago.DuckLife.Resources.ArchipelagoButton.png"));
            ArchipelagoButton.transform.position = new Vector3(3.83f, -4.3672f, 0f);
            ArchipelagoButton.transform.localScale = new Vector3(0.01f, 0.01f, 0);
            ArchipelagoButton.AddComponent<BoxCollider2D>();
            var FlyIn = ArchipelagoButton.AddComponent<FlyIn>();
            ArchipelagoButton.AddComponent<ArchipelagoBtn>();
            ArchipelagoButton.AddComponent<HandCursorOnHover>();

            FlyIn.deviation = 2;
            FlyIn.initialDelay = 180;
            FlyIn.movement = FlyIn.directions.yOnly;
            FlyIn.movtype = FlyIn.movetype.bounceOut;
            FlyIn.startCoordinates = new Vector2(2.05f, -6.3f);
            FlyIn.time = 95;
        }

    }
}
