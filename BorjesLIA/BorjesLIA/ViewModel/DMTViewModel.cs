using BorjesLIA.Models;
using BorjesLIA.Models.Charts;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BorjesLIA.ViewModel
{
    public class DMTViewModel
    {
        public IEnumerable<Settings> settings { get; set; }
        public ChartType ChartType { get; set; }
        public Quarters Quarters { get; set; }
        public SortType SortType { get; set; }
        public DtmModel AddDtm { get; set; }
        public IEnumerable<DtmModel> newDTMList { get; set; }
        public string Year { get; set; }
        public string ChartName { get; set; }
        public Task<List<DtmModel>> GetData()
        {
            ChartName = "Drivmedelstillägg";
            using (var db = new ApplicationDbContext())
            {
                if (db.DtmModels == null)
                {
                    return GetData();
                }
                else if (db.Settings.Where(x => x.Name == ChartName).Select(x => x.Year).FirstOrDefault() == "Alla")
                {
                    var lAllDtm = db.DtmModels.ToList();
                    return Task.Run(() => lAllDtm);
                }
                else
                {
                    Year = db.Settings.ToList().Where(x => x.Name == this.ChartName).Select(x => x.Year).FirstOrDefault();
                    var lDtm = db.DtmModels.Where(x => x.Date.Year.ToString() == Year).OrderBy(x => x.Date).ToList();
                    return Task.Run(() => lDtm);
                }
            }
        }

        public GoogleVisualizationDataTable DataTable { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public DMTViewModel()
        {
            using (var db = new ApplicationDbContext())
            {
                Title = "Drivmedelstillägg";
                Subtitle = "År";
                DataTable = ConstrucDataTabel(db.DtmModels.ToList().OrderBy(x => x.Date).ToArray());
            }
        }

        public GoogleVisualizationDataTable ConstrucDataTabel(DtmModel[] data)
        {
            ChartName = "Drivmedelstillägg";
            var dataTable = new GoogleVisualizationDataTable();
            var month = data.Select(x => x.Date.Month).Distinct().OrderBy(x => x);
            var years = data.Select(x => x.Year).Distinct().OrderBy(x => x);
            dataTable.AddColumn("Month", "string");
            using (var db = new ApplicationDbContext())
            {
                Year = db.Settings.ToList().Where(x => x.Name == ChartName).OrderByDescending(x => x.Year).Select(x => x.Year).FirstOrDefault();
                string[] values = Year.Split(',').Select(sValue => sValue.Trim()).ToArray();
                foreach (string yItem in values)
                {
                    dataTable.AddColumn(yItem.ToString(), "number");
                }

                foreach (var m in month)
                {
                    System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                        var strMonthName = mfi.GetMonthName(m).ToString();
                    var val = new List<object>(new[] { strMonthName });
                    foreach (var year in values)
                    {                       
                        var result = data
                            .Where(x => x.Date.Month == m && x.Year == year)
                            .Select(x => x.DieselDTMValue)
                            .SingleOrDefault();
                        val.Add(result);
                    }
                    dataTable.AddRow(val);
                }                
            }
            return dataTable;
        }
    }
}