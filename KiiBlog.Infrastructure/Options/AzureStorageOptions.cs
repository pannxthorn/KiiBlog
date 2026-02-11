using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Infrastructure.Options
{
    public class AzureStorageOptions
    {
        public const string SectionName = "AzureStorage";

        public string ConnectionString { get; set; }
        public string StorageUrl { get; set; }
        public string CdnUrl { get; set; }
        public string PrivateContainer { get; set; }
        public string PublicContainer { get; set; }
        public bool UseCdn { get; set; }
    }
}
