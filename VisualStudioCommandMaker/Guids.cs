// Guids.cs
// MUST match guids.h
using System;

namespace Company.VisualStudioCommandMaker
{
    static class GuidList
    {
        public const string guidVisualStudioCommandMakerPkgString = "7939c262-03bc-4bbb-80ea-9030f1c8ee7d";
        public const string guidVisualStudioCommandMakerCmdSetString = "e3248a3e-af93-4e81-973a-f7006c6d5962";

        public static readonly Guid guidVisualStudioCommandMakerCmdSet = new Guid(guidVisualStudioCommandMakerCmdSetString);
    };
}