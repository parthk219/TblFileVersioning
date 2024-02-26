using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
//using System.Web.Mvc;
using TblFileVersioning.Models;

namespace TblFileVersioning.Controllers
{
    [Route("FileVersioning")]
    public class FileVersioningController : Controller
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True;Initial Catalog=Files;";

        // GET: FileVersioning
        [HttpGet("FilesList")]
        public ActionResult FilesList()
        {
            List<FileVersioningModel> fileVersions = new List<FileVersioningModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM tblFileVersioning";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileVersioningModel fileVersion = new FileVersioningModel
                            {
                                fileversionid = Convert.ToInt32(reader["fileversionid"]),
                                GroupCode = reader["GroupCode"].ToString(),
                                SubGroupCode = reader["SubGroupCode"].ToString(),
                                ProductCode = reader["ProductCode"].ToString(),
                                PurchasePath = reader["PurchasePath"].ToString(),
                                VersionDate = Convert.ToDateTime(reader["VersionDate"]),
                                FilePath = reader["FilePath"].ToString(),
                                FileName = reader["FileName"].ToString(),
                                FileCode = reader["FileCode"].ToString(),
                                FileType = reader["FileType"].ToString(),
                                DomainId = Convert.ToInt32(reader["DomainId"]),
                                LanguageCode = reader["LanguageCode"].ToString(),
                                CountryCode = reader["CountryCode"].ToString()
                            };

                            fileVersions.Add(fileVersion);
                        }
                    }
                }
            }

            return Json(fileVersions);
        }



        //[HttpPost]
        [HttpPost("InsertRecord")]
        public void InsertRecord(FileVersioningModel fileVersion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            INSERT INTO tblFileVersioning (GroupCode, SubGroupCode, ProductCode, PurchasePath, VersionDate, FilePath, FileName, FileCode, FileType, DomainId, LanguageCode, CountryCode)
            VALUES (@GroupCode, @SubGroupCode, @ProductCode, @PurchasePath, @VersionDate, @FilePath, @FileName, @FileCode, @FileType, @DomainId, @LanguageCode, @CountryCode)
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupCode", fileVersion.GroupCode);
                    command.Parameters.AddWithValue("@SubGroupCode", fileVersion.SubGroupCode);
                    command.Parameters.AddWithValue("@ProductCode", fileVersion.ProductCode);
                    command.Parameters.AddWithValue("@PurchasePath", fileVersion.PurchasePath);
                    command.Parameters.AddWithValue("@VersionDate", fileVersion.VersionDate);
                    command.Parameters.AddWithValue("@FilePath", fileVersion.FilePath);
                    command.Parameters.AddWithValue("@FileName", fileVersion.FileName);
                    command.Parameters.AddWithValue("@FileCode", fileVersion.FileCode);
                    command.Parameters.AddWithValue("@FileType", fileVersion.FileType);
                    command.Parameters.AddWithValue("@DomainId", fileVersion.DomainId);
                    command.Parameters.AddWithValue("@LanguageCode", fileVersion.LanguageCode);
                    command.Parameters.AddWithValue("@CountryCode", fileVersion.CountryCode);

                    command.ExecuteNonQuery();
                }
            }
        }

        //
        [HttpPost("InsertPDFRecord")]
        public void InsertPDFRecord(IFormFile file, FileVersioningModel fileVersion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            INSERT INTO tblFileVersioning (GroupCode, SubGroupCode, ProductCode, PurchasePath, VersionDate, FilePath, FileName, FileCode, FileType, DomainId, LanguageCode, CountryCode)
            VALUES (@GroupCode, @SubGroupCode, @ProductCode, @PurchasePath, @VersionDate, @FilePath, @FileName, @FileCode, @FileType, @DomainId, @LanguageCode, @CountryCode)
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set parameters for text data
                    command.Parameters.AddWithValue("@GroupCode", fileVersion.GroupCode);
                    command.Parameters.AddWithValue("@SubGroupCode", fileVersion.SubGroupCode);
                    command.Parameters.AddWithValue("@ProductCode", fileVersion.ProductCode);
                    command.Parameters.AddWithValue("@PurchasePath", fileVersion.PurchasePath);
                    command.Parameters.AddWithValue("@VersionDate", fileVersion.VersionDate);
                    command.Parameters.AddWithValue("@FilePath", fileVersion.FilePath);
                    command.Parameters.AddWithValue("@FileName", fileVersion.FileName);
                    command.Parameters.AddWithValue("@FileCode", fileVersion.FileCode);
                    command.Parameters.AddWithValue("@FileType", fileVersion.FileType);
                    command.Parameters.AddWithValue("@DomainId", fileVersion.DomainId);
                    command.Parameters.AddWithValue("@LanguageCode", fileVersion.LanguageCode);
                    command.Parameters.AddWithValue("@CountryCode", fileVersion.CountryCode);

                    // Execute the query for text data
                    command.ExecuteNonQuery();

                    // Save the file to C drive
                    if (file != null && file.Length > 0)
                    {
                        var filePath = Path.Combine("D:\\", fileVersion.FilePath, fileVersion.FileName);
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }

            }
        }


        //









        [HttpPatch("UpdateRecord")]

        public void UpdateRecord(FileVersioningModel fileVersion)
        {

            //int fid = fileVersion.fileversionid;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            UPDATE tblFileVersioning
            SET GroupCode = @GroupCode, SubGroupCode = @SubGroupCode, ProductCode = @ProductCode,
                PurchasePath = @PurchasePath, VersionDate = @VersionDate, FilePath = @FilePath,
                FileName = @FileName, FileCode = @FileCode, FileType = @FileType,
                DomainId = @DomainId, LanguageCode = @LanguageCode, CountryCode = @CountryCode
            WHERE FileVersionId = @fid
        ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@Id", fileVersion.fileversionid);
                    command.Parameters.AddWithValue("@GroupCode", fileVersion.GroupCode);
                    command.Parameters.AddWithValue("@SubGroupCode", fileVersion.SubGroupCode);
                    command.Parameters.AddWithValue("@ProductCode", fileVersion.ProductCode);
                    command.Parameters.AddWithValue("@PurchasePath", fileVersion.PurchasePath);
                    command.Parameters.AddWithValue("@VersionDate", fileVersion.VersionDate);
                    command.Parameters.AddWithValue("@FilePath", fileVersion.FilePath);
                    command.Parameters.AddWithValue("@FileName", fileVersion.FileName);
                    command.Parameters.AddWithValue("@FileCode", fileVersion.FileCode);
                    command.Parameters.AddWithValue("@FileType", fileVersion.FileType);
                    command.Parameters.AddWithValue("@DomainId", fileVersion.DomainId);
                    command.Parameters.AddWithValue("@LanguageCode", fileVersion.LanguageCode);
                    command.Parameters.AddWithValue("@CountryCode", fileVersion.CountryCode);
                    command.Parameters.AddWithValue("@fid", fileVersion.fileversionid);

                    command.ExecuteNonQuery();
                }
            }
        }

        [HttpDelete("DeleteRecord")]
        public void DeleteRecord(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM tblFileVersioning WHERE FileVersionId = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        [HttpPost("GetFile")]
        public ActionResult<FileVersioningModel> GetFileVersionByProperties(string fileType, string languageCode, string productCode)
        {
            //FileVersioningModel fileVersion = null;
            List<FileVersioningModel> fileVersions = new List<FileVersioningModel>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM tblFileVersioning WHERE FileType = @FileType AND LanguageCode = @LanguageCode AND ProductCode = @ProductCode";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileType", fileType);
                    command.Parameters.AddWithValue("@LanguageCode", languageCode);
                    command.Parameters.AddWithValue("@ProductCode", productCode);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //if (reader.Read())
                        while (reader.Read())

                        {
                            FileVersioningModel fileVersion = new FileVersioningModel
                            {
                                fileversionid = Convert.ToInt32(reader["fileversionid"]),
                                GroupCode = reader["GroupCode"].ToString(),
                                SubGroupCode = reader["SubGroupCode"].ToString(),
                                ProductCode = reader["ProductCode"].ToString(),
                                PurchasePath = reader["PurchasePath"].ToString(),
                                VersionDate = Convert.ToDateTime(reader["VersionDate"]),
                                FilePath = reader["FilePath"].ToString(),
                                FileName = reader["FileName"].ToString(),
                                FileCode = reader["FileCode"].ToString(),
                                FileType = reader["FileType"].ToString(),
                                DomainId = Convert.ToInt32(reader["DomainId"]),
                                LanguageCode = reader["LanguageCode"].ToString(),
                                CountryCode = reader["CountryCode"].ToString()
                            };
                            fileVersions.Add(fileVersion);

                        }
                    }
                }
            }

            return Json(fileVersions);
        }


    }
}


