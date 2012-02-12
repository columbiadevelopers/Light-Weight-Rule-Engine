using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;

namespace LightWeightRulesEngine
{
    public partial class AgeInLimits : Handler
    {
        public AgeInLimits( int AgeInLimitsId, 
                            int FormId,
                            int AgeMin,
                            int AgeMax,
                            int RelationshipId)
        {
            AgeInLimitsIdValue=AgeInLimitsId;
            FormIdValue=FormId;
            AgeMinValue=AgeMin;
            AgeMaxValue=AgeMax;
            RelationshipIdValue=RelationshipId;
        }
        
        
        public  override int HandleRequest(ref int exitPath, ref XElement Application)
        {
            
            DateTime dtToday = DateTime.Today;
            if (((String)Application.Element("Birthdate")) == String.Empty) return 3;
            String Birthdate = ((String)Application.Element("Birthdate"));
            DateTime dtBirth=DateTime.Parse(Birthdate);
            
            int Age = dtToday.Year-dtBirth.Year;
            int formId=Convert.ToInt32(((String)Application.Element("FormId")));
            int relationshipid =Convert.ToInt32(((String)Application.Element("Relationship")));
            AgeInLimits ageinlimits = (from AgeInLimits ail in lstAgeInLimits
                                      where ail.FormId == formId && ail.RelationshipId == relationshipid
                                      select ail).Single();
            
            //List<AgeInLimits> AIL = new List<AgeInLimits>();
            //foreach (AgeInLimits a in ageinlimits) AIL.Add(a);
            //if((Age>=AIL[0].AgeMin)&& (Age<=AIL[0].AgeMax)) 
            if(Age>=ageinlimits.AgeMin && Age<=ageinlimits.AgeMax)
            {
                exitPath=1;
            }
            else
            {
                exitPath=2;
                ErrorLogging el = new ErrorLogging();
                el.AppId = (int)Application.Attribute("AppId");
                el.FormId = (int)Application.Element("FormId");
                el.ErrorDescription = "The Age is outside of the allowable range.";
                ErrorLogging.Insert_ErrorLogging(el);
            }
            return exitPath;
        }     

        private List<AgeInLimits> _lstAgeInLimits = new List<AgeInLimits>();
        public List<AgeInLimits> lstAgeInLimits
        {
            get 
            {
                DataTable dt = new DataTable();
                if (_lstAgeInLimits.Count == 0)
                    {
                    string SQL = "pr_App_AgeInLimits_SELECTALL";
                    DataAccess.ExecuteSQL(SQL, Handler.ConnectionString, dt);
                    DataTableToList(ref _lstAgeInLimits, dt);
                    }
                return _lstAgeInLimits; 
            }
            set{_lstAgeInLimits = value;}
        }
        private void DataTableToList( ref List<AgeInLimits> lst, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new AgeInLimits(
                    Convert.ToInt32(dr[0]),
                    Convert.ToInt32(dr[1]),
                    Convert.ToInt32(dr[2]),
                    Convert.ToInt32(dr[3]),
                    Convert.ToInt32(dr[4])));
            }
        }
        
    }
}
