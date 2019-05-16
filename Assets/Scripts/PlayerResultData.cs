using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

//refer from https://home.gamer.com.tw/creationDetail.php?sn=3562034
[StructLayout(LayoutKind.Sequential)]
public class PlayerResultData {

	public string userName;
    public int spendTime;

    public void playerResultData(string userName, int spendTime) {
        this.userName = userName;
        this.spendTime = spendTime;
    }
    public string getUserName() {
        return userName;
    }
    public int getSpendTime() {
        return spendTime;
    }
}
