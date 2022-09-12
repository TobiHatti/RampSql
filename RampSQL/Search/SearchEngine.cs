using Newtonsoft.Json;
using RampSQL.Query;
using RampSQL.Schema;
using System;
using System.Collections.Generic;

namespace RampSQL.Search
{
    public interface ISearchResultItem
    {
        int UID { get; set; }
    }

    public interface ISearchResultHitItem : ISearchResultItem
    {
        string HitMatch { get; set; }
        string HitValue { get; set; }
    }

    public enum DuplicateResultRule
    {
        KeepAll,
        KeepFirst,
        KeepLast
    }

    public class SearchEngine
    {
        private class SearchColumnEntry
        {
            public RampColumn Column { get; set; }
            public string Label { get; set; } = string.Empty;
            public LikeWildcard MatchType { get; set; } = LikeWildcard.MatchAny;
        }

        public static readonly string HitMatch = "SEHitMatch";
        public static readonly string HitLabel = "SEHitLabel";

        private string fromQuery = string.Empty;
        private string[] searchTerms = null;
        private RampColumn[] resultColumns = null;
        private List<SearchColumnEntry> searchColumns = new List<SearchColumnEntry>();
        private DuplicateResultRule resultRule;
        private List<ISearchResultItem> results = new List<ISearchResultItem>();


        public SearchEngine(IQuerySection rampQuery, string searchTerm, RampColumn[] resultColumns, DuplicateResultRule resultRule = DuplicateResultRule.KeepFirst) : this(rampQuery.ToString(), searchTerm, resultColumns, resultRule)
        { }
        public SearchEngine(RampTable table, string searchTerm, RampColumn[] resultColumns, DuplicateResultRule resultRule = DuplicateResultRule.KeepFirst) : this(table.ToString(), searchTerm, resultColumns, resultRule)
        { }

        public SearchEngine(string fromQuery, string searchTerm, RampColumn[] resultColumns, DuplicateResultRule resultRule = DuplicateResultRule.KeepFirst)
        {
            this.fromQuery = fromQuery;
            this.searchTerms = searchTerm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < searchTerms.Length; i++) searchTerms[i] = searchTerms[i].Trim();
            this.resultColumns = resultColumns;
            this.resultRule = resultRule;
        }

        public SearchEngine AddSearchField(RampColumn dbColumn, string label, LikeWildcard matchType = LikeWildcard.MatchAny)
        {
            searchColumns.Add(new SearchColumnEntry()
            {
                Column = dbColumn,
                Label = label,
                MatchType = matchType
            });

            return this;
        }

        public IQuerySection GetRampQuery()
        {
            IQuerySection mainQuery = new QueryEngine().Union(UnionType.UnionAll);
            foreach (SearchColumnEntry entry in searchColumns)
            {
                foreach (string querySegment in searchTerms)
                {
                    (mainQuery as UnionQuery).SubQuery(new QueryEngine()
                        .SelectFrom(fromQuery)
                        .Columns(resultColumns)
                        .Value(entry.Column, HitMatch)
                        .Value($@"""{entry.Label}""", HitLabel)
                        .Where.Like(entry.Column, querySegment, entry.MatchType)
                    );
                }
            }
            return mainQuery;
        }

        public string GetQuery() => GetRampQuery().ToString();
        public object[] GetParameters() => GetRampQuery().GetParameters();

        public void AddResult(ISearchResultItem item)
        {
            results.Add(item);
        }

        public ISearchResultItem[] GetFilteredResults()
        {
            if (resultRule == DuplicateResultRule.KeepAll)
                return results.ToArray();

            Dictionary<int, int> checkDict = new Dictionary<int, int>();

            for (int i = 0; i < results.Count; i++)
            {
                if (checkDict.ContainsKey(results[i].UID))
                {
                    switch (resultRule)
                    {
                        case DuplicateResultRule.KeepFirst:

                            break;
                        case DuplicateResultRule.KeepLast:
                            checkDict[results[i].UID] = i;
                            break;
                    }
                }
                else checkDict.Add(results[i].UID, i);
            }

            List<ISearchResultItem> filteredItems = new List<ISearchResultItem>();
            foreach (KeyValuePair<int, int> entry in checkDict)
            {
                filteredItems.Add(results[entry.Value]);
            }

            return filteredItems.ToArray();
        }

        public string GetFilteredResultJSON()
        {
            return JsonConvert.SerializeObject(GetFilteredResults());
        }
    }
}

