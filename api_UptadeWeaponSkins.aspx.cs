using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class api_UptadeWeaponSkins : WOApiWebPage
{
    string CustomerID = null;

    void UpdateWeaponSkins()
    {
        int WeaponitemID = 0;
        int Skin = 0;
        int Selected = 0;

        try
        {
            WeaponitemID = web.GetInt("WeaponItemID");
            Skin = web.GetInt("Skin");
            Selected = web.GetInt("Selected");
        }
        catch { }

        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_SetSkinsBought";
        sqcmd.Parameters.AddWithValue("@in_CustomerID", CustomerID);
        sqcmd.Parameters.AddWithValue("@in_WeaponItemID", WeaponitemID);
        sqcmd.Parameters.AddWithValue("@in_Skins", Skin);
        sqcmd.Parameters.AddWithValue("@in_Selected", Selected);

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

        UpdateWeaponSkins();

        Response.Write("WO_0");
    }
}
