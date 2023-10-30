using System;
using System.Reflection;
using UnityEngine;
using MelonLoader;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.DuckLifeRetroPack;

namespace Archipelago.DuckLife
{
    public class ArchipelagoMod : MelonMod
    {
        public static ArchipelagoMod instance;

        public int currentGame = 0;
        //0 = Menu
        //1 = DuckLife 1
        //2 = DuckLife 2
        //3 = DuckLife 3

        private Sprite buttonSprite;

        //Methods
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
            instance.LoggerInstance.Msg($"Login Result: {result}");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            instance.LoggerInstance.Msg($"Scene {sceneName} with build index {buildIndex} has been loaded!");
            if ( sceneName == "RetroMenu" )
            {
                //DO NOT DO THIS. EVER
                buttonSprite = GameObject.Find("Menus").transform.Find("ChallengesMenu").transform.Find("BackBtn").GetComponent<SpriteRenderer>().sprite;

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

        private void CreateArchipelagoMenu() 
        {
            GameObject ArchipelagoMenu = new GameObject("ArchipelagoMenu");
            ArchipelagoMenu.layer = LayerMask.NameToLayer("MenuFG");
            ArchipelagoMenu.transform.SetParent(GameObject.Find("Menus").transform);
            ArchipelagoMenu.transform.position = new Vector3(-19.2f, 0, 0);
            ArchipelagoMenu.AddComponent<ArchipelagoMenu>();

            //Back Button
            GameObject ArchipelagoMenuBackButton = CreateSpriteObject("ArchipelagoBackBtn", buttonSprite, new Vector3(26.61f, -4.4654f, 0));
            ArchipelagoMenuBackButton.layer = LayerMask.NameToLayer("MenuBG");
            ArchipelagoMenuBackButton.transform.SetParent(ArchipelagoMenu.transform);
            ArchipelagoMenuBackButton.AddComponent<ArchipelagoBackBtn>();
            ArchipelagoMenuBackButton.AddComponent<HandCursorOnHover>();
            GameObject ArchipelagoMenuBackButtonText = CreateTextObject("Back", "BACK");
            ArchipelagoMenuBackButtonText.transform.SetParent(ArchipelagoMenuBackButton.transform);
            ArchipelagoMenuBackButtonText.transform.position = new Vector3(26.61f, -4.4654f, 0);
            ArchipelagoMenuBackButtonText.layer = LayerMask.NameToLayer("MenuFG");
            



        }

        private static void CreateArchipelagoButton()
        {
            GameObject ArchipelagoButton = CreateSpriteObject("Archipelago button", "Archipelago.DuckLifeRetroPack.Resources.ArchipelagoButton.png", new Vector3(5.25f, -4.3672f, 0f), new Vector3(0.01f, 0.01f, 0));
            ArchipelagoButton.layer = LayerMask.NameToLayer("MenuFG");
            ArchipelagoButton.transform.SetParent(GameObject.Find("Menus").transform.Find("MainMenu"));
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

        //CreateSpriteObject
        private static GameObject CreateSpriteObject(String name, String imagePath, Vector3 position, Vector3 scale)
        {
            var spriteObject = new GameObject(name);
            spriteObject.AddComponent<SpriteRenderer>().sprite = CreateSprite(LoadImage(imagePath));
            spriteObject.AddComponent<BoxCollider2D>();
            spriteObject.transform.position = position;
            spriteObject.transform.localScale = scale;
            return spriteObject;
        }

        private static GameObject CreateSpriteObject(String name, String imagePath, Vector3 position)
        {
            var spriteObject = new GameObject(name);
            spriteObject.AddComponent<SpriteRenderer>().sprite = CreateSprite(LoadImage(imagePath));
            spriteObject.AddComponent<BoxCollider2D>();
            spriteObject.transform.position = position;
            return spriteObject;
        }

        private static GameObject CreateSpriteObject(String name, Sprite sprite, Vector3 position, Vector3 scale)
        {
            var spriteObject = new GameObject(name);
            spriteObject.AddComponent<SpriteRenderer>().sprite = sprite;
            spriteObject.AddComponent<BoxCollider2D>();
            spriteObject.transform.position = position;
            spriteObject.transform.localScale = scale;
            return spriteObject;
        }

        private static GameObject CreateSpriteObject(String name, Sprite sprite, Vector3 position)
        {
            var spriteObject = new GameObject(name);
            spriteObject.AddComponent<SpriteRenderer>().sprite = sprite;
            spriteObject.AddComponent<BoxCollider2D>();
            spriteObject.transform.position = position;
            return spriteObject;
        }

        //CreateTextObject
        private static GameObject CreateTextObject(String name, String text)
        {
            var textObject = new GameObject(name);
            textObject.AddComponent<MeshFilter>();
            textObject.AddComponent<MeshRenderer>();
            var DynamTxt = textObject.AddComponent<DynamicText>();
            DynamTxt.SetText(text);
            //DynamTxt.font = new Font("Gotham-Ultra");
            DynamTxt.anchor = DynamicTextAnchor.MiddleCenter;
            DynamTxt.enabled = true;
            return textObject;
        }

    }
}
