// <copyright file="CloudVariables.cs" company="Jan Ivar Z. Carlsen, Sindri Jóelsson">
// Copyright (c) 2016 Jan Ivar Z. Carlsen, Sindri Jóelsson. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CloudOnce
{
    using CloudPrefs;

    /// <summary>
    /// Provides access to cloud variables registered via the CloudOnce Editor.
    /// This file was automatically generated by CloudOnce. Do not edit.
    /// </summary>
    public static class CloudVariables
    {
        private static readonly CloudInt s_score = new CloudInt("Score", PersistenceType.Highest, 0);

        public static int Score
        {
            get { return s_score.Value; }
            set { s_score.Value = value; }
        }

        private static readonly CloudCurrencyInt s_varVersion = new CloudCurrencyInt("varVersion", 0, false);

        public static int varVersion
        {
            get { return s_varVersion.Value; }
            set { s_varVersion.Value = value; }
        }

        private static readonly CloudCurrencyInt s_muPoints = new CloudCurrencyInt("muPoints", 50, false);

        public static int muPoints
        {
            get { return s_muPoints.Value; }
            set { s_muPoints.Value = value; }
        }
    }
}