using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;

namespace LightWeightRulesEngine
{
    public partial class SignedAtState : Handler
    {

        public SignedAtState(Int32 SignedAtStateId, Int32 FormId, String State)
        {

            SignedAtStateIdValue = SignedAtStateId;
            FormIdValue = FormId;
            StateValue = State;
        }

        public override int HandleRequest(ref int exitPath, ref XElement Application)
        {
            var vState = Application.Element("SignedAtState");
            if (((String)vState) == String.Empty)
            {
                exitPath = 3;
                ErrorLogging el = new ErrorLogging();
                var vAppId = Application.Attribute("AppId").Value;
                el.AppId = Convert.ToInt32(vAppId);
                var vFormId = Application.Element("FormId").Value;
                el.FormId = Convert.ToInt32(vFormId);
                el.ErrorDescription = "The value of SignedAtState was Empty";
                ErrorLogging.Insert_ErrorLogging(el);
                return exitPath;
            }
            Boolean TestIsTrue = false;
            string State = ((string)Application.Element("SignedAtState"));
            String FormId = (String)(Application.Element("FormId"));
            foreach (SignedAtState sas in lstSignedAtState)
            {
                if (sas.State == State && sas.FormId == Convert.ToInt32(FormId))
                {
                    TestIsTrue = true;
                }
            }
            if (TestIsTrue)
            {
                exitPath = 1;
                return 1;
            }
            else
            {
                exitPath = 2;
                ErrorLogging el = new ErrorLogging();
                el.AppId = (int)Application.Attribute("AppId");
                el.FormId = (int)Application.Element("FormId");
                el.ErrorDescription = "The value of SignedAtState was not in the SignedAtState list";
                ErrorLogging.Insert_ErrorLogging(el);
                return 2;
            }


        }

        private List<SignedAtState> _lstSignedAtState = new List<SignedAtState>();
        public List<SignedAtState> lstSignedAtState
        {
            get
            {
                DataTable dt = new DataTable();
                if (_lstSignedAtState.Count == 0)
                {
                    string SQL = "pr_App_SignedAtState_SelectAll";
                    DataAccess.ExecuteSQL(SQL, Handler.ConnectionString, dt);
                    DataTableToList(ref _lstSignedAtState, dt);
                }
                return _lstSignedAtState;
            }
            set { _lstSignedAtState = value; }
        }

        private void DataTableToList(ref List<SignedAtState> lst, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new SignedAtState(
                    Convert.ToInt32(dr[0]),
                    Convert.ToInt32(dr[1]),
                    ((String)(dr[2]))));
            }
        }

    }
}