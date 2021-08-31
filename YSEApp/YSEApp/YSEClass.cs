using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace YSEApp
{
    class YSEClass
    {
        public string Mysql_update(string query, string constring)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(constring))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            string return1 = command.ExecuteNonQuery().ToString();
                            connection.Close();
                            return return1;
                        }
                        catch (Exception ex)
                        {
                            // Response.Write("<BR> ERROR 35: " & ex.ToString)
                            return "ERROR|" + ex.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet Mysql_run(string query, string constring)
        {
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            MySqlDataAdapter da;
            try
            {
                var connStr = constring;


                using (MySqlConnection connection = new MySqlConnection(connStr))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        try
                        {
                            connection.Open();
                            command.CommandTimeout = 300;
                            int iCount = 0;

                            da = new MySqlDataAdapter(command);
                            da.Fill(ds);
                            connection.Close();
                            return ds;
                        }
                        catch (Exception ex)
                        {
                            // Response.Write("<BR> Error 35 " & ex.ToString)
                            // log("CALL " & query & " ------ SQL_RUL DLL ERROR: " & ex.ToString, True, "Y", "C:\FEED\logs\ALG_DLL_ERROR_" & DateTime.Now.ToString("MM-dd-yyyy") & ".txt")
                            return null/* TODO Change to default(_) if this is not a reference type */;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log("CALL " & query & " ------ SQL_RUL DLL ERROR: " & ex.ToString, True, "Y", "C:\FEED\logs\ALG_DLL_ERROR_" & DateTime.Now.ToString("MM-dd-yyyy") & ".txt")
                DataSet ds1 = new DataSet();
                ds1.Tables[0].Columns.Add("ERROR");
                ds1.Tables[0].Rows.Add("ERROR " + ex.ToString());
                return ds1;
            }
        }
    }
    public class WLItem
    {
        string ItemNumber  { get; set;}
        string Qty { get; set; }
        string Price { get; set; }
        string ItemName { get; set; }
    }

    public class EagleItem
    {


        public string ItemNumber { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string ImageUrl { get; set; }
        public string ProductUrl { get; set; }
        public string PrimaryCat { get; set; }
        public string SubCat { get; set; }
        public string Cost { get; set; }
        public string Retail { get; set; }
        public string NumberInStock { get; set; }
        YSEClass gl = new YSEClass();

        string saveItem(String connstr)
        {


            string rtn = "";
            string qry = "";
            if(ItemNumber == "") {
                qry = "insert into ph14846596325_EagleSales.EagleSales_Item ( SKU, Name, Supplier, ImageUrl, ProductUrl,Cost) values ('" + SKU + "', '" + Name + "', '" + Supplier + "', '" + ImageUrl + "', '" + ProductUrl + "', '" + Cost + "'); SELECT LAST_INSERT_ID(); ";

                DataSet ds = gl.Mysql_run(qry, connstr);
                
                foreach(DataRow a in ds.Tables[0].Rows)
                {
                    ItemNumber = a["LAST_INSERT_ID"].ToString();
                }
                

                if(ItemNumber != ""){
                    qry = "insert into ph14846596325_EagleSales.EagleSales_ItemGroup (ItemNumber, MainGroupID, SubGroupID, StartDate, EndDate) values('" + ItemNumber + "', '" + PrimaryCat + "', '" + SubCat + "', '" + DateTime.Now.Date.ToString("yyyy/MM/dd") + "', '1-1-1900'); ";
                    ds = gl.Mysql_run(qry, connstr);
                    foreach(DataRow a  in ds.Tables[0].Rows){
                                   rtn = ItemNumber;
                    }

                }
            }
        
            else {
                //'update 
            }

            return rtn;

        }
    }



}
