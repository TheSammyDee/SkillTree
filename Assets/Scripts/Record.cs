using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record {

    public DateTime date { get; private set; }
    public int amount { get; private set; }
    public Skill skill { get; private set; }

    public Record(DateTime date, int amount, Skill skill) {
        this.date = date;
        this.amount = amount;
        this.skill = skill;
    }
}
