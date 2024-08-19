using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text;
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
       public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var totalVolumeCredits = 0;
            

            var statement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items")
            );
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                statement
            );

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true
            };

            var filePath = "statement.txt";
            using (var writer = XmlWriter.Create(filePath, settings))
            {
                doc.Save(writer);
            } 

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var lines = play.Lines;

                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;

               
                var thisCredits = 0;
                IPerformanceCalculator calculator;

                var tragedyCalculator = new TragedyCalculator();
                var comedyCalculator = new ComedyCalculator();
                var historicalCalculator = new HistoricalCalculator(tragedyCalculator, comedyCalculator);

                switch (play.Type)
                {
                    case "tragedy":
                        calculator = tragedyCalculator;
                        thisCredits = calculator.CalculateVolumeCredits(perf, play);
                        break;
                    case "comedy":
                        calculator = comedyCalculator;
                        thisCredits = calculator.CalculateVolumeCredits(perf, play);
                        break;
                    case "history":
                        calculator = historicalCalculator;
                        thisCredits = calculator.CalculateVolumeCredits(perf, play);
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }

                var thisAmount = calculator.CalculateAmount(perf, play)/100.0;

                statement.Element("Items").Add(
                    new XElement("Item",
                        new XElement("AmountOwed", FormatCurrency(thisAmount)),
                        new XElement("EarnedCredits", thisCredits), 
                        new XElement("Seats", perf.Audience)
                    )
                );

                totalAmount += (int)(thisAmount*100); 
                totalVolumeCredits += thisCredits;
            }

            statement.Add(new XElement("AmountOwed", FormatCurrency(totalAmount/100.0)));
            statement.Add(new XElement("EarnedCredits", totalVolumeCredits));
            doc.Save(filePath);
            var fileContent = File.ReadAllText(filePath, Encoding.UTF8);

            return fileContent;
        }

        private string FormatCurrency(double amount)
        {
            if (amount % 1 == 0)
            {
                return amount.ToString("F0", CultureInfo.InvariantCulture);
            }
            else
            {
                return amount.ToString("F1", CultureInfo.InvariantCulture);
            }
        }

    }
}