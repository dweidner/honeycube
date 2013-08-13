﻿#region Using Statements

using System;
using System.Windows.Forms;
using HoneyCube.Editor.Commands;
using HoneyCube.Editor.Presenter;
using HoneyCube.Editor.Services;
using HoneyCube.Editor.Views;
using System.Diagnostics;

#endregion

namespace HoneyCube.Editor
{
    /// <summary>
    /// The DefaultCommandMap contains all methods to bind certain commands to 
    /// a set of identifiers or key combinations.
    /// </summary>
    public class DefaultCommandMap : CommandMap
    {
        /// <summary>
        /// Public constructor. Creates a default set of commands that can be
        /// triggered throughout the entire application.
        /// </summary>
        /// <param name="window">The application window maintaining most of the control elements.</param>
        /// <param name="appLog">The logging element.</param>
        public DefaultCommandMap(IAppWindow window, IAppLogPresenter appLog)
        {
            #region Menu: File

            // MenuFileExit
            If(Keys.Control | Keys.Q)
                .ThenExecute(new ActionCommand(() => {
                    window.Presenter.CloseRequested();
                }));

            #endregion

            #region Menu: View

            // MenuViewShowLog
            If(Keys.Control | Keys.L)
                .ThenExecute(new ActionCommand(appLog.ShowClicked));

            If("MenuViewSidebarSidebar")
                .ThenExecute(new ActionCommand(window.ToggleSidebar));

            If("MenuViewSidebarProjectTree")
                .ThenExecute(new ActionCommand(window.ToggleProjectTree));

            If("MenuViewSidebarInspector")
                .ThenExecute(new ActionCommand(window.ToggleInspector));            

            #endregion

            #region Menu: Help

            If("MenuHelpGithub")
                .ThenExecute(new ActionCommand(() => {
                    Process.Start(L10n.LookUpLocalizedString("GithubUrl"));
                }));

            #endregion

            #region Test Logging

            // Some random log messages
            string[] logMessages = new string[] {
                "Crowding all sail the Pequod pressed after them; the harpooneers handling their weapons, and loudly cheering from the heads of their yet suspended boats.",
                "Now, Bildad, like Peleg, and indeed many other Nantucketers, was a Quaker, the island having been originally settled by that sect.",
                "During the most violent shocks of the Typhoon, the man at the Pequod's jaw-bone tiller had several times been reelingly hurled to the deck by its spasmodic motions",
                "More Moby Dick anyone? Buy the book!"
            };

            If(Keys.Control | Keys.T)
                .ThenExecute(new ActionCommand(() => {
                    AppLog.For("General").Add(
                        logMessages[new Random().Next(0, 4)],
                        new Random().Next(0, 2) == 0 ? LogMessageType.Warning : LogMessageType.Error);
                }));

            #endregion
        }
    }
}
