using CleanCar.DAL.Models;
using CleanCar.DAL.Repositories.IRepositories;
using CleanCar.Logic.Services.IServices;
using InDesign;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace CleanCar.Logic.Services
{
    public class PdfReportService : IPdfReportService
    {
        private readonly ICustomerRepository _repository;
        private readonly string dirName = "content";
        private readonly string fileInddName = "tamplate";
        private readonly string relativeFileInddName = "content/tamplate.indd";

        public PdfReportService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetFilePath(int id)
        {
            string fullFileInddName = HostingEnvironment.MapPath($"~/{relativeFileInddName}");

            //string fullFileInddName = System.IO.Path.GetFullPath(path);
            if (!File.Exists(fullFileInddName))
            {
                string pathDir = HostingEnvironment.MapPath($"~/{dirName}");
                DirectoryInfo dirInfo = new DirectoryInfo(pathDir);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                    fullFileInddName = CreateTemplateINDD(pathDir, fileInddName);
                }
                else
                {
                    fullFileInddName = CreateTemplateINDD(pathDir, fileInddName);
                }
            }

            return CreateReportPDF(fullFileInddName, await _repository.GetCustomerAsync(id));
        }

        private string CreateTemplateINDD(string dirName, string fileName)
        {
            // Create application instance
            Type type = Type.GetTypeFromProgID("InDesign.Application");
            dynamic application = Activator.CreateInstance(type, true);

            //Set unit type
            application.ViewPreferences.HorizontalMeasurementUnits = idMeasurementUnits.idMillimeters;
            application.ViewPreferences.VerticalMeasurementUnits = idMeasurementUnits.idMillimeters;

            // Create new document
            application.Documents.Add(true, application.DocumentPresets.FirstItem());

            // Get active document and change some settings
            dynamic document = application.ActiveDocument;  //Document
            document.DocumentPreferences.FacingPages = false;
            document.DocumentPreferences.PageWidth = 210;
            document.DocumentPreferences.PageHeight = 297;

            // Get first page (already created) and set margins
            dynamic page = document.Pages[1];   // Page
            page.MarginPreferences.Top = 10;
            page.MarginPreferences.Bottom = 10;
            page.MarginPreferences.Left = 20;
            page.MarginPreferences.Right = 10;

            // Create rectangle and fit an image into it
            dynamic rectangle = page.Rectangles.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);    //Rectangle
            rectangle.GeometricBounds = new[] { 10, 20, 50, 60 };
            rectangle.Place(HostingEnvironment.MapPath("~/logo.png"), false);
            rectangle.Fit(idFitOptions.idContentToFrame);
            rectangle.StrokeWeight = 0;

            dynamic textFrameTitle = page.TextFrames.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);   //TextFrame
            textFrameTitle.GeometricBounds = new[] { 20, 60, 30, 195 };
            textFrameTitle.Contents = "ОАО «Global Clean Car Corporation»";
            dynamic paragraphTitle = textFrameTitle.Paragraphs[1];  // Paragraph
            paragraphTitle.Justification = idJustification.idCenterAlign;
            paragraphTitle.PointSize = 20;

            dynamic textFrameSpecification = page.TextFrames.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);   //TextFrame
            textFrameSpecification.GeometricBounds = new[] { 30, 60, 50, 195 };
            textFrameSpecification.Contents = "Помой машину сегодня и мы гарантируем - завтра не будет дождя!\nНаши сертифицированные шаманы умеют обращаться с бубном.";
            dynamic paragraphSpecification = textFrameSpecification.Paragraphs[1];  // Paragraph
            paragraphSpecification.Justification = idJustification.idCenterAlign;
            paragraphSpecification.PointSize = 10;

            string pathF = System.IO.Path.GetFullPath($"{dirName}/{fileName}");
            document.Save(pathF);
            return pathF;
        }

        private string CreateReportPDF(string fileName, Customer customer)
        {
            // Create application instance
            Type type = Type.GetTypeFromProgID("InDesign.Application", true);
            dynamic application = Activator.CreateInstance(type);

            // Set unit type
            application.ViewPreferences.HorizontalMeasurementUnits = idMeasurementUnits.idMillimeters;
            application.ViewPreferences.VerticalMeasurementUnits = idMeasurementUnits.idMillimeters;

            // Open document
            application.Open(fileName);

            // Get active document and change some settings
            dynamic document = application.ActiveDocument;  //Document
            document.DocumentPreferences.FacingPages = false;
            document.DocumentPreferences.PageWidth = 210;
            document.DocumentPreferences.PageHeight = 297;

            // Get first page (already created) and set margins
            dynamic page = document.Pages[1];   // Page
            page.MarginPreferences.Top = 10;
            page.MarginPreferences.Bottom = 10;
            page.MarginPreferences.Left = 20;
            page.MarginPreferences.Right = 10;

            dynamic textFrameCustomer = page.TextFrames.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);   //TextFrame
            textFrameCustomer.GeometricBounds = new[] { 50, 60, 60, 155 };
            textFrameCustomer.Contents = $"Уважаемый {customer.FirstName} {customer.SecondName}!";
            dynamic paragraphCustomer = textFrameCustomer.Paragraphs[1];    // Paragraph
            paragraphCustomer.Justification = idJustification.idCenterAlign;
            paragraphCustomer.PointSize = 12;

            dynamic textFrameThanks = page.TextFrames.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);  //TextFrame
            textFrameThanks.GeometricBounds = new[] { 60, 20, 270, 195 };
            textFrameThanks.Contents = "Благодарим Вас за то, что вы воспользовались нашими услугами:";
            int countRow = customer.Orders.Count() + 2;
            List<string> listStringOperations = new List<string>() { "#", "Operation", "Date", "Cost" };
            int number = 1;
            foreach (var operation in customer.Orders)
            {
                listStringOperations.AddRange(new List<string>() {
                    number++.ToString(), 
                    operation.Operation.Name, 
                    operation.DateTime.ToString("yyyy-MM-dd"),
                    operation.Price.ToString() });
            }
            listStringOperations.AddRange(new List<string>() { "Total", $"{customer.Orders.Select(c => c.Price).Count()}", "", $"{customer.Orders.Select(c => c.Price).Sum()}" });
            if (countRow - 34 < 0)
            {
                dynamic table = textFrameThanks.Tables.Add();
                table.BodyRowCount = countRow;
                table.ColumnCount = 4;
                table.Contents = listStringOperations.ToArray();

                dynamic paragraphThanks = textFrameThanks.Paragraphs[1];
                paragraphThanks.Justification = idJustification.idCenterAlign;
                paragraphThanks.PointSize = 12;

                dynamic textFrameFooter = page.TextFrames.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);
                textFrameFooter.GeometricBounds = new[] { 277, 20, 287, 200 };
                textFrameFooter.Contents = "ОАО «Global Clean Car Corporation». Made in InDesign.";
                dynamic paragraphFooter = textFrameFooter.Paragraphs[1];
                paragraphFooter.Justification = idJustification.idCenterAlign;
                paragraphFooter.PointSize = 10;
            }
            else
            {
                dynamic table = textFrameThanks.Tables.Add();
                table.BodyRowCount = 34;
                table.ColumnCount = 3;
                var listFirstTAble = listStringOperations.Take(34 * 3).ToList();
                table.Contents = listFirstTAble.ToArray();

                dynamic paragraphThanks = textFrameThanks.Paragraphs[1];
                paragraphThanks.Justification = idJustification.idCenterAlign;
                paragraphThanks.PointSize = 12;

                listStringOperations = listStringOperations.Skip(34 * 4).ToList();

                int pageCount = 1;
                dynamic textFrameFooter = page.TextFrames.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);
                textFrameFooter.GeometricBounds = new[] { 277, 20, 287, 200 };
                textFrameFooter.Contents = $"ОАО «Global Clean Car Corporation». Made in InDesign. Page {pageCount++}";
                dynamic paragraphFooter = textFrameFooter.Paragraphs[1];
                paragraphFooter.Justification = idJustification.idCenterAlign;
                paragraphFooter.PointSize = 10;

                while (countRow > 0)
                {
                    //Create second page and set margins
                    page = document.Pages.Add(idLocationOptions.idUnknown, document);
                    page.MarginPreferences.Top = 10;
                    page.MarginPreferences.Bottom = 10;
                    page.MarginPreferences.Left = 20;
                    page.MarginPreferences.Right = 10;

                    listStringOperations.Insert(0, "Cost");
                    listStringOperations.Insert(0, "Date");
                    listStringOperations.Insert(0, "Operation");
                    listStringOperations.Insert(0, "#");

                    dynamic textFrameNext = page.TextFrames.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);
                    textFrameNext.GeometricBounds = new[] { 10, 20, 270, 200 };
                    dynamic tableNext = textFrameNext.Tables.Add();
                    tableNext.BodyRowCount = listStringOperations.Count / 4 < 44 ? listStringOperations.Count / 4 : 44;
                    tableNext.ColumnCount = 4;

                    tableNext.Contents = listStringOperations.ToArray();

                    dynamic paragraphNext = textFrameNext.Paragraphs[1];
                    paragraphNext.Justification = idJustification.idCenterAlign;
                    paragraphNext.PointSize = 12;
                    listStringOperations = listStringOperations.Skip(44 * 4).ToList();
                    countRow = listStringOperations.Count / 4;

                    dynamic textFrameFooterPage = page.TextFrames.Add(document.Layers.FirstItem(), idLocationOptions.idUnknown, page);
                    textFrameFooterPage.GeometricBounds = new[] { 277, 20, 287, 200 };
                    textFrameFooterPage.Contents = $"ОАО «Global Clean Car Corporation». Made in InDesign. Page {pageCount++}";
                    dynamic paragraphFooterPage = textFrameFooterPage.Paragraphs[1];
                    paragraphFooterPage.Justification = idJustification.idCenterAlign;
                    paragraphFooterPage.PointSize = 10;
                }
            }
            //string fullPath = System.IO.Path.GetFullPath($"{dirName}");
            string pathDir = HostingEnvironment.MapPath($"~/{dirName}");
            string filePdfName = $@"{pathDir}\{customer.FirstName}_{customer.SecondName}_{DateTime.Now.ToString("yyyy-MM-dd")}.pdf";
            document.Export(idExportFormat.idPDFType, filePdfName, false);

            document.Close(idSaveOptions.idNo);

            return filePdfName;
            //throw new NotImplementedException();
        }
    }
}
