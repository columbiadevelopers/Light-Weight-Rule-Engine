///
/// This class is generated from an automated application and is created based on
/// Database fields and their property types.
/// 
/// Modifying this file may result in problems interacting with your database table.
/// We recommend you do not modify this file but regenerate it from the database as needed.
/// 
/// This is a partial class so that you can create a second partial class that will contain all your
/// overloaded and custom code.  This second partial class will not be modified by the class generator.
/// 

using System;
using System.Diagnostics;
namespace LightWeightRulesEngine
{
    public partial class BenefitsInLimits : Handler
    {

        public BenefitsInLimits() { }


        private Int32 BenefitInLimitsIdValue;
        private Int32 FormIdValue;
        private Int32 RelationshipIdValue;
        private Single BenefitMaxValue;
        private Single BenefitMinValue;
        private Boolean ActiveValue;

        public Int32 BenefitInLimitsId
        {
            get { return BenefitInLimitsIdValue; }
            set { BenefitInLimitsIdValue = value; }
        }

        public Int32 FormId
        {
            get { return FormIdValue; }
            set { FormIdValue = value; }
        }

        public Int32 RelationshipId
        {
            get { return RelationshipIdValue; }
            set { RelationshipIdValue = value; }
        }

        public Single BenefitMax
        {
            get { return BenefitMaxValue; }
            set { BenefitMaxValue = value; }
        }

        public Single BenefitMin
        {
            get { return BenefitMinValue; }
            set { BenefitMinValue = value; }
        }

        public Boolean Active
        {
            get { return ActiveValue; }
            set { ActiveValue = value; }
        }

    }
}