using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace Facebook.Configuration
{
    /// <summary>Encapsulates both the in-memory representation of the server state and the logic required for loading and saving the file system copy.</summary>
    [XmlRoot(ElementName = "feed_template_bundles")]    
    public class FeedTemplateBundleIds : List<FeedTemplateBundleIdMap>
    {
        /// <summary>Parameterless constructor required for deserialization.</summary>
        internal FeedTemplateBundleIds() { }

        /// <summary>Initializes a <see cref="FeedTemplateBundleIds" /> instance for the speciied <paramref name="apiKey" />.</summary>
        /// <param name="apiKey">The API key of the application the feed template bundles are configured for.</param>
        public FeedTemplateBundleIds(String apiKey)
        {
            this.ApiKey = apiKey;
        }

        private static String GetCacheKey(String apiKey)
        {
            return "__FB__TEMPLATE_BUNDLE_IDS__" + apiKey;
        }

        private static String GetFilePath(String apiKey)
        {
            var fileName = String.Format("FeedTemplateBundleIds{0}.xml", apiKey);
            return HostingEnvironment.IsHosted ? HostingEnvironment.MapPath("~/App_Data/" + fileName) : Path.Combine(Environment.CurrentDirectory, fileName);
        }

        public String ApiKey { get; set; }

        private static FeedTemplateBundleIds _current;
        /// <summary>Gets the current instance of the <see cref="FeedTemplateBundleIds" />.</summary>
        /// <param name="apiKey">The API key of the application the feed template bundles are configured for.</param>
        /// <remarks>The <see cref="FeedTemplateBundleIds" /> is stored in the cache. If it does not yet exist there, it is loaded from disk.</remarks>
        public static FeedTemplateBundleIds GetCurrent(String apiKey)
        {
            if (HostingEnvironment.IsHosted)
            {
                var cacheKey = FeedTemplateBundleIds.GetCacheKey(apiKey);
                if (HttpRuntime.Cache[cacheKey] == null)
                {
                    HttpRuntime.Cache[cacheKey] = FeedTemplateBundleIds.LoadFromFile(apiKey);
                }
                return (FeedTemplateBundleIds)HttpRuntime.Cache[cacheKey];
            }
            else
            {
                if (FeedTemplateBundleIds._current == null)
                {
                    FeedTemplateBundleIds._current = FeedTemplateBundleIds.LoadFromFile(apiKey);
                }
                return FeedTemplateBundleIds._current;
            }
        }

        internal static void SetCurrent(String apiKey, FeedTemplateBundleIds FeedTemplateBundleIds)
        {
            if (HostingEnvironment.IsHosted) HttpRuntime.Cache["VIP__SERVER_STATE"] = FeedTemplateBundleIds;
            else FeedTemplateBundleIds._current = FeedTemplateBundleIds;
        }

        /// <summary>Loads the file system copy of the <see cref="FeedTemplateBundleIds" />.</summary>
        /// <returns>An instance of <see cref="FeedTemplateBundleIds" /> loaded with the settings from the file system copy.</returns>
        internal static FeedTemplateBundleIds LoadFromFile(String apiKey)
        {
            if (File.Exists(FeedTemplateBundleIds.GetFilePath(apiKey)))
            {
                var serializer = new XmlSerializer(typeof(FeedTemplateBundleIds));
                Object result;
                if (FeedTemplateBundleIds.InvokeOnFile(fileStream => { return serializer.Deserialize(fileStream); }, apiKey, out result))
                {
                    return (FeedTemplateBundleIds)result;
                }
                else
                {
                    return new FeedTemplateBundleIds(apiKey);
                }

            }
            else return new FeedTemplateBundleIds(apiKey);
        }

        /// <summary>Asynchronously saves the <see cref="FeedTemplateBundleIds" /> to disk.</summary>
        /// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
        internal Boolean Save()
        {
            var path = FeedTemplateBundleIds.GetFilePath(this.ApiKey);
            if (!File.Exists(path)) this.EnsurePath(new FileInfo(path).Directory.FullName);

            var serializer = new XmlSerializer(this.GetType());
            Object result;
            return FeedTemplateBundleIds.InvokeOnFile(fileStream =>
            {
                fileStream.SetLength(0);
                serializer.Serialize(fileStream, this);
                return null;
            }, this.ApiKey, out result);
        }

        private static Object _lock = new Object();
        /// <summary>Executes the specified <see cref="Func{FileStream, Object}" /> delegate on the filesystem copy of the <see cref="FeedTemplateBundleIds" />.
        /// The work done on the file is wrapped in a lock statement to ensure there are no locking collisions caused by attempting to save and load
        /// the file simultaneously from separate requests.
        /// </summary>
        /// <param name="func">The logic to be executed on the <see cref="FeedTemplateBundleIds" /> file.</param>
        /// <param name="apiKey">The API key of the application the feed template bundles are configured for.</param>
        /// <param name="result">When this method returns, the result of the invocation of <paramref name="func" />.</param>
        /// <returns>An object containing any result data returned by <paramref name="func" />.</returns>
        private static Boolean InvokeOnFile(Func<FileStream, Object> func, String apiKey, out Object result)
        {
            Boolean success = false;
            if (Monitor.TryEnter(FeedTemplateBundleIds._lock, 500))
            {
                var path = FeedTemplateBundleIds.GetFilePath(apiKey);
                try
                {
                    using (var fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                    {
                        result = func.Invoke(fileStream);
                    }
                    success = true;
                }
                catch
                {
                    try { File.Delete(path); }
                    catch { /* ignore */ }
                    result = null;
                }
                finally
                {
                    Monitor.Exit(FeedTemplateBundleIds._lock);
                }
            }
            else
            {
                result = null;
            }
            return success;
        }

        private void EnsurePath(String path)
        {
            if (!Directory.Exists(path))
            {
                var parent = new DirectoryInfo(path).Parent;
                this.EnsurePath(parent.FullName);
                Directory.CreateDirectory(path);
            }
        }
    }
}
