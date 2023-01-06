using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=_nRzoTzeyxU&list=WL&index=22&t=4s
[System.Serializable]
public class Dialogue
{
    public string _name;
    [TextArea(3,10)]
    public string[] _sentences;
}
