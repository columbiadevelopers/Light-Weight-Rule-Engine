using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LightWeightRulesEngine
{
    public class RulesByFormId
    {
        private int _FormId = 0;
        public int FormId
        {
            get { return _FormId; }
            set { _FormId = value; }
        }
        private int _RuleId = 0;
        public int RuleId
        {
            get { return _RuleId; }
            set { _RuleId = value; }
        }
        private bool _Active = false;
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public RulesByFormId() { }
        public RulesByFormId(int FormId, int RuleId, bool Active)
        {
            _FormId = FormId;
            _RuleId = RuleId;
            _Active = Active;
        }
        public static List<RulesByFormId> SelectAll_RulesByFormId(string ConnectionString)
        {
            List<RulesByFormId> lstRulesByFormId = new List<RulesByFormId>();
            DataTable dt = new DataTable();
            String SQL = "pr_App_RulesByFormId_SELECTALL";
            DataAccess.ExecuteSQL(SQL, ConnectionString, dt);
            foreach (DataRow dr in dt.Rows) lstRulesByFormId.Add(
                  new RulesByFormId(
                      Convert.ToInt32(dr["FormId"]),
                      Convert.ToInt32(dr["RuleId"]),
                      Convert.ToBoolean(dr["Active"])
                      ));

            return lstRulesByFormId;
        }

    }
}
