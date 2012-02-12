
using System;
using System.Data;
using System.Collections.Generic;

namespace LightWeightRulesEngine
{
    public partial class ProductForms
    {

        public ProductForms(Int32 FormId, String FormName, Boolean Active)
        {
            FormIdValue = FormId;
            FormNameValue = FormName;
            ActiveValue = Active;
        }

        public static List<ProductForms> SelectAll_ProductForms(string ConnectionString)
        {
            List<ProductForms> lstProductForms = new List<ProductForms>();
            DataTable dt = new DataTable();
            String SQL = "pr_App_ProductForms_SELECTALL";
            DataAccess.ExecuteSQL(SQL, ConnectionString, dt);
            foreach (DataRow dr in dt.Rows) lstProductForms.Add(
                  new ProductForms(
                      Convert.ToInt32(dr["FormId"]),
                      dr["FormName"].ToString(),
                      Convert.ToBoolean(dr["Active"])));

            return lstProductForms;
        }

    }
}
