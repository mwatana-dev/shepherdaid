using System;

namespace Rite.Software.Shepherdaid
{
    public static class Utility
    {
        public static string ShowErrorMessage(Exception exp)
        {
            string errorMessage = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
            return errorMessage;
        }

        public static string ShowAlert()
        {
            string alertMessage = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {

                alertMessage = ex.Message;
            }
            return alertMessage;
        }
    }
}