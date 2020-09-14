﻿#nullable enable

namespace SettingsProject
{
    internal readonly struct SettingCondition
    {
        public SettingIdentity Source { get; }
        public object SourceValue { get; }
        public SettingIdentity Target { get; }

        public SettingCondition(SettingIdentity source, object sourceValue, SettingIdentity target)
        {
            Source = source;
            SourceValue = sourceValue;
            Target = target;
        }

        public override string ToString()
        {
            return $"{nameof(Source)}: {Source}, {nameof(SourceValue)}: {SourceValue}, {nameof(Target)}: {Target}";
        }
    }
}