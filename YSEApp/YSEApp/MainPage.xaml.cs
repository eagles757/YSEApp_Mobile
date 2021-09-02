using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Net.Http;
using Xamarin.Forms;
using ZXing;

namespace YSEApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        YSEClass gl = new YSEClass();
        // string connstr = "server=a2nlmysql39plsk.secureserver.net;uid=Eagles757;pwd=Joker321@@;database=ph14846596325_EagleSales;persistsecurityinfo=True;SslMode=none";
        EagleItem i = new EagleItem();
        HttpClient client;

        public MainPage()
        {
           // ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            InitializeComponent();
        }
        public void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Scanned result", result.Text, "OK");



                //string qry = "select * from ph14846596325_EagleSales.EagleSales_Item as a left join ph14846596325_EagleSales.EagleSales_ItemGroup as b on b.ItemNumber = a.ItemNumber left join(select ItemNumber, Indv_Cost from ph14846596325_EagleSales.EagleSales_Transact order by t_ID desc) as c on c.ItemNumber = a.ItemNumber left join(select* from  ph14846596325_EagleSales.EagleSales_Pricing where enddate = '1/1/1900') as d on d.ItemNumber = a.ItemNumber left join(select* from  ph14846596325_EagleSales.EagleSales_CurrentOnHand) as e on e.ItemNumber = a.ItemNumber where a.ItemNumber = " + result.Text + "'";

                //DataSet ds = gl.Mysql_run(qry, connstr);

                //foreach (DataRow a in ds.Tables[0].Rows)
                //{
                //    i.ItemNumber = a["a.ItemNumber"].ToString();
                //    i.SKU = a["SKU"].ToString();
                //    i.Name = a["Name"].ToString();
                //    i.Supplier = a["Supplier"].ToString();
                //    i.ImageUrl = a["ImageUrl"].ToString();
                //    i.ProductUrl = a["ProductUrl"].ToString();
                //    i.PrimaryCat = a["MainGroupID"].ToString();
                //    i.SubCat = a["SubGroupID"].ToString();
                //    i.Cost = a["Indv_Cost"].ToString();
                //    i.Retail = a["Price"].ToString();
                //    i.NumberInStock = a["NumberInStock"].ToString();
                //    break;
                //}
                HttpResponseMessage response = await client.GetAsync("https://www.yseusa.com//WebAPI/YSEA.php?YSEKey=YSE1234YSE&ItemNum=" + result.Text);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    i = JsonConvert.DeserializeObject<EagleItem>(content);
                }


                string results_t = "Item Number: " + i.ItemNumber + " - " + i.Name + " [" + i.SKU + "]. Retail: " + i.Retail + ".  Cost: " + i.Cost + ". Number in stock: " + i.NumberInStock;



                await DisplayAlert("Scanned result",  results_t, "OK");


            });
        }
        
    }

}
