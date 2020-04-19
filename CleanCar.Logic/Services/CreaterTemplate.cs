using CleanCar.Logic.Services.IServices;
using InDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace CleanCar.Logic.Services
{
    public class CreaterTemplate : ICreaterTemplate
    {
        public string CreateTemplateINDD(string dirName, string fileName)
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
    }
}
