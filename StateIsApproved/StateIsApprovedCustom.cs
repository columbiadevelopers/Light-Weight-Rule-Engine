using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;

namespace LightWeightRulesEngine
{
    public partial class StateIsApproved:Handler
    {
        
        public StateIsApproved( int ApprovedStatesId, 
                            int FormId,
                            String State)
        {
            ApprovedStatesIdValue = ApprovedStatesId;
            FormIdValue=FormId;
            StateValue=State;
        }
        
        
        public  override int HandleRequest(ref int exitPath, ref XElement Application)
        {
           var vState=Application.Element("State");
            if (((String)vState) == String.Empty)
            {
                exitPath = 3;
                ErrorLogging el = new ErrorLogging();
                var vAppId = Application.Attribute("AppId").Value;
                el.AppId = Convert.ToInt32(vAppId);
                var vFormId = Application.Element("FormId").Value;
                el.FormId = Convert.ToInt32(vFormId);
                el.ErrorDescription = "The value of State was Empty";
                ErrorLogging.Insert_ErrorLogging(el);
                return exitPath;
            }
            Boolean TestIsTrue = false;
           string State= ((string)Application.Element("State"));
           String FormId=(String)(Application.Element("FormId"));
           foreach (StateIsApproved sia in lstStateIsApproved)
           {
               if (sia.State==State && sia.FormId==Convert.ToInt32(FormId))
               {
                   TestIsTrue=true;
               }
           }
            if(TestIsTrue)
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
               el.ErrorDescription = "The value of State was not in the approved list";
               ErrorLogging.Insert_ErrorLogging(el);
               return 2;
           }
          
            
        }     

        private List<StateIsApproved> _lstStateIsApproved = new List<StateIsApproved>();
        public List<StateIsApproved> lstStateIsApproved
        {
            get 
            {
                DataTable dt = new DataTable();
                if (_lstStateIsApproved.Count == 0)
                    {
                    string SQL = "pr_App_StateIsApproved_SELECTALL";
                    DataAccess.ExecuteSQL(SQL, Handler.ConnectionString, dt);
                    DataTableToList(ref _lstStateIsApproved, dt);
                    }
                return _lstStateIsApproved; 
            }
            set{_lstStateIsApproved = value;}
        }
        private void DataTableToList( ref List<StateIsApproved> lst, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new StateIsApproved(
                    Convert.ToInt32(dr[0]),
                    Convert.ToInt32(dr[1]),
                    ((String)(dr[2]))));
            }
        }
        
    }
}
