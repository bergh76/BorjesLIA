using BorjesLIA.Models;
using BorjesLIA.Models.Diesel;
using BorjesLIA.Models.Euro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BorjesLIA.ViewModel
{
    public class ResultTilesPanelViewModels
    {

        public string actualDiesel { get; set; }
        public string lastDiesel { get; set; }
        public string diffDiesel { get; set; }

        public string actualDTM { get; set; }
        public string lastDTM { get; set; }
        public string diffDTM { get; set; }

        public string actualEuro { get; set; }
        public string lastEuro { get; set; }
        public string diffEuro { get; set; }

        public void returnTilePanelValues()
        {
            string error = "Fel vid datainsamling";
            using (var _db = new ApplicationDbContext())
            {
                List<DieselWeekModel> iDPM = new List<DieselWeekModel>();
                iDPM = _db.DieselPriceWeek.ToList();
                if (iDPM == null || iDPM.GetEnumerator().MoveNext())
                {
                    var getDiesel = iDPM.Select(s => s.DieselWeekValue)
                                        .FirstOrDefault();
                    actualDiesel = getDiesel.ToString();
                    var getLastDiesel = iDPM.OrderByDescending(x => x.Week).Select(x => x.DieselWeekValue).ElementAt(1);
                    lastDiesel = getLastDiesel.ToString();

                    var dieselTemp = Math.Round(Convert.ToDecimal(actualDiesel) - Convert.ToDecimal(lastDiesel), 2);
                    diffDiesel = dieselTemp.ToString();
                }

                else
                {
                    actualDiesel = error;
                    lastDiesel = error;
                    diffDiesel = error;
                }
                List<EuroExchangeModel> iEPM = new List<EuroExchangeModel>();
                iEPM = _db.EuroExchangeModels.ToList();
                if (iEPM == null || iEPM.GetEnumerator().MoveNext())
                {
                    var getEuro = iEPM.Select(s => s.euroValue)
                                        .FirstOrDefault();
                    actualEuro = getEuro.ToString();
                    var getLastEuro = iEPM.OrderByDescending(x => x.Date).Select(x => x.euroValue).ElementAt(1);
                    lastEuro = getLastEuro.ToString();

                    var EuroTemp = Math.Round(Convert.ToDecimal(actualEuro) - Convert.ToDecimal(lastEuro), 2);
                    diffEuro = EuroTemp.ToString();
                }

                else
                {
                    actualEuro = error;
                    lastEuro = error;
                    diffEuro = error;
                }
            }
        }
    }
}