using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;

namespace LightWeightRulesEngine
{
    public partial class BenefitsInLimits:Handler
    {
        public BenefitsInLimits( int BenefitsInLimitsId, 
                            int FormId,
                            int BenefitMin,
                            int BenefitMax,
                            int RelationshipId)
        {
            BenefitInLimitsIdValue=BenefitsInLimitsId;
            FormIdValue=FormId;
            BenefitMinValue=BenefitMin;
            BenefitMaxValue=BenefitMax;
            RelationshipIdValue=RelationshipId;
        }
        
        
        public  override int HandleRequest(ref int exitPath, ref XElement Application)
        {

            var vBenefit = Application.Element("Benefit").Value;
            if (((String)vBenefit) == String.Empty)
            {
                exitPath = 3;
                ErrorLogging el = new ErrorLogging();
                var vAppId=Application.Attribute("AppId").Value;
                el.AppId = Convert.ToInt32(vAppId);
                var vFormId = Application.Element("FormId").Value;
                el.FormId = Convert.ToInt32(vFormId);
                el.ErrorDescription = "The empty value of Benefit caused a catatstrophic failure";
                ErrorLogging.Insert_ErrorLogging(el);
                return exitPath;
            }

            String Benefit = ((String)Application.Element("Benefit"));
            float iBenefit = Convert.ToSingle(Benefit);
            
            int formId=Convert.ToInt32(((String)Application.Element("FormId")));
            int relationshipid =Convert.ToInt32(((String)Application.Element("Relationship")));
            BenefitsInLimits benefitsinlimits = (from BenefitsInLimits bil in lstBenefitsInLimits
                                      where bil.FormId == formId && bil.RelationshipId == relationshipid
                                      select bil).Single();
            
            if(iBenefit>=benefitsinlimits.BenefitMin && iBenefit<=benefitsinlimits.BenefitMax)
            {
                exitPath=1;
            }
            else if((iBenefit < benefitsinlimits.BenefitMin) || (iBenefit > benefitsinlimits.BenefitMax))
            {
                exitPath=2;
                ErrorLogging el = new ErrorLogging();
                el.AppId = (int)Application.Attribute("AppId");
                el.FormId = (int)Application.Element("FormId");
                el.ErrorDescription = "The value of Benefit was outside the approved range";
                ErrorLogging.Insert_ErrorLogging(el);
            }
            return exitPath;
        }     

        private List<BenefitsInLimits> _lstBenefitsInLimits = new List<BenefitsInLimits>();
        public List<BenefitsInLimits> lstBenefitsInLimits
        {
            get 
            {
                DataTable dt = new DataTable();
                if (_lstBenefitsInLimits.Count == 0)
                    {
                    string SQL = "pr_App_BenefitInLimits_SELECTALL";
                    DataAccess.ExecuteSQL(SQL, Handler.ConnectionString, dt);
                    DataTableToList(ref _lstBenefitsInLimits, dt);
                    }
                return _lstBenefitsInLimits; 
            }
            set{_lstBenefitsInLimits = value;}
        }
        private void DataTableToList( ref List<BenefitsInLimits> lst, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new BenefitsInLimits(
                    Convert.ToInt32(dr[0]),
                    Convert.ToInt32(dr[1]),
                    Convert.ToInt32(dr[2]),
                    Convert.ToInt32(dr[3]),
                    Convert.ToInt32(dr[4])));
            }
        }
        
    }
}
