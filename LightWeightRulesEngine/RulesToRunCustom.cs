using System;
using System.Data;
using System.Collections.Generic;

namespace LightWeightRulesEngine
{
    public partial class RulesToRun
    {

        public RulesToRun(Int32 RuleToRunId, Int32 FormId, Int32 RuleId, Int32 ActionId, Int32 SuccessorId, Boolean Active)
        {

            RuleToRunIdValue = RuleToRunId;
            FormIdValue = FormId;
            RuleIdValue = RuleId;
            ActionIdValue = ActionId;
            SuccessorIdValue = SuccessorId;
            ActiveValue = Active;
        }

        public static List<RulesToRun> SelectAll_RulesToRun(string ConnectionString)
        {
            List<RulesToRun> lstRulesToRun = new List<RulesToRun>();
            DataTable dt = new DataTable();
            String SQL = "pr_App_RulesToRun_SELECTALL";
            DataAccess.ExecuteSQL(SQL, ConnectionString, dt);
            foreach (DataRow dr in dt.Rows) lstRulesToRun.Add(
                  new RulesToRun(
                      Convert.ToInt32(dr["RuleToRunId"]),
                      Convert.ToInt32(dr["FormId"]),
                      Convert.ToInt32(dr["RuleId"]),
                      Convert.ToInt32(dr["ActionId"]),
                      Convert.ToInt32(dr["SuccessorId"]),
                      Convert.ToBoolean(dr["Active"])));

            return lstRulesToRun;
        }
    }
}