using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TblFileVersioning.Models
{
    public class FileVersioningModel
    {
        public int fileversionid { get; set; }
        public string GroupCode { get; set; }
        public string SubGroupCode { get; set; }
        public string ProductCode { get; set; }
        public string PurchasePath { get; set; }
        public DateTime VersionDate { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileCode { get; set; }
        public string FileType { get; set; }
        public int DomainId { get; set; }
        public string LanguageCode { get; set; }
        public string CountryCode { get; set; }
    }
}
