using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Euro;
using BorjesLIA.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BorjesLIA.ViewModel
{
    public class EuroViewModel
    {
        public ChartType ChartType { get; set; }
        public IEnumerable<Settings> settings { get; set; }
        public EuroExchangeModel AddEuro { get; set; }
        public IEnumerable<EuroExchangeModel> newEuroList { get; set; }
        public string Year { get; set; }
        public string ChartName { get; set; }
        public IEnumerable<int> SelectedFeatures { get; set; }
        public IEnumerable<SelectListItem> Features { get; set; }
        public int ID { get; set; }

        public Task<List<EuroExchangeModel>> GetData()
        {
            ChartName = "Eurokurs";
            using (var db = new ApplicationDbContext())
            {

                if (db.EuroExchangeModels == null)
                {
                    return GetData();
                }

                else if (db.Settings.Where(x => x.Name == ChartName).Select(x => x.Year).FirstOrDefault() == "Alla")
                {
                    var lAllEuro = db.EuroExchangeModels.ToList();
                    return Task.Run(() => lAllEuro);
                }

                else
                {
                    Year = db.Settings.ToList().Where(x => x.Name == this.ChartName).OrderByDescending(x => x.Year).Select(x => x.Year).FirstOrDefault();
                    var lEuro = db.EuroExchangeModels.Where(x => x.Date.Year.ToString() == Year).OrderBy(x => x.Date).ToList();
                    return Task.Run(() => lEuro);
                }
            }
        }


        public string Title { get; set; }
        public string Subtitle { get; set; }
        public GoogleVisualizationDataTable DataTable { get; set; }
        public EuroViewModel()
        {
            using (var db = new ApplicationDbContext())
            {
                Title = "Euroindex";
                Subtitle = "År";
                DataTable = ConstrucDataTabel(db.EuroExchangeModels.ToList().OrderBy(x => x.Date).ToArray());
            }
        }
        public GoogleVisualizationDataTable ConstrucDataTabel(EuroExchangeModel[] data)
        {
            ChartName = "Eurokurs";
            var dataTable = new GoogleVisualizationDataTable();
            var date = data.Select(x => x.Date.Month).Distinct().OrderBy(x => x);
            var years = data.Select(x => x.Year).Distinct().OrderBy(x => x);
            dataTable.AddColumn("Month", "string");
            using (var db = new ApplicationDbContext())
            {
                Year = db.Settings.ToList().Where(x => x.Name == this.ChartName).OrderByDescending(x => x.Year).Select(x => x.Year).FirstOrDefault();
                if (Year != null)
                {
                    string[] values = Year.Split(',').Select(sValue => sValue.Trim()).ToArray();
                    foreach (string yItem in values)
                    {
                        dataTable.AddColumn(yItem.ToString(), "number");
                    }
                    foreach (var d in date)
                    {
                        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                        var strMonthName = mfi.GetMonthName(d).ToString();
                        var val = new List<object>(new[] { strMonthName });
                        foreach (var year in values)
                        {
                            var result = data
                                .Where(x => x.Date.Month == d && x.Year == year)
                                .Select(x => x.euroValue)
                                .SingleOrDefault();
                            val.Add(result);
                        }
                        dataTable.AddRow(val);
                    }
                    return dataTable;
                }
                else
                {
                    return dataTable;
                }
            }
        }
    }
}
