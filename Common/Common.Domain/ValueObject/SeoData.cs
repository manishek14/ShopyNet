using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.ValueObject
{
    public class SeoData
    {
        private SeoData() { }

        public SeoData(string metaData, string metaDescription, string metaKeywords, bool indexPage, string canonicalUrl, string schema)
        {
            MetaData = metaData;
            MetaDescription = metaDescription;
            MetaKeywords = metaKeywords;
            IndexPage = indexPage;
            CanonicalUrl = canonicalUrl;
            Schema = schema;
        }

        public static SeoData CreateEmpty()
        {
            return new SeoData();
        }

        public string MetaData { get; private set; }
        public string MetaDescription { get; private set; }
        public string MetaKeywords { get; private set; }
        public bool IndexPage { get; private set; }
        public string CanonicalUrl { get; private set; }
        public string Schema { get; private set; }
    }
}
