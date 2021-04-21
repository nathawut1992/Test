using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

//AlexRedd:: BR mode
public partial class api_SrvCharUpdateBR : WOApiWebPage
{
    string CustomerID = null;
    string CharID = null;

    void UpdateCharStatusBR()
    {
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_Char_SRV_SetStatusBR";
        sqcmd.Parameters.AddWithValue("@in_CustomerID", CustomerID);
        sqcmd.Parameters.AddWithValue("@in_CharID", CharID);        
        sqcmd.Parameters.AddWithValue("@in_GameDollars", web.Param("sB"));
        sqcmd.Parameters.AddWithValue("@in_XP", web.Param("s8"));

        // generic trackable stats
        sqcmd.Parameters.AddWithValue("@in_Stat03", web.Param("ts03"));
        
        if (!CallWOApi(sqcmd))
            return;
    }
    

    protected override void Execute()
    {
        // we still need to check login credentials in case of double login from other computer
        if (!WoCheckLoginSession())
            return;

        string skey1 = web.Param("skey1");
        if (skey1 != SERVER_API_KEY)
            throw new ApiExitException("bad key");

        CustomerID = web.CustomerID();
        CharID = web.Param("CharID");

        UpdateCharStatusBR();       

        Response.Write("WO_0");
    }
}
