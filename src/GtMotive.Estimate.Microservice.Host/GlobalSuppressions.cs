﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Security Hotspot", "S4792:Configuring loggers is security-sensitive", Justification = "Is already been controlled in the code")]
[assembly: SuppressMessage("Security Hotspot", "S4507:Make sure this debug feature is deactivated before delivering the code in production.", Justification = "Is already been controlled in the code")]
