// This software code is made available "AS IS" without warranties of any        
// kind.  You may copy, display, modify and redistribute the software            
// code either by itself or as incorporated into your code; provided that        
// you do not remove any proprietary notices.  Your use of this software         
// code is at your own risk and you waive any claim against Amazon               
// Digital Services, Inc. or its affiliates with respect to your use of          
// this software code. (c) 2006-2007 Amazon Digital Services, Inc. or its             
// affiliates.          


using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace AmazonS3
{
    /// An interface into the S3 system.  It is initially configured with
    /// authentication and connection parameters and exposes methods to access and
    /// manipulate S3 data.
    public class AWSAuthConnection
    {
        private string awsAccessKeyId;
        private string awsSecretAccessKey;
        private bool isSecure;
        private string server;
        private int port;
        private CallingFormat callingFormat;

        public AWSAuthConnection( string awsAccessKeyId, string awsSecretAccessKey )
            : this( awsAccessKeyId, awsSecretAccessKey, true, CallingFormat.SUBDOMAIN )
        {
        }

        public AWSAuthConnection(string awsAccessKeyId, string awsSecretAccessKey, CallingFormat format )
            : this(awsAccessKeyId, awsSecretAccessKey, true, format )
        {
        }

        public AWSAuthConnection(string awsAccessKeyId, string awsSecretAccessKey, bool isSecure)
            : this( awsAccessKeyId, awsSecretAccessKey, isSecure, Utils.Host, CallingFormat.SUBDOMAIN )
        {
        }

        public AWSAuthConnection(string awsAccessKeyId, string awsSecretAccessKey, bool isSecure, CallingFormat format) 
            : this( awsAccessKeyId, awsSecretAccessKey, isSecure, Utils.Host, format )
        {
        }

        public AWSAuthConnection( string awsAccessKeyId, string awsSecretAccessKey, bool isSecure,
                                  string server, CallingFormat format ) 
            : this(awsAccessKeyId, awsSecretAccessKey, isSecure, server,
                   isSecure ? Utils.SecurePort : Utils.InsecurePort, format )
        {
        }

        public AWSAuthConnection( string awsAccessKeyId, string awsSecretAccessKey, bool isSecure,
                                  string server )
            : this( awsAccessKeyId, awsSecretAccessKey, isSecure, server,
                    isSecure ? Utils.SecurePort : Utils.InsecurePort, CallingFormat.SUBDOMAIN )
        {
        }

        public AWSAuthConnection( string awsAccessKeyId, string awsSecretAccessKey, bool isSecure,
                                  string server, int port) 
            : this( awsAccessKeyId, awsSecretAccessKey, isSecure, server,
                    port, CallingFormat.SUBDOMAIN )
        {
        }

        public AWSAuthConnection( string awsAccessKeyId, string awsSecretAccessKey, bool isSecure,
                                  string server, int port, CallingFormat format )
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.isSecure = isSecure;
            this.server = server;
            this.port = port;
            this.callingFormat = format;
        }

        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="bucket">The name of the bucket to create</param>
        /// <param name="location">Location-constraint for bucket (can be null)</param>
        /// <param name="headers">A Map of string to string representing the headers to pass (can be null)</param>
        public Response createBucket( string bucket, String location, SortedList headers )
        {
            if (!validateBucketName( bucket ))
                throw new ArgumentException( "Invalid Bucket Name: " + bucket );

            string body;
            if (location == null)
                body = "";
            else if (Location.EU.Equals(location))
                body = "<CreateBucketConstraint><LocationConstraint>" + 
                        location + 
                       "</LocationConstraint></CreateBucketConstraint>";
            else
                throw new ArgumentException( "Invalid Location: "+location );

            return new Response(makeRequest("PUT", bucket, "", null, headers, new S3Object(body, null), 5000)); // 2sec timeout...
        }

        /// <summary>
        /// Check if the specified bucket exists (via a HEAD request).
        /// </summary>
        /// <param name="bucket">The name of the bucket to list</param>
        public bool checkBucketExists(String bucket)
        {
            try
            {
                WebResponse response = makeRequest("HEAD", bucket, "", null, null, null, 1000);
                response.Close();
                return true;
            }
            catch (WebException ex)
            {
                HttpWebResponse response = ex.Response as HttpWebResponse;
                if (response != null && 
                    response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
        }

        /// <summary>
        /// Lists the contents of a bucket.
        /// </summary>
        /// <param name="bucket">The name of the bucket to list</param>
        /// <param name="prefix">All returned keys will start with this string (can be null)</param>
        /// <param name="marker">All returned keys will be lexographically greater than this string (can be null)</param>
        /// <param name="maxKeys">The maximum number of keys to return (can be 0)</param>
        /// <param name="headers">A Map of string to string representing HTTP headers to pass.</param>
        public ListBucketResponse listBucket( string bucket, string prefix, string marker,
                                              int maxKeys, SortedList headers ) {
            return listBucket( bucket, prefix, marker, maxKeys, null, headers );
        }

        /// <summary>
        /// Lists the contents of a bucket.
        /// </summary>
        /// <param name="bucket">The name of the bucket to list</param>
        /// <param name="prefix">All returned keys will start with this string (can be null)</param>
        /// <param name="marker">All returned keys will be lexographically greater than this string (can be null)</param>
        /// <param name="maxKeys">The maximum number of keys to return (can be 0)</param>
        /// <param name="headers">A Map of string to string representing HTTP headers to pass.</param>
        /// <param name="delimiter">Keys that contain a string between the prefix and the first
        /// occurrence of the delimiter will be rolled up into a single element.</param>
        public ListBucketResponse listBucket( string bucket, string prefix, string marker,
                                              int maxKeys, string delimiter, SortedList headers ) {
            SortedList query = Utils.queryForListOptions(prefix, marker, maxKeys, delimiter);
            return new ListBucketResponse( makeRequest( "GET", bucket, "", query, headers, null, 5000 ) );
        }

        /// <summary>
        /// Deletes an empty Bucket.
        /// </summary>
        /// <param name="bucket">The name of the bucket to delete</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        /// <returns></returns>
        public Response deleteBucket( string bucket, SortedList headers )
        {
            return new Response( makeRequest( "DELETE", bucket, "", null, headers, null, 2000 ) );
        }

        /// <summary>
        /// Writes an object to S3.
        /// </summary>
        /// <param name="bucket">The name of the bucket to which the object will be added.</param>
        /// <param name="key">The name of the key to use</param>
        /// <param name="obj">An S3Object containing the data to write.</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        public Response put( string bucket, string key, S3Object obj, SortedList headers )
        {
			return new Response(makeRequest("PUT", bucket, key, null, headers, obj, 3600000)); // 60 min timeout
        }

        /// <summary>
        /// Reads an object from S3
        /// </summary>
        /// <param name="bucket">The name of the bucket where the object lives</param>
        /// <param name="key">The name of the key to use</param>
        /// <param name="headers">A Map of string to string representing the HTTP headers to pass (can be null)</param>
        public GetResponse get( string bucket, string key, SortedList headers )
        {
			return new GetResponse(makeRequest("GET", bucket, key, null, headers, null, 3600000)); // 60 min timeout
        }

        /// <summary>
        /// Delete an object from S3.
        /// </summary>
        /// <param name="bucket">The name of the bucket where the object lives.</param>
        /// <param name="key">The name of the key to use.</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        /// <returns></returns>
        public Response delete( string bucket, string key, SortedList headers )
        {
            return new Response(makeRequest("DELETE", bucket, key, null, headers, null, 2000));
        }

        public LocationResponse getBucketLocation(string bucket)
        {
            return new LocationResponse(makeRequest("GET", bucket, "", newSingleParam("location"), null, null, 1000));
        }

        /// <summary>
        /// Get the logging xml document for a given bucket
        /// </summary>
        /// <param name="bucket">The name of the bucket</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        public GetResponse getBucketLogging(string bucket, SortedList headers)
        {
            return new GetResponse(makeRequest("GET", bucket, "", newSingleParam("logging"), headers, null, 1000));
        }

        /// <summary>
        /// Write a new logging xml document for a given bucket
        /// </summary>
        /// <param name="bucket">The name of the bucket to enable/disable logging on</param>
        /// <param name="loggingXMLDoc">The xml representation of the logging configuration as a String.</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        public Response putBucketLogging(string bucket, string loggingXMLDoc, SortedList headers)
        {
            return new Response(makeRequest("PUT", bucket, "", newSingleParam("logging"), headers, new S3Object(loggingXMLDoc, null), 1000));
        }

        /// <summary>
        /// Get the ACL for a given bucket.
        /// </summary>
        /// <param name="bucket">The the bucket to get the ACL from.</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        public GetResponse getBucketACL(string bucket, SortedList headers)
        {
            return getACL(bucket, null, headers);
        }

        /// <summary>
        /// Get the ACL for a given object
        /// </summary>
        /// <param name="bucket">The name of the bucket where the object lives</param>
        /// <param name="key">The name of the key to use.</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        public GetResponse getACL( string bucket, string key, SortedList headers )
        {
            if (key == null)
                key = "";
            return new GetResponse(makeRequest("GET", bucket, key, newSingleParam("acl"), headers, null, 1000));
        }

        /// <summary>
        /// Write a new ACL for a given bucket
        /// </summary>
        /// <param name="bucket">The name of the bucket to change the ACL.</param>
        /// <param name="aclXMLDoc">An XML representation of the ACL as a string.</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        public Response putBucketACL(string bucket, string aclXMLDoc, SortedList headers)
        {
            return putACL(bucket, null, aclXMLDoc, headers);
        }

        /// <summary>
        /// Write a new ACL for a given object
        /// </summary>
        /// <param name="bucket">The name of the bucket where the object lives or the
        /// name of the bucket to change the ACL if key is null.</param>
        /// <param name="key">The name of the key to use; can be null.</param>
        /// <param name="aclXMLDoc">An XML representation of the ACL as a string.</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        public Response putACL(string bucket, string key, string aclXMLDoc, SortedList headers)
        {
            if ( key == null )
                key = "";
            return new Response(makeRequest("PUT", bucket, key, newSingleParam("acl"), headers, new S3Object(aclXMLDoc, null), 1000));
        }

        /// <summary>
        /// List all the buckets created by this account.
        /// </summary>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        public ListAllMyBucketsResponse listAllMyBuckets( SortedList headers )
        {
            return new ListAllMyBucketsResponse(makeRequest("GET", "", "", null, headers, null, 5000));
        }

        /// <summary>
        /// Make a new WebRequest
        /// </summary>
        /// <param name="method">The HTTP method to use (GET, PUT, DELETE)</param>
        /// <param name="bucket">The bucket name for this request</param>
        /// <param name="key">The key this request is for</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        /// <param name="obj">S3Object that is to be written (can be null).</param>
		/// <param name="timeout">Timeout in ms. Set to zero for no timeout.</param>
        private WebResponse makeRequest(string method, string bucket, string key, 
                                        SortedList query, SortedList headers, 
                                        S3Object obj, int timeout)
        {
            if (!String.IsNullOrEmpty(key))
                key = Utils.urlEncode(key);
            String url = buildUrl(bucket, key, query);

            int redirectCount = 0;
            for (; ; )
            {
                // prep request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

				req.KeepAlive = false;

				//??????????????????????????????????
				//Since we fixed the ".Close()" problem, I don't think this is needed...
				//if (timeout > 0)
				//{
				//    req.ReadWriteTimeout = timeout;
				//    req.Timeout = timeout;
				//}
				//??????????????????????????????????

				//default timeout is 100 sec... this is not good for putting ~100MB files!
				if (timeout > 0)
					req.Timeout = timeout;

                req.Method = method;
                // we handle redirects manually
                req.AllowAutoRedirect = false;
                // we already buffer everything
                req.AllowWriteStreamBuffering = false;
                // if we are sending >1kb data, ask for 100-Continue
                if (obj != null && obj.Bytes.Length > 1024)
                    req.ServicePoint.Expect100Continue = true;

                addHeaders(req, headers);
                if (obj != null)
                    addMetadataHeaders(req, obj.Metadata);
                addAuthHeader(req, bucket, key, query);

                if (obj != null)
                {
                    // Work around an HttpWebRequest bug where it will
                    // send the request body even if the server does *not* 
                    // send 100 continue
                    req.KeepAlive = false;
                    // Write actual data
                    byte[] data = obj.Bytes;
                    req.ContentLength = data.Length;
                    Stream requestStream = req.GetRequestStream();
					MemoryStream dataStream = new MemoryStream(data);
					BufferedTransfer(dataStream, requestStream);
                    //requestStream.Write(data, 0, data.Length);
                    requestStream.Close();
                }

				// execute request
                HttpWebResponse response;
				try
				{
					response = (HttpWebResponse)req.GetResponse();
					if (!isRedirect(response))
						return response;
					// retry against redirected url
					url = response.Headers["Location"];
					response.Close();
					if (url == null || url.Length == 0)
						throw new WebException("Redirect without Location header, may need to change calling format.", WebExceptionStatus.ProtocolError);
				}
				catch (WebException ex)
				{
					if (ex.Response == null)
						throw;
					string msg = Utils.slurpInputStreamAsString(ex.Response.GetResponseStream());
					throw new WebException(msg, ex, ex.Status, ex.Response);
				}

                redirectCount++;
                if (redirectCount > 10)
                    throw new WebException("Too many redirects.");
            }
        }

		public static void BufferedTransfer(Stream input, Stream output)
		{
			int bufferSize = 4096 * 2;    // default to 4K, may want to use 8K instead

			byte[] bytes = new byte[bufferSize];

			int read = 0;
			while ((read = input.Read(bytes, 0, bytes.Length)) != 0)
			{
				output.Write(bytes, 0, read);
			}
			output.Flush();
		}

        private string buildUrl(string bucket, string key, SortedList query)
        {
            StringBuilder url = new StringBuilder();
            url.Append(isSecure ? "https://" : "http://");
            url.Append(Utils.buildUrlBase(server, port, bucket, callingFormat));
            if (key != null && key.Length != 0)
                url.Append(key);
            // build the query string parameter
            if (query != null && query.Count != 0)
                url.Append(Utils.convertQueryListToQueryString(query));
            return url.ToString();
        }

        static private Boolean isRedirect(WebResponse response)
        {
            HttpWebResponse httpResp = response as HttpWebResponse;
            if (httpResp != null)
            {
                int status = (int)httpResp.StatusCode;
                return (status >= 300 && status < 400);
            }
            return false;
        }

        /// <summary>
        /// Add the given headers to the WebRequest
        /// </summary>
        /// <param name="req">Web request to add the headers to.</param>
        /// <param name="headers">A map of string to string representing the HTTP headers to pass (can be null)</param>
        private void addHeaders( WebRequest req, SortedList headers )
        {
            addHeaders( req, headers, "" );
        }

        /// <summary>
        /// Add the given metadata fields to the WebRequest.
        /// </summary>
        /// <param name="req">Web request to add the headers to.</param>
        /// <param name="metadata">A map of string to string representing the S3 metadata for this resource.</param>
        private void addMetadataHeaders( WebRequest req, SortedList metadata )
        {
            addHeaders( req, metadata, Utils.METADATA_PREFIX );
        }

        /// <summary>
        /// Add the given headers to the WebRequest with a prefix before the keys.
        /// </summary>
        /// <param name="req">WebRequest to add the headers to.</param>
        /// <param name="headers">Headers to add.</param>
        /// <param name="prefix">String to prepend to each before ebfore adding it to the WebRequest</param>
        private void addHeaders( WebRequest req, SortedList headers, string prefix )
        {
            if ( headers != null )
            {
                foreach ( string key in headers.Keys )
                {
                    if (prefix.Length == 0 && key.Equals("Content-Type"))
                    {
                        req.ContentType = headers[key] as string;
                    }
                    else
                    {
                        req.Headers.Add(prefix + key, headers[key] as string);
                    }
                }
            }
        }

        /// <summary>
        /// Add the appropriate Authorization header to the WebRequest
        /// </summary>
        /// <param name="request">Request to add the header to</param>
        /// <param name="resource">The resource name (bucketName + "/" + key)</param>
        private void addAuthHeader( WebRequest request, string bucket, string key, SortedList query )
        {
            WebHeaderCollection headers = (request as HttpWebRequest).Headers;
            if (headers[Utils.ALTERNATIVE_DATE_HEADER] == null)
            {
                headers.Add(Utils.ALTERNATIVE_DATE_HEADER, Utils.getHttpDate());
            }

            string canonicalString = Utils.makeCanonicalString( bucket, key, query, request );
            string encodedCanonical = Utils.encode( awsSecretAccessKey, canonicalString, false );
            headers.Add( "Authorization", "AWS " + awsAccessKeyId + ":" + encodedCanonical );
        }


        static readonly Regex BUCKET_NAME_REGEX_PATH 
            = new Regex("^[0-9A-Za-z\\.\\-_]*$", RegexOptions.CultureInvariant);
        static readonly Regex BUCKET_NAME_REGEX_SUBDOMAIN
            = new Regex("^[a-z0-9]([a-z0-9\\-]*[a-z0-9])?(\\.[a-z0-9]([a-z0-9\\-]*[a-z0-9])?)*$", RegexOptions.CultureInvariant);
        static readonly Regex BUCKET_NAME_REGEX_SUBDOMAIN_BAD
            = new Regex("^[0-9]+\\.[0-9]+\\.[0-9]+\\.[0-9]+$", RegexOptions.CultureInvariant);

        private bool validateBucketName(string bucketName)
        {
            bool valid;
            if (callingFormat == CallingFormat.PATH)
            {
                valid = null != bucketName &&
                    bucketName.Length >= 3 &&
                    bucketName.Length <= 255 &&
                    BUCKET_NAME_REGEX_PATH.IsMatch( bucketName );
            }
            else
            {
                // If there wasn't a location-constraint, then the current actual 
                // restriction is just that no 'part' of the name (i.e. sequence
                // of characters between any 2 '.'s has to be 63) but the recommendation
                // is to keep the entire bucket name under 63.
                valid = null != bucketName &&
                    bucketName.Length >= 3 &&
                    bucketName.Length <= 63 &&
                    !BUCKET_NAME_REGEX_SUBDOMAIN_BAD.IsMatch( bucketName ) &&
                    BUCKET_NAME_REGEX_SUBDOMAIN.IsMatch( bucketName );
            }
            return valid;
        }

        static private SortedList newSingleParam(string name)
        {
            SortedList query = new SortedList(1);
            query.Add(name, null);
            return query;
        }
    }
}
