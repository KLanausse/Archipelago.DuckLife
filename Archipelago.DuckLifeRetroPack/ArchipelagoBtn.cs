using System;
using Wix;
using UnityEngine;

//-19.2

// Token: 0x020000DB RID: 219
public class ArchipelagoBtn : MonoBehaviour
{
    public int delay = 280;

    public GameObject Menus = GameObject.Find("Menus");

    private void Update()
    {
        this.delay--;

        //Taken from the MenuSwitch Class Update Method
        if (this.i < this.time)
        {
            this.i += Time.deltaTime * 60f;
            this.MoveUsingFunction(this.realbounceout(this.i / this.time, this.deviation));
        }
    }

    private void OnMouseDown()
    {
        if (this.delay < 0)
        {
            //Transition.ToScene("Menu3");
            //if (G.lite)
            //{
            //    this.menuObject.MoveToPromo();
            //    return;
            //}
            //this.menuObject.MoveToChallenges();
            MoveToArchipelago();
        }
    }

    public void MoveToArchipelago()
    {
        this.startCoordinates = new Vector2(0f, 0f);
        this.tar = new Vector2(-19.2f, 0f); //-10.8f
        this.dist.x = this.tar.x - this.startCoordinates.x;
        this.dist.y = this.tar.y - this.startCoordinates.y;
        this.i = 0f;
    }

    public void MoveToMenuFromArchipelago()
    {
        this.startCoordinates = new Vector2(-19.2f, 0f);
        this.tar = new Vector2(0f, 0f); //-10.8f
        this.dist.x = this.tar.x - this.startCoordinates.x;
        this.dist.y = this.tar.y - this.startCoordinates.y;
        this.i = 0f;
    }

    //Taken from the MenuSwitch Class
    private void MoveUsingFunction(float f)
    {
        Menus.transform.SetPosX(this.startCoordinates.x + this.dist.x * f);
        Menus.transform.SetPosY(this.startCoordinates.y + this.dist.y * f);
    }

    //Taken from the MenuSwitch Class
    private float realbounceout(float x, float bounceamount)
    {
        float num = 3.1415927f / bounceamount;
        return (Mathf.Pow(num * x - num, 3f) + Mathf.Pow(num * x - num, 2f)) / (Mathf.Pow(num, 3f) - Mathf.Pow(num, 2f)) + 1f;
    }

    // Token: 0x0400046D RID: 1133
    private Vector2 startCoordinates;

    // Token: 0x0400046E RID: 1134
    public float time = 70;
    //71 if going diangnal

    // Token: 0x0400046F RID: 1135
    private int initialDelay;

    // Token: 0x04000470 RID: 1136
    public float deviation = 2f;

    // Token: 0x04000471 RID: 1137
    private Vector2 tar;

    // Token: 0x04000472 RID: 1138
    private Vector2 dist;

    // Token: 0x04000473 RID: 1139
    private float i = 0;


}
