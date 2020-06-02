using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using System.ServiceProcess;

namespace FBChecklist.Common
{
    public static class Helpers
    {
       
        public class DownloadFile : Controller
        {
            private string _Url;
            private string _fileName;
            public DownloadFile(string url, string fileName)
            {

                _Url = url;
                _fileName = fileName;

            }
            public FileResult download()
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(_Url);
                string[] a = _Url.Split('.');
                _fileName = String.Format("{1}.{0}", a[a.Length - 1], _fileName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, _fileName);
            }
        }
        public static class Messages
        {

          
            public static string EMPTY_USERNAME_PASSWORD = "Empty username and/or password";
            public static string OPERATION_COMPLETED_SUCCESSFULLY = "Operation Completed Successfully";
            public static string WRONG_FULL_NAME_FORMAT = "Wrong full name format, it should be like " + "Samson Fiado" + " and avoid spaces";
            public static string PASSWORD_COMFIRM_PASSWORD_MISMATCH = "Password and Comfirm Password Mismatch";
            public static string DONT_HAVE_ACCESS = "You do not have access to the page";
            public static string MINIMUM_DAYS_TO_APPLY_FOR_LEAVE_GRANT = "Minimum leave days to apply for leave grant is 7, you are applying for: ";
            public static string START_DATE_MUST_BE_SMALLER = "Starting Date must be smaller than end date";
            public static string INVALID_LEAVE_DATES_ENTRY = "Invalid leave dates entry";
            public static string APPLICATION_CANT_BE_EDITED_AT_THIS_STAGE = "Application can not be edited at this stage,talk to your line manager";
            public static string ALREADY_APPLIED_FOR_LEAVE_GRANT_THIS_FISCAL_YEAR = "You have already applied for Leave Grant this financial year";
            public static string NOT_ELIGIBLE_FOR_LEAVE_GRANT = "You are not eligible for leave grant";
          


        }
        public static string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(',');
            }
            return builder.ToString();
        }

       
        public static int GetQuarter(this DateTime dt)
        {
            return (dt.Month - 1) / 3 + 1;
        }
      

        public struct TrainingReason
        {
            public string ID;
            public string REASON;

            public TrainingReason(string id, string reason)
            {
                ID = id;
                REASON = reason;
            }
        }

        public struct Punishment
        {
            public string ID { get; set; }
            public string PUNISHMENT { get; set; }

            public Punishment(string id, string punishment)
            {
                ID = id;
                PUNISHMENT = punishment;
            }
        }


        public struct NotificationType
        {
            public string ID { get; set; }
            public string TYPE { get; set; }

            public NotificationType(string id, string type)
            {
                ID = id;
                TYPE = type;
            }
        }

        public class parameters
        {
            public static int Total_Records_Per_Page_On_Search = Int32.MaxValue - 1;
           
            //FileFormats
            public static int Pdf = 1;
            public static int Excel = 2;
            public static int Csv = 3;
            public static int Html = 4;
            public static int Xml = 5;
            public static int IsAct = 2;
            public static int ActiveDirectory = 22;

            //ActionTypes
            public static string DeleteAction = "DELETE";
            public static string CreateAction = "CREATE";
            public static string UpdateAction = "UPDATE";
            public static string ProcessAction = "PROCESS";

            //Clustered Server Disk Settings
            public static int hasClusteredDisks = 1;
            public static int NoClusteredDisks = 2;

           



            //Training Reasons

            public static IEnumerable<TrainingReason> TRAINING_REASONS = new List<TrainingReason>
            {
                new TrainingReason("1","Skills Gap"),
                new TrainingReason("2","Developmental Training")

            };

            public static IEnumerable<NotificationType> NOTIFICATION_TYPES = new List<NotificationType>()
            {
                new NotificationType("Announcement","Announcement"),
                new NotificationType("Memorandum","Memorandum")
            };

          
        }
        public static string ToSafeFileName(string s)
        {
            return s
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("\"", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
        }

        public static decimal? getDecimal(object rawValue)
        {
            decimal finalValue;

            double? doubleValue = rawValue as double?;
            if (doubleValue.HasValue)
                return (decimal)doubleValue.Value;
            else if (decimal.TryParse(rawValue as string, out finalValue))
            {
                return finalValue;
            }
            else
            {
                return null;//could also throw an exception if you wanted.
            }
        }

        //Add System.ServiceProcess to access this
        public static bool ServiceIsRunning(string ServiceName)
        {           
            ServiceController sc = new ServiceController();
            sc.ServiceName = ServiceName;

            if (sc.Status == ServiceControllerStatus.Running)
            {
                Console.WriteLine("Service is Up");
                return true;

            }
            else
            {
                Console.WriteLine("Service is Down");
                return false;
            }
        }

       
        public static decimal DiskSpaceInGigabytes(decimal value)
        {
            const decimal BytesInGB = 1073741824;
            var sizeInGigabytes = (value) / BytesInGB;
            return sizeInGigabytes;
        }

       // public static string DatabaseConnect2 = ConfigurationManager.ConnectionStrings["MIHR"].ConnectionString;
        public static bool CheckConnection()
        {
            String address = "208.69.34.231";
            System.Net.NetworkInformation.Ping pingsender = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply reply = pingsender.Send(address, 1000);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       


        //Duplicates in NewBroker
        public static string AddSuffix(string filename, string suffix)
        {
            string fDir = Path.GetDirectoryName(filename);
            string fName = Path.GetFileNameWithoutExtension(filename);
            string fExt = Path.GetExtension(filename);
            string renamedFilePath = Path.Combine(fDir, String.Concat(fName, suffix, fExt));
            return renamedFilePath;
        }

     


      

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }


        private const string cryptoKey = "AHCX@2017";

        public static string BandIdentifier = "Tier";

        public const string VALID_MOBILE_REGEX = @"^(099|088|+265)\d{7}$";
        public const string VALID_TELEPHONE_REGEX = @"^(01)\d{6}$";

        public static readonly string NumericRegex = @"^\d+$";

        // The Initialization Vector for the DES encryption routine
        private static readonly byte[] IV = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };


        // Decrypts provided string parameter

        public static string Decrypt(string s)
        {
            if (s == null || s.Length == 0) return string.Empty;
            string result = string.Empty;
            try
            {
                byte[] buffer = Convert.FromBase64String(s);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                des.Key = MD5.ComputeHash(Encoding.ASCII.GetBytes(cryptoKey));
                des.IV = IV;
                result = Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
                return string.Empty;
            }
            return result;
        }

        public static T IsNull<T>(T Value, T DefaultValue)
        {
            if (Value == null || Convert.IsDBNull(Value)) return DefaultValue;
            else return Value;

        }


        // Sanitises input by removing invalid characters and trimming spaces

        public static string SanitiseInput(string InputString)
        {
            try
            {
                return Regex.Replace(InputString, "[^.A-Za-z0-9 _]", string.Empty).Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        //gets SelectedTxt of SelectList
        public static string getText(SelectList selectList)
        {
            string text = selectList.Where(x => x.Selected).FirstOrDefault().Text;
            return text;
        }
        // Converts a string to proper Title Case (e.g. SAMSON FIADO ==> Samson Fiado)

        public static string TitleCaseString(string s)
        {
            if (s == null) return s;
            string[] words = s.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;
                char firstChar = char.ToUpper(words[i][0]);
                string rest = string.Empty;
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return string.Join(" ", words);
        }

        public static string HashPassword(string plainText)
        {
            try
            {
                SHA512Managed managed = new SHA512Managed();
                return Convert.ToBase64String(managed.ComputeHash(Encoding.ASCII.GetBytes(plainText)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetUsername(string input)
        {
            String[] names = input.Split(' ');
            String lname = names.Last();
            String fname = names.First();
            char fletter = fname[0];

            String username = fletter + lname;

            return username.ToLower();

        }
        public static long GetPassword()
        {
            return DateTime.Now.Ticks;
        }





        public static string FormatName(string theName)
        {

            TextInfo theNameFormatting = Thread.CurrentThread.CurrentCulture.TextInfo;
            theName = Regex.Replace(theName, "[ ]{2,}", " ");
            if (!string.IsNullOrEmpty(theName))
            {
                return Helpers.TitleCaseString(theNameFormatting.ToTitleCase(theName)).Trim();
            }
            else
            {
                return string.Empty;
            }
        }


        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public static string ConvertToCommonDateFormat(DateTime Input)
        {
            try
            {
                return Input.ToString("dd MMM yyyy HH:mm");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}