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

        public string actDTM { get; set; }
        public string lastDTM { get; set; }
        public string diffDTM { get; set; }

        public string actualEuro { get; set; }
        public string lastEuro { get; set; }
        public string diffEuro { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public ResultTilesPanelViewModels()
        {
        }

        public void returnTilePanelValues()
        {
            string error = "Fel vid datainsamling";

            GetDieselWeek(error);

            GetDieselQuarter(error);

            GetEuroIndex(error);

            GetDTM(error);
        }

        // Popoulates Eurodata stats to RestulTilesPanelViewModel
        private void GetEuroIndex(string error)
        {
            List<EuroExchangeModel> iEPM = new List<EuroExchangeModel>();
            iEPM = db.EuroExchangeModels.OrderByDescending(x => x.Date).ToList();
            if (iEPM == null || iEPM.GetEnumerator().MoveNext())
            {
                var getEuro = iEPM.Select(s => s.euroValue)
                                    .FirstOrDefault();
                actualEuro = getEuro.ToString();
                var countiEPMRow = iEPM.Count();
                if (countiEPMRow > 1)
                {
                    var getLastEuro = iEPM.OrderByDescending(x => x.Date)
                        .Select(x => x.euroValue)
                        .ElementAt(1);

                    lastEuro = getLastEuro.ToString();
                }
                else
                {
                    lastEuro = "0";
                }

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

        // Popoulates DieselQuarter Price stats to RestulTilesPanelViewModel
        private void GetDieselQuarter(string error)
        {
            List<DieselQuarterPriceModel> iDPQ = new List<DieselQuarterPriceModel>();
            iDPQ = db.DieselPriceQuarter.ToList();
            if (iDPQ == null || iDPQ.GetEnumerator().MoveNext())
            {
                var getDieselQ = iDPQ.OrderByDescending(x => x.Year).Select(s => s.DieselQuarterValue)
                                    .FirstOrDefault();
                actDieselQuarter = getDieselQ.ToString();
                var countDPQRow = iDPQ.Count();
                if (countDPQRow > 1)
                {
                    var getLastQDiesel = iDPQ.OrderByDescending(x => x.Year)
                        .Select(x => x.DieselQuarterValue)
                        .ElementAt(1);

                    lastDieselQuarter = getLastQDiesel.ToString();
                }
                else
                {
                    lastDieselQuarter = "0";
                }
                var dQSUM = Math.Round(Convert.ToDecimal(actDieselQuarter) - Convert.ToDecimal(lastDieselQuarter), 3);
                diffDieselQuarter = dQSUM.ToString();
            }

            else
            {
                actDieselQuarter = error;
                lastDieselQuarter = error;
                diffDieselQuarter = error;
            }
        }

        // Popoulates DieselWeek Price stats to RestulTilesPanelViewModel
        private void GetDieselWeek(string error)
        {
            List<DieselWeekModel> iDPW = new List<DieselWeekModel>();
            iDPW = db.DieselPriceWeek.ToList();
            if (iDPW == null || iDPW.GetEnumerator().MoveNext())
            {
                var getDieselW = iDPW.OrderByDescending(x=>x.Year).Select(s => s.DieselWeekValue)
                                    .FirstOrDefault();
                actDieselWeek = getDieselW.ToString();

                var countDPWRow = iDPW.Count();
                if (countDPWRow > 1)
                {
                    var getLastWDiesel = iDPW.OrderByDescending(x => x.Year)
                        .Select(x => x.DieselWeekValue)
                        .ElementAt(1);

                    lastDieselWeek = getLastWDiesel.ToString();
                }
                else
                {
                    lastDieselWeek = "0";
                }
                var dWSUM = Math.Round(Convert.ToDecimal(actDieselWeek) - Convert.ToDecimal(lastDieselWeek), 2);
                diffDieselWeek = dWSUM.ToString();
            }

            else
            {
                actDieselWeek = error;
                lastDieselWeek = error;
                diffDieselWeek = error;
            }
        }

        private void GetDTM(string error)
        {
            List<DtmModel> iDTM = new List<DtmModel>();
            iDTM = db.DtmModels.ToList();
            if (iDTM == null || iDTM.GetEnumerator().MoveNext())
            {
                var getDTM = iDTM.Select(s => s.DieselDTMValue)
                                    .FirstOrDefault();
                actDTM = getDTM.ToString();

                var countDTMRow = iDTM.Count();
                if (countDTMRow > 1)
                {
                    var getLastDTM = iDTM.OrderByDescending(x => x.DieselDTMValue)
                        .Select(x => x.DieselDTMValue)
                        .ElementAt(1);

                    lastDTM = getLastDTM.ToString();
                }
                else
                {
                    lastDTM = "0";
                }
                var dtmSUM = Math.Round(Convert.ToDecimal(actDTM) - Convert.ToDecimal(lastDTM), 2);
                diffDTM = dtmSUM.ToString();
            }

            else
            {
                actDTM = error;
                lastDTM = error;
                diffDTM = error;
            }
        }
    }
}