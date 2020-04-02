/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Forge Partner Development
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Inventor;
using Autodesk.Forge.DesignAutomation.Inventor.Utils;

namespace CreateBitmapsBundlePlugin
{
    [ComVisible(true)]
    public class SampleAutomation
    {
        private readonly InventorServer inventorApplication;

        public SampleAutomation(InventorServer inventorApp)
        {
            inventorApplication = inventorApp;
        }

        public void Run(Document doc)
        {
            LogTrace("Run called with {0}", doc.DisplayName);

            RunWithArguments(doc, null);
        }

        public void RunWithArguments(Document doc, NameValueMap map)
        {
            LogTrace("Processing " + doc.FullFileName);

            dynamic invDoc = doc;
            ViewOrientationTypeEnum[] orientations = {
                ViewOrientationTypeEnum.kIsoTopRightViewOrientation,
                ViewOrientationTypeEnum.kIsoTopLeftViewOrientation,
                ViewOrientationTypeEnum.kBackViewOrientation
            };
            var imagesFolder = System.IO.Path.Combine(
                System.IO.Directory.GetCurrentDirectory(), "OutputImages");
            foreach (var orientation in orientations)
            {
                var fileName = orientation.ToString() + ".png";
                var filePath = System.IO.Path.Combine(imagesFolder, fileName);
                Camera cam = inventorApplication.TransientObjects.CreateCamera();
                cam.SceneObject = invDoc.ComponentDefinition;
                cam.ViewOrientationType = orientation;
                cam.Fit();
                cam.ApplyWithoutTransition();
                cam.SaveAsBitmap(filePath, 200, 200, Type.Missing, Type.Missing);
                LogTrace($"Saved image as {filePath}");
            }
        }

        #region Logging utilities

        /// <summary>
        /// Log message with 'trace' log level.
        /// </summary>
        private static void LogTrace(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

        /// <summary>
        /// Log message with 'trace' log level.
        /// </summary>
        private static void LogTrace(string message)
        {
            Trace.TraceInformation(message);
        }

        /// <summary>
        /// Log message with 'error' log level.
        /// </summary>
        private static void LogError(string format, params object[] args)
        {
            Trace.TraceError(format, args);
        }

        /// <summary>
        /// Log message with 'error' log level.
        /// </summary>
        private static void LogError(string message)
        {
            Trace.TraceError(message);
        }

        #endregion
    }
}