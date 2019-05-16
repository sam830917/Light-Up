using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public class Settings {

	public float sound;
    public string language;

    public void settings(float sound, string language)
    {
        this.sound = sound;
        this.language = language;
    }
    public float getSound()
    {
        return sound;
    }
    public string getLanguage()
    {
        return language;
    }
}
