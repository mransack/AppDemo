
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Net;

namespace AppDemo
{
    public class Authorize
    {
       
        public static bool AuthorizeUser()
        {
            if (System.Web.HttpContext.Current.Session["AuthUser"] != null)
            {
                var url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path).ToLower();
                int user_id = Convert.ToInt32(System.Web.HttpContext.Current.Session["AuthUserID"]);
                List<int?> userRoles = GetRoles(user_id);

                bool isAuthorized = false;
                //we now have all the roles, lets check whether user has access to the current page or not
                foreach (var role in userRoles)
                {
                    //Admin
                    if (role == 1)
                    {
                        foreach (var page in AdminPages())
                        {
                            if (url.Contains(page.ToLower()))
                            {
                                isAuthorized = true;
                                break;
                            }
                        }
                    }
                    //if (role == 2) //Contract Approver
                    //{
                    //    foreach (var page in ContractAprroverPages())
                    //    {
                    //        if (url.Contains(page.ToLower()))
                    //        {
                    //            isAuthorized = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (role == 3) //Contract Manager
                    //{
                    //    foreach (var page in ContractManagerPages())
                    //    {
                    //        if (url.Contains(page.ToLower()))
                    //        {
                    //            isAuthorized = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (role == 4) //Report Viewer
                    //{
                    //    foreach (var page in ReportViewerPages())
                    //    {
                    //        if (url.Contains(page.ToLower()))
                    //        {
                    //            isAuthorized = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (role == 5) //Usage Fee Manager
                    //{
                    //    foreach (var page in UsageFeeManagerPages())
                    //    {
                    //        if (url.Contains(page.ToLower()))
                    //        {
                    //            isAuthorized = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    if (role == 6)//Accounting
                    {
                        foreach (var page in AdminPages())
                        {
                            if (url.Contains(page.ToLower()))
                            {
                                isAuthorized = true;
                                break;
                            }
                        }
                    }
                    //if (role == 7)//Notification
                    //{
                    //    foreach (var page in AdminPages())
                    //    {
                    //        if (url.Contains(page.ToLower()))
                    //        {
                    //            isAuthorized = true;
                    //            break;
                    //        }
                    //    }
                    //}
                }

                if (isAuthorized)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public static bool AllowDashboard()
        {

            if (System.Web.HttpContext.Current.Session["AuthUser"] != null)
            {
                var url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
                int user_id = Convert.ToInt32(System.Web.HttpContext.Current.Session["AuthUserID"]);

                bool isAuthorized = true;
                
                if (isAuthorized)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public static bool AuthorizeVendor()
        {
            if (System.Web.HttpContext.Current.Session["AuthVendor"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        //internal static bool sendAppErrorEmail(Exception ex, string name)
        //{
        //    string fromEmail = "support@datagainservices.com";
        //    string toEmail = ConfigurationManager.AppSettings["UnsupportEmail"];
        //    string body = "\n ------------------------------------------------ Exception Details  ------------------------------------------------";

        //    body = body + "\n Message :" + ex.Message + " USER NAME was [" + name + "] " + "\n Targetsite :" + ex.TargetSite + "\n StackTrace" + ex.StackTrace;

        //    if (ex.InnerException != null)
        //    {
        //        body = body + "\n \n InnerException Message :" + ex.InnerException.Message + "\n InnerException Targetsite :" + ex.InnerException.TargetSite + "\n StackTrace" + ex.InnerException.StackTrace;
        //    }

        //    body = body + "\n\n Thanks";

        //    //return SendExceptionMail(fromEmail, toEmail, "Application Error Occured", body, false);
        //    return SendExceptionMail(fromEmail, toEmail, "SCC -"+ex.Message +" "+name, body, false); //Need to send on next Delivery 02/05/2019
        //}
        //public static bool SendExceptionMail(string fromEmail, string toEmail, string subject, string body, bool isHtml)
        //{
        //    MailMessage mail = new MailMessage(fromEmail, toEmail, subject, body);
        //    mail.IsBodyHtml = isHtml;
        //    return SendExceptionMail(mail);
        //}
        //public static bool SendExceptionMail(MailMessage mail)
        //{
        //    try
        //    {
        //        SmtpClient client = new SmtpClient();
        //        client.EnableSsl = false;
        //        client.Port = 25;
        //        client.Send(mail);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public static cse_users GetLoggedInUser(string email)
        {
            MvcAppDemoEntities db = new MvcAppDemoEntities();
            using (db = new MvcAppDemoEntities())
            {
               cse_users userObj = db.cse_users.Where(u => u.email_id == email).FirstOrDefault();
               return userObj;
            }
        }

        //public static List<string> GetAdditionalVendors(string invoice)
        //{
        //    utahcontractsEntities db = new utahcontractsEntities();
            
        //    using (db = new utahcontractsEntities())
        //    {
        //        var contract_id =  db.cse_invoicemaster.Where(u => u.invoice_id == invoice).Select(u=>u.contract_id).FirstOrDefault();
        //        var emails = db.cse_vendormaster.Where(x => x.contract_id == contract_id).Select(x=>x.vendor_email).ToList();
        //        if(emails!=null && emails.Count > 0)
        //        {
        //            emails = emails.ConvertAll(d => d.ToLower()).Distinct().ToList();
        //            return emails;
        //        }
        //        return null;
        //    }
        //}
        
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

    
        public static Object CopyObject(Object source, Object destination)
            {
                try
                {
                    Type sourceType = source.GetType();
                    PropertyInfo[] srcProperties = sourceType.GetProperties();
                    Type destType = destination.GetType();
                    PropertyInfo[] destProperties = destType.GetProperties();
                    PropertyInfo destProperty = null;
                    foreach (PropertyInfo property in srcProperties)
                    {
                        destProperty = destProperties.Where(a => a.Name.Equals(property.Name)).FirstOrDefault();
                        if (destProperty != null)
                        {
                            destProperty.SetValue(destination, property.GetValue(source));
                        }
                    }
                    return destination;
                }
                catch (Exception e)
                {
                    throw new Exception("Exception in CopyToObject- " + e.Message);
                }
            }
        
        public static List<string> AdminPages()
        {
            string[] pages = {
                "/Account/Login",
                "/Home/Dashboard",
                "/Home/Search",
                "/Home/Index",
                "/Home/",
                "/Manage/Contracts",
                "/Contract/Index",
                "/Contract/",
                "/Contract/BulkUpdate",
                "/Contract/Details",
                "/Manage/Reports",
                "/Manage/Usagefees",
                "/Manage/Users",
                "/Manage/Keywords",
                "/Manage/Commodities",
                "/Manage/Vendors",
                "/Manage/Portfolio",
                "/Manage/KeywordSearch",
                "/Manage/Roles",
                "/Contract/Approve",
                "/Account/Logout",

                "/Manage/AddKeywords",
                "/Manage/UpdateKeyword",
                "/Manage/DeleteKeyword",
                "/Manage/UpdateCommodity",
                "/Manage/AddCommodity",
                "/Manage/DeleteCommodity",
                "/Manage/UpdatePortfolio",
                "/Manage/AddPortfolio",
                "/Manage/DeletePortfolio",
                "/Manage/SaveUser",

                "/Contract/CreateContract",
                "/Contract/UpdateContract",
                "/Contract/SaveNotes",
                "/Contract/AddVendors",
                "/Contract/UpdateVendors",
                "/Contract/DeleteVendor",
                "/Contract/UpdateContract",
                "/Contract/UpdateContract",
                "/Manage/AdminReports",
                "/Manage/Upload",
                "/Manage/AddFrequentKeywords",
                "/Manage/GetSelectedKeywords"
            };
            return pages.ToList();
        }

        public static List<int?> GetRoles(int user_id)
        {
            MvcAppDemoEntities db = new MvcAppDemoEntities();
            using (db = new MvcAppDemoEntities())
            {
                List<int?> userRoles = db.cse_role_userrelation.Where(u => u.user_id == user_id).Select(x => x.role_id).ToList();
                return userRoles;
            }
        }

    }
}