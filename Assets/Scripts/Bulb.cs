using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb : MonoBehaviour{

    //0=Dark 1=Light
    private int bulbStatus;
    private GameObject bulbGameObj;
    private int positionX;
    private int positionY;
    
    public void setBulbStatus(int bulbStatus)
    {
        this.bulbStatus = bulbStatus;
    }
    public int getBulbStatus()
    {
        return bulbStatus;
    }
    public void setBulbGameObj(GameObject bulbGameObj)
    {
        this.bulbGameObj = bulbGameObj;
    }
    public GameObject getBulbGameObj()
    {
        return bulbGameObj;
    }
    public void setPositionX(int positionX)
    {
        this.positionX = positionX;
    }
    public int getPositionX()
    {
        return positionX;
    }
    public void setPositionY(int positionY)
    {
        this.positionY = positionY;
    }
    public int getPositionY()
    {
        return positionY;
    }
}
