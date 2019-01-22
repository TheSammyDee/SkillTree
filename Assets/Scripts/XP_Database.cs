using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class XP_Database : SQLiterDB {

    private const string DB_NAME = "XP_database";

    private const string TB_SKILLS = "SKILLS";
    private const string TB_RECORDS = "RECORDS";
    private const string TB_SKILLS_PARENTS = "SKILLS_PARENTS";

    private const string COL_ID = "ID";
    private const string COL_NAME = "NAME";
    private const string COL_TOTAL = "TOTAL";
    private const string COL_COLOR_R = "COLOR_R";
    private const string COL_COLOR_G = "COLOR_G";
    private const string COL_COLOR_B = "COLOR_B";
    private const string COL_SKILL_ID = "SKILL_ID";
    private const string COL_PARENT_SKILL_ID = "PARENT_SKILLS_ID";
    private const string COL_DATE = "DATE";
    private const string COL_AMOUNT = "AMOUNT";

    public XP_Database() {
        SQLiteInit(DB_NAME);
        PrepareSkillsTable();
        PrepareRecordsTable();
        PrepareSkillsParentsTable();
    }

    private void PrepareSkillsTable() {
        Dictionary<string, string> skillsColumns = new Dictionary<string, string>();
        skillsColumns.Add(COL_ID, Types.INTEGER + "" + Constraints.UNIQUE);
        skillsColumns.Add(COL_NAME, Types.TEXT);
        //skillsColumns.Add(COL_TOTAL, Types.INTEGER);
        skillsColumns.Add(COL_COLOR_R, Types.REAL);
        skillsColumns.Add(COL_COLOR_G, Types.REAL);
        skillsColumns.Add(COL_COLOR_B, Types.REAL);

        PrepareTable(TB_SKILLS, skillsColumns);
    }

    private void PrepareRecordsTable() {
        Dictionary<string, string> recordsColumns = new Dictionary<string, string>();
        recordsColumns.Add(COL_ID, Types.INTEGER + "" + Constraints.PRIMARY_KEY);
        recordsColumns.Add(COL_DATE, Types.INTEGER);
        recordsColumns.Add(COL_AMOUNT, Types.INTEGER);
        recordsColumns.Add(COL_SKILL_ID, Types.INTEGER);

        PrepareTable(TB_RECORDS, recordsColumns);
    }

    private void PrepareSkillsParentsTable() {
        Dictionary<string, string> skillsParentsColumns = new Dictionary<string, string>();
        skillsParentsColumns.Add(COL_ID, Types.INTEGER + "" + Constraints.PRIMARY_KEY);
        skillsParentsColumns.Add(COL_SKILL_ID, Types.INTEGER);
        skillsParentsColumns.Add(COL_PARENT_SKILL_ID, Types.INTEGER);

        PrepareTable(TB_SKILLS_PARENTS, skillsParentsColumns);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InsertSkill(Skill skill) {
        _sqlString = "INSERT INTO " + TB_SKILLS
            + " ("
            + COL_ID + ","
            + COL_NAME + ","
            //+ COL_TOTAL + ","
            + COL_COLOR_R + ","
            + COL_COLOR_G + ","
            + COL_COLOR_B
            + ") VALUES ("
            + skill.id + ","
            + "'" + skill.name + "',"
           // + skill.total + ","
            + skill.color.r + ","
            + skill.color.g + ","
            + skill.color.b + ");";

        ExecuteNonQuery(_sqlString);

        foreach (int parentSkillID in skill.parents) {
            InsertSkillParent(skill, parentSkillID);
        }
    }

    public void InsertSkillParent(Skill skill, int parentSkillID) {
        _sqlString = "INSERT INTO " + TB_SKILLS_PARENTS
            + " ("
            + COL_SKILL_ID + ","
            + COL_PARENT_SKILL_ID
            + ") VALUES ("
            + skill.id + ","
            + parentSkillID + ");";

        ExecuteNonQuery(_sqlString);
    }

    public void InsertRecord(Record record) {
        _sqlString = "INSERT INTO " + TB_RECORDS
            + " ("
            + COL_DATE + ","
            + COL_AMOUNT + ","
            + COL_SKILL_ID
            + ") VALUES ("
            + record.date.ToBinary() + ","
            + record.amount + ","
            + record.skill.id + ");";

        ExecuteNonQuery(_sqlString);
    }

    public List<Skill> GetAllSkills() {
        List<Skill> skills = new List<Skill>();
        Skill skill;

        _connection.Open();

        _command.CommandText = "SELECT * FROM " + TB_SKILLS;
        _reader = _command.ExecuteReader();

        while (_reader.Read()) {
            skill = new Skill(
                _reader.GetInt64(0),
                _reader.GetString(1),
                new Color(_reader.GetFloat(2), _reader.GetFloat(3), _reader.GetFloat(4)),
                new List<long>());

            skills.Add(skill);
        }

        foreach (Skill sk in skills) {
            sk.parents = GetParentSkills(sk);
        }

        _reader.Close();
        _connection.Close();

        return skills;
    }

    private List<long> GetParentSkills(Skill skill) {
        List<long> parentSkills = new List<long>();

        _command.CommandText = "SELECT " + COL_PARENT_SKILL_ID + " FROM " + TB_SKILLS_PARENTS
            + " WHERE " + COL_SKILL_ID + " = " + skill.id + ";";
        _reader = _command.ExecuteReader();

        while (_reader.Read()) {
            parentSkills.Add(_reader.GetInt64(0));
        }

        return parentSkills;
    }

    public int GetTotalForSkills(List<long> skillIDs) {
        _connection.Open();

        int total = 0;
        int i = 1;
        int listLength = skillIDs.Count;
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;

        sb.Append("SELECT SUM(" + COL_AMOUNT
            + ") FROM " + TB_RECORDS
            + " WHERE ");
        foreach (long id in skillIDs) {
            sb.Append(COL_SKILL_ID + "=" + id);
            if (i == listLength)
                sb.Append(";");
            else
                sb.Append(" OR ");
            i++;
        }

        _command.CommandText = sb.ToString();
        _reader = _command.ExecuteReader();

        if (_reader.Read()) {
            total = _reader.GetInt32(0);
        }

        _reader.Close();
        _connection.Close();

        return total;
    }

    private string ColorToString(Color color) {
        string r = ((int)Mathf.Lerp(0, 255, color.r)).ToString();
        string g = ((int)Mathf.Lerp(0, 255, color.g)).ToString();
        string b = ((int)Mathf.Lerp(0, 255, color.b)).ToString();
        return r + g + b;
    }

    private Color StringToColor(string colorString) {
        int r = (int)Mathf.InverseLerp(0, 225, Int32.Parse(colorString.Substring(0, 2)));
        int g = (int)Mathf.InverseLerp(0, 225, Int32.Parse(colorString.Substring(2, 2)));
        int b = (int)Mathf.InverseLerp(0, 225, Int32.Parse(colorString.Substring(4, 2)));
        return new Color(r, g, b);
    }
}
