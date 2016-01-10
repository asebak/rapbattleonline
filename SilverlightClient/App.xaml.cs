#region Using

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Browser;
using Common.Types.Attributes;

#endregion

namespace RapBattleAudio
{
    public partial class App
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Startup event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
        private void Application_Startup([NotNull] object sender, [NotNull] StartupEventArgs e)
        {
            var battleId = Convert.ToInt32(e.InitParams["battleId"]);
            this.RootVisual = new MainPage(battleId);
        }

        /// <summary>
        /// Handles the Exit event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Application_Exit(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the UnhandledException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ApplicationUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void Application_UnhandledException([NotNull] object sender,
            [NotNull] ApplicationUnhandledExceptionEventArgs e)
        {
            if (!Debugger.IsAttached)
            {
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDOM(e));
            }
        }

        /// <summary>
        /// Reports the error to DOM.
        /// </summary>
        /// <param name="e">The <see cref="ApplicationUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void ReportErrorToDOM([NotNull] ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                var errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}