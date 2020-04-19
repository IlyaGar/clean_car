using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Logic.Services.IServices
{
    public interface IPdfReportService
    {
        Task<string> GetFilePath(int id);
    }
}
