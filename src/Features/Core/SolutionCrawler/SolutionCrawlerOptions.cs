// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.Options;

namespace Microsoft.CodeAnalysis.SolutionCrawler
{
    internal static class SolutionCrawlerOptions
    {
        [ExportOption]
        public static readonly Option<bool> SolutionCrawler = new Option<bool>("FeatureManager/Components", "Solution Crawler", defaultValue: true);

        [ExportOption]
        public static readonly Option<int> ActiveFileWorkerBackOffTimeSpanInMS = new Option<int>("SolutionCrawler", "Active file worker backoff timespan in ms", defaultValue: 200);

        [ExportOption]
        public static readonly Option<int> AllFilesWorkerBackOffTimeSpanInMS = new Option<int>("SolutionCrawler", "All files worker backoff timespan in ms", defaultValue: 1500);

        [ExportOption]
        public static readonly Option<int> EntireProjectWorkerBackOffTimeSpanInMS = new Option<int>("SolutionCrawler", "Entire project analysis worker backoff timespan in ms", defaultValue: 5000);

        [ExportOption]
        public static readonly Option<int> SemanticChangeBackOffTimeSpanInMS = new Option<int>("SolutionCrawler", "Semantic change backoff timespan in ms", defaultValue: 100);

        [ExportOption]
        public static readonly Option<int> ProjectPropagationBackOffTimeSpanInMS = new Option<int>("SolutionCrawler", "Project propagation backoff timespan in ms", defaultValue: 500);

        [ExportOption]
        public static readonly Option<int> PreviewBackOffTimeSpanInMS = new Option<int>("SolutionCrawler", "Preview backoff timespan in ms", defaultValue: 500);
    }
}
