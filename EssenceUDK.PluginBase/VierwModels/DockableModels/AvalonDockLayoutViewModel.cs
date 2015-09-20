﻿#region

using System.IO;
using System.Windows.Input;
using AvalonDock;
using AvalonDock.Layout.Serialization;
using EssenceUDK.PluginBase.Commands;

#endregion

namespace EssenceUDK.PluginBase.VierwModels.DockableModels
{

    /// <summary>
    ///     Class implements a viewmodel to support the
    ///     <seealso cref="AvalonDockLayoutSerializer" />
    ///     attached behavior which is used to implement
    ///     load/save of layout information on application
    ///     start and shut-down.
    /// </summary>
    public class AvalonDockLayoutViewModel
    {
        #region fields

        private InnerRelayCommand mLoadLayoutCommand;
        private InnerRelayCommand mSaveLayoutCommand;

        #endregion fields

        #region command properties

        /// <summary>
        ///     Implement a command to load the layout of an AvalonDock-DockingManager instance.
        ///     This layout defines the position and shape of each document and tool window
        ///     displayed in the application.
        ///     Parameter:
        ///     The command expects a reference to a <seealso cref="DockingManager" /> instance to
        ///     work correctly. Not supplying that reference results in not loading a layout (silent return).
        /// </summary>
        public ICommand LoadLayoutCommand
        {
            get
            {
                return mLoadLayoutCommand ?? (mLoadLayoutCommand = new InnerRelayCommand(p =>
                {
                    var docManager = p as DockingManager;

                    if (docManager == null)
                        return;

                    LoadDockingManagerLayout(docManager);
                }));
            }
        }

        /// <summary>
        ///     Implements a command to save the layout of an AvalonDock-DockingManager instance.
        ///     This layout defines the position and shape of each document and tool window
        ///     displayed in the application.
        ///     Parameter:
        ///     The command expects a reference to a <seealso cref="string" /> instance to
        ///     work correctly. The string is supposed to contain the XML layout persisted
        ///     from the DockingManager instance. Not supplying that reference to the string
        ///     results in not saving a layout (silent return).
        /// </summary>
        public ICommand SaveLayoutCommand
        {
            get
            {
                if (mSaveLayoutCommand == null)
                {
                    mSaveLayoutCommand = new InnerRelayCommand(p =>
                    {
                        var xmlLayout = p as string;

                        if (xmlLayout == null)
                            return;

                        SaveDockingManagerLayout(xmlLayout);
                    });
                }

                return mSaveLayoutCommand;
            }
        }

        #endregion command properties

        #region methods

        #region LoadLayout

        /// <summary>
        ///     Loads the layout of a particular docking manager instance from persistence
        ///     and checks whether a file should really be reloaded (some files may no longer
        ///     be available).
        /// </summary>
        /// <param name="docManager"></param>
        private void LoadDockingManagerLayout(DockingManager docManager)
        {
            var layoutFileName = "layout.xml";

            if (File.Exists(layoutFileName) == false)
                return;

            var layoutSerializer = new XmlLayoutSerializer(docManager);

            layoutSerializer.LayoutSerializationCallback += (s, args) =>
            {
                // This can happen if the previous session was loading a file
                // but was unable to initialize the view ...
                if (args.Model.ContentId == null)
                {
                    return;
                }

                ReloadContentOnStartUp(args);
            };

            layoutSerializer.Deserialize(layoutFileName);
        }

        private static void ReloadContentOnStartUp(LayoutSerializationCallbackEventArgs args)
        {
            var sId = args.Model.ContentId;

            // Empty Ids are invalid but possible if aaplication is closed with File>New without edits.
            if (string.IsNullOrWhiteSpace(sId))
            {
                //args.Cancel = true;
            }

            //if (args.Model.ContentId == FileStatsViewModel.ToolContentId)
            //    args.Content = Workspace.This.FileStats;
            //else
            //{
            //    args.Content = AvalonDockLayoutViewModel.ReloadDocument(args.Model.ContentId);

            //    if (args.Content == null)
            //        args.Cancel = true;
            //}
        }

        private static object ReloadDocument(string path)
        {
            object ret = null;

            if (!string.IsNullOrWhiteSpace(path))
            {
                switch (path)
                {
                    /***
                              case StartPageViewModel.StartPageContentId: // Re-create start page content
                                if (Workspace.This.GetStartPage(false) == null)
                                {
                                  ret = Workspace.This.GetStartPage(true);
                                }
                                break;
                    ***/
                    default:
                        // Re-create text document
                        //ret = Workspace.This.Open(path);
                        break;
                }
            }

            return ret;
        }

        #endregion LoadLayout

        #region SaveLayout

        private void SaveDockingManagerLayout(string xmlLayout)
        {
            // Create XML Layout file on close application (for re-load on application re-start)
            if (xmlLayout == null)
                return;

            var fileName = "layout.xml";

            File.WriteAllText(fileName, xmlLayout);
        }

        #endregion SaveLayout

        #endregion methods
    }

}