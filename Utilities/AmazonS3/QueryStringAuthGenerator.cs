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
using System.Text;
using System.Web;

namespace AmazonS3
{
    /// This class mimics the behavior of AWSAuthConnection, except instead of actually performing
    /// the operation, QueryStringAuthGenerator will return URLs with query string parameters that
    /// can be used to do the same thing.  These parameters include an expiration date, so that
    /// if you hand them off to someone else, they will only work for a limited amount of time.
    public class QueryStringAuthGenerator
    {
        private string awsAccessKeyId;
        private string awsSecretAccessKey;
        private bool isSecure;
        private string server;
        private int port;
        private CallingFormat callingFormat;

        private long expiresIn = NOT_SET;
        private long expires = NOT_SET;

        // by default, expire in 1 minute
        public static readonly long DEFAULT_EXPIRES_IN = 60 * 1000;

        // Sentinel to indicate when a date is not set
        private static readonly long NOT_SET = -1;

        public QueryStringAuthGenerator( string awsAccessKeyId, string awsSecretAccessKey )
            : this( awsAccessKeyId, awsSecretAccessKey, true, CallingFormat.SUBDOMAIN )
        {
        }

        public QueryStringAuthGenerator( string awsAccessKeyId, string awsSecretAccessKey,
                                         bool isSecure, CallingFormat format )
            : this(awsAccessKeyId, awsSecretAccessKey, isSecure, Utils.Host, format)
        {
        }

        public QueryStringAuthGenerator(string awsAccessKeyId, string awsSecretAccessKey,
                                         bool isSecure )
            : this( awsAccessKeyId, awsSecretAccessKey, isSecure, Utils.Host, CallingFormat.SUBDOMAIN )
        {
        }

        public QueryStringAuthGenerator(string awsAccessKeyId, string awsSecretAccessKey,
                                         bool isSecure, string server, CallingFormat format )
            : this(awsAccessKeyId, awsSecretAccessKey, isSecure, server,
                   isSecure ? Utils.SecurePort : Utils.InsecurePort, format)
        {
        }

        public QueryStringAuthGenerator(string awsAccessKeyId, string awsSecretAccessKey,
                                         bool isSecure, string server ) 
            : this( awsAccessKeyId, awsSecretAccessKey, isSecure, server,
                    isSecure ? Utils.SecurePort : Utils.InsecurePort, CallingFormat.SUBDOMAIN )
        {
        }

        public QueryStringAuthGenerator( string awsAccessKeyId, string awsSecretAccessKey,
                                         bool isSecure, string server, int port )
            : this( awsAccessKeyId, awsSecretAccessKey, isSecure, server,
                    port, CallingFormat.SUBDOMAIN)
        {
        }

        public QueryStringAuthGenerator( string awsAccessKeyId, string awsSecretAccessKey,
                                         bool isSecure, string server, int port, CallingFormat format )
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.isSecure = isSecure;
            this.server = server;
            this.port = port;
            this.expiresIn = DEFAULT_EXPIRES_IN;
            this.expires = NOT_SET;
            this.callingFormat = format;
        }

        /// <summary>
        /// Sets/Gets the current CallingFormat
        /// </summary>
        public CallingFormat CallingFormat
        {
            get {
                return this.callingFormat;
            }
            set {
                this.callingFormat = value;
            }
        }

        /// <summary>
        /// Sets/Gets the milliseconds since the Epoch that this
        /// expires at
        /// </summary>
        public long Expires {
            get {
                return expires;
            }
            set {
                expires = value;
                expiresIn = NOT_SET;
            }
        }

        public long ExpiresIn {
            get {
                return expiresIn;
            }
            set {
                expiresIn = value;
                expires = NOT_SET;
            }
        }

        public string createBucket( string bucket, SortedList headers, SortedList metadata )
        {
            return generateURL( "PUT", bucket, "", mergeMeta( headers, metadata ) );
        }

        public string listBucket(string bucket, string prefix, string marker,
                                  int maxKeys, SortedList headers)
        {
            return listBucket(bucket, prefix, marker, maxKeys, null, headers);
        }

        public string listBucket(string bucket, string prefix, string marker,
                                  int maxKeys, string delimiter, SortedList headers)
        {
            SortedList query = Utils.queryForListOptions(prefix, marker, maxKeys, delimiter);
            return generateURL("GET", bucket, "", query, headers);
        }

        public string deleteBucket( string bucket, SortedList headers )
        {
            return generateURL( "DELETE", bucket, "", headers );
        }

        public string put( string bucket, string key, S3Object obj, SortedList headers )
        {
            SortedList metadata = null;
            if ( obj != null )
            {
                metadata = obj.Metadata;
            }

            return generateURL("PUT", bucket, key, mergeMeta(headers, metadata));
        }

        public string get( string bucket, string key, SortedList headers )
        {
            return generateURL("GET", bucket, key, headers);
        }

        public string delete( string bucket, string key, SortedList headers )
        {
            return generateURL("DELETE", bucket, key, headers);
        }

        public string getBucketLogging(string bucket, SortedList headers)
        {
            SortedList query = new SortedList();
            query.Add("logging", "");
            return generateURL("GET", bucket, "", query, headers);
        }

        public string putBucketLogging(string bucket, SortedList headers)
        {
            SortedList query = new SortedList();
            query.Add("logging", "");
            return generateURL("PUT", bucket, "", query, headers);
        }

        public string getBucketACL(string bucket, SortedList headers)
        {
            SortedList query = new SortedList();
            query.Add("acl", "");
            return generateURL("GET", bucket, "", query, headers);
        }

        public string getACL(string bucket, string key, SortedList headers)
        {
            SortedList query = new SortedList();
            query.Add("acl", "");
            return generateURL("GET", bucket, key, query, headers);
        }

        public string putBucketACL(string bucket, SortedList headers)
        {
            SortedList query = new SortedList();
            query.Add("acl", "");
            return generateURL("PUT", bucket, "", query, headers);
        }

        public string putACL(string bucket, string key, SortedList headers)
        {
            SortedList query = new SortedList();
            query.Add("acl", "");
            return generateURL("PUT", bucket, key, query, headers);
        }

        public string listAllMyBuckets( SortedList headers )
        {
            return generateURL( "GET", "", "", headers );
        }

        public string makeBaseURL( string bucket, string key )
        {
            key = Utils.urlEncode(key);
            return makeBaseURL(new StringBuilder(), bucket, key).ToString();
        }

        private StringBuilder makeBaseURL(StringBuilder builder, string bucket, string key)
        {
            builder.Append(this.isSecure ? "https://" : "http://");
            builder.Append(Utils.buildUrlBase(server, port, bucket, callingFormat));
            if (bucket != null && bucket.Length != 0 &&
                key != null && key.Length != 0) {
                builder.Append(key);
            }
            return builder;
        }

        private string generateURL(string method, string bucket, string key, SortedList headers)
        {
            return generateURL(method, bucket, key, new SortedList(), headers);
        }

        private string generateURL(string method, string bucket, string key, SortedList queryParameters, SortedList headers)
        {
            long expires = 0L;
            if ( this.expiresIn != NOT_SET )
            {
                expires = Utils.currentTimeMillis() + this.expiresIn;
            }
            else if ( this.expires != NOT_SET )
            {
                expires = this.expires;
            }
            else
            {
                throw new Exception( "Illegal expire state!" );
            }

            // convert to seconds
            expires /= 1000;

            // escape key to prepare for being part of url
            key = Utils.urlEncode(key);

            string canonicalString = Utils.makeCanonicalString(method, bucket, key, queryParameters, headers, "" + expires);
            string encodedCanonical = Utils.encode( this.awsSecretAccessKey, canonicalString, true );

            queryParameters.Add("Signature", encodedCanonical);
            queryParameters.Add("Expires", "" + expires);
            queryParameters.Add("AWSAccessKeyId", awsAccessKeyId);

            StringBuilder builder = new StringBuilder();
            makeBaseURL(builder, bucket, key);
            builder.Append(Utils.convertQueryListToQueryString(queryParameters));
            return builder.ToString();
        }

        private SortedList mergeMeta( SortedList headers, SortedList metadata )
        {
            SortedList merged = new SortedList();
            if ( headers != null )
            {
                foreach ( string key in headers.Keys )
                {
                    merged.Add( key, headers[ key ] );
                }
            }

            if ( metadata != null )
            {
                foreach ( string key in metadata.Keys )
                {
                    string existing = merged[ key ] as string;
                    if ( existing != null )
                    {
                        existing += "," + metadata[ key ];
                    }
                    else
                    {
                        existing = metadata[ key ] as string;
                    }
                    merged.Add( key, existing );
                }
            }

            return merged;
        }
    }
}
