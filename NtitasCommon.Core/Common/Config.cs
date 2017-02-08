using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtitasCommon.Core.Common
{
    public interface IConfig
    {
        /// <summary>
        /// Retrieves the default page size count
        /// </summary>
        int DefaultPageSize { get; }
    }

    public class Config : IConfig
    {
        /// <summary>
        /// Retrieves the default page size count
        /// </summary>
        public int DefaultPageSize
        {
            get
            {
                int ret;
                if (int.TryParse(ConfigurationManager.AppSettings.Get("DefaultPageSize"), out ret))
                    return ret;
                return 25;
            }
        }
    }
}
