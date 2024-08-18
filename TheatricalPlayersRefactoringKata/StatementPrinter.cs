using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            var tragedyCalculator = new TragedyCalculator();
            var comedyCalculator = new ComedyCalculator();
            var historicalCalculator = new HistoricalCalculator(tragedyCalculator, comedyCalculator);

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                IPerformanceCalculator calculator;

                switch (play.Type)
                {
                    case "tragedy":
                        calculator = tragedyCalculator;
                        break;
                    case "comedy":
                        calculator = comedyCalculator;
                        break;
                    case "history":
                        calculator = historicalCalculator;
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }

               
                var thisAmount = calculator.CalculateAmount(perf, play)/100.0;
                var thisCredits = calculator.CalculateVolumeCredits(perf, play);

                
                volumeCredits += thisCredits;

                
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
                totalAmount += (int)(thisAmount * 100);
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount/100.0);
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }
    }
}