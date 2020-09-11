﻿using System;
using System.Collections.Generic;
using System.Windows.Input;

#nullable enable

namespace SettingsProject
{
    internal static class LinkActionCommandHandler
    {
        private static readonly Dictionary<string, Action<IReadOnlyDictionary<string, string>>> s_commandRegistry = new Dictionary<string, Action<IReadOnlyDictionary<string, string>>>
        {
            // TODO import these via MEF imports
            { "ManageLaunchProfiles", _ => new LaunchProfilesWindow().Show() }
        };

        public static ICommand ActionCommand { get; } = new DelegateCommand<Setting>(setting => Handle(setting.Metadata.EditorMetadata));

        private static void Handle(IReadOnlyDictionary<string, string> editorMetadata)
        {
            // TODO safe dictionary lookups with more helpful error messages

            var action = editorMetadata["Action"];

            switch (action)
            {
                case "Command":
                    var command = editorMetadata["Command"];
                    var handler = s_commandRegistry[command];
                    handler(editorMetadata);
                    break;

                case "URL":
                    // TODO launch browser
                    break;
            }
        }
    }
}