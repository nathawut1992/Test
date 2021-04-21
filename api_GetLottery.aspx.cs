using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class api_GetLottery : WOApiWebPage
{
    void OutLotData(StringBuilder xml)
    {
        xml.Append("<l ");
        xml.Append(xml_attr("i", reader["ID"]));
        xml.Append(xml_attr("t", ToUnixTime(Convert.ToDateTime(reader["BuyTime"]))));
        xml.Append(xml_attr("u", reader["UserID"]));
        xml.Append(xml_attr("b", reader["Bank"]));
        xml.Append(xml_attr("ct", reader["CountTickets"]));
        xml.Append(xml_attr("ch", reader["Chance"]));
        xml.Append("/>");
    }
    void OutWinnersData(StringBuilder xml)
    {
        xml.Append("<w ");
        xml.Append(xml_attr("t2", ToUnixTime(Convert.ToDateTime(reader["Date"]))));
        xml.Append(xml_attr("wu", reader["WinUserID"]));
        xml.Append(xml_attr("j", reader["Jackpot"]));
        xml.Append("/>");
    }

    void GetLottery()
    {
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_GetLottery";

        if (!CallWOApi(sqcmd))
            return;

        StringBuilder xml = new StringBuilder();
        xml.Append("<?xml version=\"1.0\"?>\n");
        xml.Append("<lottery>");

        while (reader.Read())
        {
            OutLotData(xml);
        }

        xml.Append("</lottery>");

        GResponse.Write(xml.ToString());
    }
    void GetWinnerList()
    {
        SqlCommand sqcmd = new SqlCommand();
        sqcmd.CommandType = CommandType.StoredProcedure;
        sqcmd.CommandText = "WZ_GetLotteryWinners";

        if (!CallWOApi(sqcmd))
            return;

        StringBuilder xml = new StringBuilder();
        xml.Append("<?xml version=\"1.0\"?>\n");
        xml.Append("<winners>");

        while (reader.Read())
        {
            OutWinnersData(xml);
        }

        xml.Append("</winners>");

        GResponse.Write(xml.ToString());
    }

    protected override void Execute()
    {
        if (!WoCheckLoginSession())
            return;

        GetLottery();
        GetWinnerList();
    }
}
