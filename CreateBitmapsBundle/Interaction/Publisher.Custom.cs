using System.Collections.Generic;
using Autodesk.Forge.DesignAutomation.Model;

namespace Interaction
{
    /// <summary>
    /// Customizable part of Publisher class.
    /// </summary>
    internal partial class Publisher
    {
        /// <summary>
        /// Constants.
        /// </summary>
        private static class Constants
        {
            private const int EngineVersion = 23;
            public static readonly string Engine = $"Autodesk.Inventor+{EngineVersion}";

            public const string Description = "Generates images for the Inventor model";

            internal static class Bundle
            {
                public static readonly string Id = "CreateBitmapsBundle";
                public const string Label = "alpha";

                public static readonly AppBundle Definition = new AppBundle
                {
                    Engine = Engine,
                    Id = Id,
                    Description = Description
                };
            }

            internal static class Activity
            {
                public static readonly string Id = Bundle.Id;
                public const string Label = Bundle.Label;
            }

            internal static class Parameters
            {
                public const string InventorDoc = nameof(InventorDoc);
                public const string OutputImages = nameof(OutputImages);
            }
        }


        /// <summary>
        /// Get command line for activity.
        /// </summary>
        private static List<string> GetActivityCommandLine()
        {
            return new List<string> { $"$(engine.path)\\InventorCoreConsole.exe /al $(appbundles[{Constants.Activity.Id}].path) /i $(args[{Constants.Parameters.InventorDoc}].path)" };
        }

        /// <summary>
        /// Get activity parameters.
        /// </summary>
        private static Dictionary<string, Parameter> GetActivityParams()
        {
            return new Dictionary<string, Parameter>
                    {
                        {
                            Constants.Parameters.InventorDoc,
                            new Parameter
                            {
                                Verb = Verb.Get,
                                Description = "IPT file to process"
                            }
                        },
                        {
                            Constants.Parameters.OutputImages,
                            new Parameter
                            {
                                Verb = Verb.Put,
                                Zip = true,
                                LocalName = "OutputImages",
                                Description = "Resulting images",
                                Ondemand = false,
                                Required = true
                            }
                        }
                    };
        }

        /// <summary>
        /// Get arguments for workitem.
        /// </summary>
        private static Dictionary<string, IArgument> GetWorkItemArgs()
        {
            // TODO: update the URLs below with real values
            return new Dictionary<string, IArgument>
                    {
                        {
                            Constants.Parameters.InventorDoc,
                            new XrefTreeArgument
                            {
                                Url = "https://developer.api.autodesk.com/oss/v2/signedresources/446b13c0-f5ae-4c2e-8212-fff87ec5b19e?region=US"
                            }
                        },
                        {
                            Constants.Parameters.OutputImages,
                            new XrefTreeArgument
                            {
                                Verb = Verb.Put,
                                Url = "https://developer.api.autodesk.com/oss/v2/signedresources/c220262e-0a2b-42a0-bb98-9ab4cb2a640b?region=US"
                            }
                        }
                    };
        }
    }
}
