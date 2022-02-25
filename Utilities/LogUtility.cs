using System;
using System.Windows.Forms;

using Serilog;
using Serilog.Events;

namespace Grimoire.Utilities
{
    /// <summary>
    /// Provides abstraction ontop of Serilog to automate the handling of log messages
    /// </summary>
    public static class LogUtility
    {
        public static ILogger Log = Serilog.Log.Logger;

        /// <summary>
        /// Record the provided message via SeriLog while also showing the message to a caller via MessageBox with given title.
        /// args will be processed for string formatting
        /// </summary>
        /// <param name="msg">Message to be recorded/displayed</param>
        /// <param name="title">Title of the MessageBox to be shown</param>
        /// <param name="level">Level of log / message</param>
        /// <param name="args">Not yet implemented</param>
        public static void MessageBoxAndLog(string msg, string title, LogEventLevel level,  params object[] args)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;
            
            switch (level)
            {
                case LogEventLevel.Information:
                    Log.Information(msg);
                    break;

                case LogEventLevel.Error:
                    Log.Error(msg);
                    icon = MessageBoxIcon.Error;
                    break;

                case LogEventLevel.Warning:
                    Log.Warning(msg);
                    icon = MessageBoxIcon.Warning;
                    break;
            }

            MessageBox.Show(msg, title, MessageBoxButtons.OK, icon);
        }

        public static void MessageBoxAndLog(Exception ex, string opName, string title, LogEventLevel level)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;

            string msg = $"An exception has occured during: {opName}\n\n\tMessage:\n\t- {ex.Message}\n\nStack-Trace:\n\t- {ex.StackTrace}";

            switch (level)
            {
                case LogEventLevel.Information:
                    Log.Information(msg);
                    break;

                case LogEventLevel.Error:
                    Log.Error(msg);
                    icon = MessageBoxIcon.Error;
                    break;

                case LogEventLevel.Warning:
                    Log.Warning(msg);
                    icon = MessageBoxIcon.Warning;
                    break;
            }

            MessageBox.Show(msg, title, MessageBoxButtons.OK, icon);
        }
    }
}
