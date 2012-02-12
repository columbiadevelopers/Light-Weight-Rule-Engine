using System;
using System.Collections.Generic;
using System.Data;

namespace LightWeightRulesEngine
{
    public partial class Rule
    {

        public Rule(Int32 RuleID, String RuleDescription, String ClassName, String AssemblyName, Boolean Active)
        {

            RuleIDValue = RuleID;
            RuleDescriptionValue = RuleDescription;
            ClassNameValue = ClassName;
            AssemblyNameValue = AssemblyName;
            ActiveValue = Active;
        }

        public static List<Rule> SelectAll_Rules(string ConnectionString)
        {
            List<Rule> lstRules = new List<Rule>();
            DataTable dt = new DataTable();
            String SQL = "pr_App_Rules_SELECTALL";
            DataAccess.ExecuteSQL(SQL, ConnectionString, dt);
            foreach (DataRow dr in dt.Rows) lstRules.Add(
                  new Rule(
                      Convert.ToInt32(dr["RuleId"]),
                      dr["RuleDescription"].ToString(),
                      dr["ClassName"].ToString(),
                      dr["AssemblyName"].ToString(),
                      Convert.ToBoolean(dr["Active"])));

            return lstRules;
        }
        public static List<Rule> SelectAllActive_Rules(string ConnectionString)
        {
            List<Rule> lstRules = new List<Rule>();
            DataTable dt = new DataTable();
            String SQL = "pr_App_Rules_SELECTALLACTIVE";
            DataAccess.ExecuteSQL(SQL, ConnectionString, dt);
            foreach (DataRow dr in dt.Rows) lstRules.Add(
                  new Rule(
                      Convert.ToInt32(dr["RuleId"]),
                      dr["RuleDescription"].ToString(),
                      dr["ClassName"].ToString(),
                      dr["AssemblyName"].ToString(),
                      Convert.ToBoolean(dr["Active"])));

            return lstRules;
        }
    }
}
