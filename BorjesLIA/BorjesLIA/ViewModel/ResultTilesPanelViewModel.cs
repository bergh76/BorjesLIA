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

        public string actDieselWeek { get; set; }
        public string lastDieselWeek { get; set; }
        public string diffDieselWeek { get; set; }

        public string actDieselQuarter { get; set; }
        public string lastDieselQuarter { get; set; }
        public string diffDieselQuarter { get; set; }

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
                    actDieselWeek = getDiesel.ToString();
                    var getLastDiesel = iDPM.OrderByDescending(x => x.Week).Select(x => x.DieselWeekValue).ElementAt(1);
                    lastDieselWeek = getLastDiesel.ToString();

                    var dWTemp = Math.Round(Convert.ToDecimal(actDieselWeek) - Convert.ToDecimal(lastDieselWeek), 2);
                    diffDieselWeek = dWTemp.ToString();
                }

                else
                {
                    actDieselWeek = error;
                    lastDieselWeek = error;
                    diffDieselWeek = error;
                }

                List<DieselQuarterPriceModel> iDPQ = new List<DieselQuarterPriceModel>();
                iDPQ = _db.DieselPriceQuarter.ToList();
                if (iDPQ == null || iDPQ.GetEnumerator().MoveNext())
                {
                    var getDieselQ = iDPQ.Select(s => s.DieselQuarterValue)
                                        .FirstOrDefault();
                    actDieselQuarter = getDieselQ.ToString();
                    var getLastQDiesel = iDPQ.OrderByDescending(x => x.DieselQuarterValue).Select(x => x.DieselQuarterValue).ElementAt(1);
                    lastDieselQuarter = getLastQDiesel.ToString();

                    var dQTemp = Math.Round(Convert.ToDecimal(actDieselQuarter) - Convert.ToDecimal(lastDieselQuarter), 3);
                    diffDieselWeek = dQTemp.ToString();
                }

                else
                {
                    actDieselQuarter = error;
                    lastDieselQuarter = error;
                    diffDieselWeek = error;
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