﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace UI
{
    public static class Global
    {
        public static string ConnectionString 
        {
            get 
            {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }
        }

        public static bool IsPageAccessible(string page)
        {
            if (HttpContext.Current.Session["UserId"] == null)
                return false;
            else
            {
                // call business function with user id and page name
                return true;
            }
        }


        public static bool SendPendingTransactionStatusMail(string customerName, string customerEmail, string status, string employeeName)
        {
            try
            {
                var fromAddress = new MailAddress("group2csefall2015@gmail.com", "SBS");
                var toAddress = new MailAddress(customerEmail, customerName);
                const string fromPassword = "group2fall";
                const string subject = "Update from SBS.";
                string body = string.Format("Hello {0}, <br /> <br />Your pending transaction has been {1} by {2}. <br /><br /> Regards, <br /> SBS Team.", customerName, status, employeeName);

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public static class Validate
    {
        static String UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static String LOWER = "abcdefghijklmnopqrstuvwxyz";
        static String DIGIT = "0123456789";
        static String SPECIAL = @"!@#$_-";

        public static Boolean generalValidate(String inputString, Boolean allowAlpha, Boolean allowNumeral, String allowChars = "")
        {
            String lowerString = inputString.ToLower();

            foreach (char inputChar in lowerString)
            {
                if (allowAlpha)
                    if (LOWER.IndexOf(inputChar) >= 0)
                        continue;
                if (allowNumeral)
                    if (DIGIT.IndexOf(inputChar) >= 0)
                        continue;
                if (allowChars.IndexOf(inputChar) >= 0)
                    continue;

                return false;
            }
            return true;
        }

        public static Boolean isUserNameValid(String inputString)
        {
            return generalValidate(inputString, true, true, "- ");
        }

        public static Boolean isZipCodeValid(String inputString)
        {
            if (inputString.Length > 10)
                return false;

            return generalValidate(inputString, false, true, "-");
        }

        public static Boolean isAddressValid(String inputString)
        {
            if (inputString.Length > 30)
                return false;

            return generalValidate(inputString, true, true, " .");
        }

        public static Boolean isCityValid(String inputString)
        {
            if (inputString.Length > 20)
                return false;

            return generalValidate(inputString, true, false, "- ");
        }

        public static Boolean isStateValid(String inputString)
        {
            if (inputString.Length > 20)
                return false;

            return generalValidate(inputString, true, false, " ");
        }

        public static Boolean isPhoneNumberValid(String inputString)
        {
            if (inputString.Length > 18)
                return false;
            return generalValidate(inputString, false, true, "-() +");
        }

        public static Boolean isEmailAddressValid(String inputString)
        {
            return generalValidate(inputString, true, true, "@.-_");
        }

        public static Boolean isUserIDValid(String inputString)
        {
            return generalValidate(inputString, false, true, "");
        }

        public static Boolean isBranchValid(String inputString)
        {
            return generalValidate(inputString, false, true, "");
        }

        public static Boolean isSecurityQuestionValid(String inputString)
        {
            return generalValidate(inputString, true, true, " '?.,-");
        }

        public static Boolean isSecurityAnswerValid(String inputString)
        {
            return generalValidate(inputString, true, true, " '-,.");
        }

        public static Boolean isDescriptionValid(String inputString)
        {
            return generalValidate(inputString, false, true, " -,.?;$&");
        }

        public static Boolean isAccountNumberValid(String inputString)
        {
            return generalValidate(inputString, false, true, "");
        }

        public static Boolean isAmountValid(String inputString)
        {
            if (inputString.Length > 10)
                return false;
            return generalValidate(inputString, false, true, ".");
        }

        public static Boolean isPasswordValid(String inputString)
        {
            if (inputString.Length < 8)
                return false;

            int upperCount = 0, lowerCount = 0, digitCount = 0, specialCount = 0;

            foreach (char inputChar in inputString)
            {
                if (UPPER.IndexOf(inputChar) >= 0)
                    upperCount++;
                if (LOWER.IndexOf(inputChar) >= 0)
                    lowerCount++;
                if (DIGIT.IndexOf(inputChar) >= 0)
                    digitCount++;
                if (SPECIAL.IndexOf(inputChar) >= 0)
                    specialCount++;
            }

            if (upperCount == 0 || lowerCount == 0 || digitCount == 0 || specialCount == 0)
                return false;

            return generalValidate(inputString, true, true, "!@#$-_");
        }
    }
}