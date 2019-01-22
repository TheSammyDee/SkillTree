using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill {

    public long id { get; private set; }
	public string name { get; set; }
    public int total { get; set; }
    public Color color { get; set; }
    public List<long> parents { get; set; }

    public Skill(long id, string name, Color color, List<long> parents) {
        this.id = id;
        this.name = name;
        total = 0;
        this.color = color;
        this.parents = parents;
    }
}
