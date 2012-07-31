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
using System.Text;
using AmazonS3;

namespace S3Sample
{
    class S3Test
    {
        private static readonly string awsAccessKeyId = "<INSERT YOUR AWS ACCESS KEY ID HERE>";
        private static readonly string awsSecretAccessKey = "<INSERT YOUR AWS SECRET ACCESS KEY HERE>";

        static readonly HttpStatusCode HTTP_OK = HttpStatusCode.OK;
        static readonly HttpStatusCode HTTP_NO_CONTENT = HttpStatusCode.NoContent;

        static readonly string bucketName = awsAccessKeyId.ToLower() + "-test-bucket";
        static CallingFormat[] callingFormats = { CallingFormat.PATH, CallingFormat.SUBDOMAIN };
        static int assertionCount = 0;

        static readonly int UnspecifiedMaxKeys = -1;

        static int Main(string[] args)
        {
            if (awsAccessKeyId.StartsWith("<INSERT")) {
                System.Console.WriteLine("Please examine S3Test.cs and update it with your credentials");
                return -1;
            }

            testUrlEscaping();

            // test all operation for both regular and vanity domains
            // regular: http://s3.amazonaws.com/key
            // subdomain: http://bucket.s3.amazonaws.com/key
            // testing pure vanity domains (http://<vanity domain>/key) is not covered here
            // but is possible with some assitional setup 

            foreach ( CallingFormat format in callingFormats )
            {
                AWSAuthConnection conn = new AWSAuthConnection( awsAccessKeyId, awsSecretAccessKey, false, format );

                using ( Response response = conn.createBucket( bucketName, Location.DEFAULT, null ) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't create bucket" );
                }

                using ( ListBucketResponse listBucketResponse = conn.listBucket( bucketName, null, null, 0, null ) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "Couldn't get list" );
                    assertEquals(
                        0,
                        listBucketResponse.Entries.Count,
                        "list wasn't empty" );
                    verifyBucketResponseParameters( listBucketResponse, bucketName, "", "", UnspecifiedMaxKeys, null, false, null );
                }

                // start delimiter tests

                string text = "this is a test";
                string key = "example.txt";
                string innerKey = "test/inner.txt";
                string badcharKey = "x+/last{key}.txt";
                string lastKey = "z-last-key.txt";

                using ( Response response = conn.put(bucketName, key, new S3Object(text, null), null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put simple object");
                }

                using ( Response response = conn.put(bucketName, innerKey, new S3Object(text, null), null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put simple object");
                }

                using ( Response response = conn.put(bucketName, lastKey, new S3Object(text, null), null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put simple object");
                }

                using ( Response response = conn.put(bucketName, badcharKey, new S3Object(text, null), null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put simple object");
                }

                // plain list
                using ( ListBucketResponse listBucketResponse = conn.listBucket(bucketName, null, null, 0, null) ) {
                    assertEquals(
                        listBucketResponse.Status,
                        HTTP_OK,
                        "couldn't get list");
                    assertEquals(4, listBucketResponse.Entries.Count, "Unexpected list size");
                    assertEquals(0, listBucketResponse.CommonPrefixEntries.Count, "Unexpected common prefix size");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "", "", UnspecifiedMaxKeys, null, false, null);
                }

                // root "directory"
                using ( ListBucketResponse listBucketResponse = conn.listBucket(bucketName, null, null, 0, "/", null) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't get list");
                    assertEquals(2, listBucketResponse.Entries.Count, "Unexpected list size");
                    assertEquals(2, listBucketResponse.CommonPrefixEntries.Count, "Unexpected common prefix size");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "", "", UnspecifiedMaxKeys, "/", false, null);
                }

                // root "directory" with a max-keys of "1"
                using ( ListBucketResponse listBucketResponse = conn.listBucket(bucketName, null, null, 1, "/", null) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't get list");
                    assertEquals(1, listBucketResponse.Entries.Count, "Unexpected list size");
                    assertEquals(0, listBucketResponse.CommonPrefixEntries.Count, "Unexpected common prefix size");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "", "", 1, "/", true, "example.txt");
                }

                // root "directory" with a max-keys of "2"
                string marker = null;
                using ( ListBucketResponse listBucketResponse = conn.listBucket(bucketName, null, null, 2, "/", null) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't get list");
                    assertEquals(1, listBucketResponse.Entries.Count, "Unexpected list size");
                    assertEquals(1, listBucketResponse.CommonPrefixEntries.Count, "Unexpected common prefix size");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "", "", 2, "/", true, "test/");
                    marker = listBucketResponse.NextMarker;
                }
                using ( ListBucketResponse listBucketResponse = conn.listBucket( bucketName, null, marker, 2, "/", null ) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't get list");
                    assertEquals(1, listBucketResponse.Entries.Count, "Unexpected list size");
                    assertEquals(1, listBucketResponse.CommonPrefixEntries.Count, "Unexpected common prefix size");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "", marker, 2, "/", false, null);
                }

                // test "directory" (really just prefix)
                using ( ListBucketResponse listBucketResponse = conn.listBucket(bucketName, "test/", null, 0, "/", null) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't get list");
                    assertEquals(1, listBucketResponse.Entries.Count, "Unexpected list size");
                    assertEquals(0, listBucketResponse.CommonPrefixEntries.Count, "Unexpected common prefix size");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "test/", "", UnspecifiedMaxKeys, "/", false, null);
                }

                // test "directory" 'x+' (really just prefix)
                using ( ListBucketResponse listBucketResponse = conn.listBucket(bucketName, "x+/", null, 0, "/", null) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't get list");
                    assertEquals(1, listBucketResponse.Entries.Count, "Unexpected list size");
                    assertEquals(0, listBucketResponse.CommonPrefixEntries.Count, "Unexpected common prefix size");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "x+/", "", UnspecifiedMaxKeys, "/", false, null);
                }

                // remove innerkey
                using ( Response response = conn.delete(bucketName, innerKey, null) ) {
                    assertEquals(
                        HTTP_NO_CONTENT,
                        response.Status,
                        "couldn't delete entry");
                }

                // remove badcharKey
                using ( Response response = conn.delete(bucketName, badcharKey, null) ) {
                    assertEquals(
                        HTTP_NO_CONTENT,
                        response.Status,
                        "couldn't delete entry");
                }

                // remove last key
                using ( Response response = conn.delete(bucketName, lastKey, null) ) {
                    assertEquals(
                        HTTP_NO_CONTENT,
                        response.Status,
                        "couldn't delete entry");
                }

                // End of delimiter tests

                SortedList metadata = new SortedList();
                metadata.Add("title", "title");
                using ( Response response = conn.put(bucketName, key, new S3Object(text, metadata), null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put complex object");
                }

                using ( GetResponse getResponse = conn.get(bucketName, key, null) ) {
                    assertEquals(
                        HTTP_OK,
                        getResponse.Status,
                        "couldn't get object");
                    assertEquals(text, getResponse.Object.Data, "didn't get the right data back");
                    assertEquals(1, getResponse.Object.Metadata.Count, "didn't get the right metadata back");
                    assertEquals(
                        "title",
                        getResponse.Object.Metadata["title"],
                        "didn't get the right metadata back");
                    assertEquals(
                        (long)text.Length,
                        getResponse.Connection.ContentLength,
                        "didn't get the right content-length");
                }

                // end delimiter tests

                using ( Response response = conn.put(bucketName, key, new S3Object(text, null), null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put simple object");
                }

                SortedList metadataWithSpaces = new SortedList();
                string titleWithSpaces = " \t  title with leading and trailing spaces    ";
                metadataWithSpaces.Add("title", titleWithSpaces);
                using ( Response response = conn.put(bucketName, key, new S3Object(text, metadataWithSpaces), null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put metadata with leading and trailing spaces");
                }

                using ( GetResponse getResponse = conn.get(bucketName, key, null) ) {
                    assertEquals(
                        HTTP_OK,
                        getResponse.Status,
                        "couldn't get object");
                    assertEquals(1, getResponse.Object.Metadata.Count, "didn't get the right metadata back");
                    assertEquals(
                        titleWithSpaces.Trim(),
                        getResponse.Object.Metadata["title"],
                        "didn't get the right metadata back");
                }

                string weirdKey = "&=/%# ++/++";
                using ( Response response = conn.put(bucketName, weirdKey, new S3Object(text, null), null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put weird key");
                }

                using ( GetResponse getResponse = conn.get(bucketName, weirdKey, null) ) {
                    assertEquals(
                        HTTP_OK,
                        getResponse.Status,
                        "couldn't get weird key");
                }

                string acl = null;
                using ( GetResponse getResponse = conn.getACL(bucketName, key, null) ) {
                    assertEquals(
                        HTTP_OK,
                        getResponse.Status,
                        "couldn't get acl");
                    acl = getResponse.Object.Data;
                }


                using ( Response response = conn.putACL(bucketName, key, acl, null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put acl");
                }

                string bucketACL = null;
                using ( GetResponse getResponse = conn.getBucketACL(bucketName, null) ) {
                    assertEquals(
                        HTTP_OK,
                        getResponse.Status,
                        "couldn't get bucket acl");
                    bucketACL = getResponse.Object.Data;
                }

                using ( Response response = conn.putBucketACL(bucketName, bucketACL, null) ) {
                    assertEquals(
                        HTTP_OK,
                        response.Status,
                        "couldn't put bucket acl");
                }

                // bucket logging tests
                string bucketLogging;
                using ( GetResponse getResponse = conn.getBucketLogging(bucketName, null) ) {
                    assertEquals(
                            HTTP_OK,
                            getResponse.Status,
                            "couldn't get bucket logging config");
                    bucketLogging = getResponse.Object.Data;
                }

                using ( Response response = conn.putBucketLogging(bucketName, bucketLogging, null) ) {
                    assertEquals(
                            HTTP_OK,
                            response.Status,
                            "couldn't put bucket logging config");
                }

                // end bucket logging tests

                using ( ListBucketResponse listBucketResponse = conn.listBucket(bucketName, null, null, 0, null) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't list bucket");
                    ArrayList entries = listBucketResponse.Entries;
                    assertEquals(2, entries.Count, "didn't get back the right number of entries");
                    // depends on weirdKey < $key
                    assertEquals(weirdKey, ((ListEntry)entries[0]).Key, "first key isn't right");
                    assertEquals(key, ((ListEntry)entries[1]).Key, "second key isn't right");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "", "", UnspecifiedMaxKeys, null, false, null);
                }

                using ( ListBucketResponse listBucketResponse = conn.listBucket(bucketName, null, null, 1, null) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't list bucket");
                    assertEquals(
                        1,
                        listBucketResponse.Entries.Count,
                        "didn't get back the right number of entries");
                    verifyBucketResponseParameters(listBucketResponse, bucketName, "", "", 1, null, true, null);
                }
                using ( ListBucketResponse listBucketResponse = conn.listBucket( bucketName, null, null, 0, null ) ) {
                    assertEquals(
                        HTTP_OK,
                        listBucketResponse.Status,
                        "couldn't list bucket");

                    // delete the bucket's content
                    foreach ( ListEntry entry in listBucketResponse.Entries ) {
                        using ( Response response = conn.delete( bucketName, entry.Key, null ) ) {
                            assertEquals(
                                HTTP_NO_CONTENT,
                                response.Status,
                                "couldn't delete entry" );
                        }
                    }
                }

                ArrayList buckets = null;
                using ( ListAllMyBucketsResponse listAllMyBucketsResponse = conn.listAllMyBuckets(null) ) {
                    assertEquals(
                        HTTP_OK,
                        listAllMyBucketsResponse.Status,
                        "couldn't list all my buckets");
                    buckets = listAllMyBucketsResponse.Buckets;
                }

                using ( Response response = conn.deleteBucket(bucketName, null) ) {
                    assertEquals(
                        HTTP_NO_CONTENT,
                        response.Status,
                        "couldn't delete bucket");
                }

                using ( ListAllMyBucketsResponse listAllMyBucketsResponse = conn.listAllMyBuckets(null) ) {
                    assertEquals(
                        HTTP_OK,
                        listAllMyBucketsResponse.Status,
                        "couldn't list all my buckets");
                    assertEquals(
                        buckets.Count - 1,
                        listAllMyBucketsResponse.Buckets.Count,
                        "bucket count is incorrect");
                }

                QueryStringAuthGenerator generator =
                    new QueryStringAuthGenerator(awsAccessKeyId, awsSecretAccessKey, false);

                checkURL(
                        generator.createBucket(bucketName, null, null),
                        "PUT",
                        HTTP_OK,
                        "couldn't create bucket");
                checkURL(
                        generator.put(bucketName, key, new S3Object("test data", null), null),
                        "PUT",
                        HTTP_OK,
                        "put object",
                        "test data");
                checkURL(
                        generator.get(bucketName, key, null),
                        "GET",
                        HTTP_OK,
                        "get object");
                checkURL(
                        generator.listBucket(bucketName, null, null, 0, null),
                        "GET",
                        HTTP_OK,
                        "list bucket");
                checkURL(
                        generator.listAllMyBuckets(null),
                        "GET",
                        HTTP_OK,
                        "list all my buckets");
                checkURL(
                        generator.getACL(bucketName, key, null),
                        "GET",
                        HTTP_OK,
                        "get acl");
                checkURL(
                        generator.putACL(bucketName, key, null),
                        "PUT",
                        HTTP_OK,
                        "put acl",
                        acl);
                checkURL(
                        generator.getBucketACL(bucketName, null),
                        "GET",
                        HTTP_OK,
                        "get bucket acl");
                checkURL(
                        generator.putBucketACL(bucketName, null),
                        "PUT",
                        HTTP_OK,
                        "put bucket acl",
                        bucketACL);
                checkURL(
                        generator.delete(bucketName, key, null),
                        "DELETE",
                        HTTP_NO_CONTENT,
                        "delete object");
                checkURL(
                        generator.deleteBucket(bucketName, null),
                        "DELETE",
                        HTTP_NO_CONTENT,
                        "delete bucket");
            }

            System.Console.WriteLine("OK (" + assertionCount + " tests passed)");

            return 0;
        }

        private static void verifyBucketResponseParameters(ListBucketResponse listBucketResponse,
            string bucketName, string prefix, string marker,
            int maxKeys, string delimiter, bool isTruncated,
            string nextMarker)
        {
            assertEquals(bucketName, listBucketResponse.Name, "Bucket name should match.");
            assertEquals(prefix, listBucketResponse.Prefix, "Bucket prefix should match.");
            assertEquals(marker, listBucketResponse.Marker, "Bucket marker should match.");
            assertEquals(delimiter, listBucketResponse.Delimiter, "Bucket delimiter should match.");
            if (UnspecifiedMaxKeys != maxKeys)
            {
                assertEquals(maxKeys, listBucketResponse.MaxKeys, "Bucket max-keys should match.");
            }
            assertEquals(isTruncated, listBucketResponse.IsTruncated, "Bucket should not be truncated.");
            assertEquals(nextMarker, listBucketResponse.NextMarker, "Bucket nextMarker should match.");
        }

        private static void assertEquals(int expected, int actual, string message) {
            assertionCount++;
            if (expected != actual) {
                throw new Exception(message + ": expected " + expected + " but got " + actual);
            }
        }

        private static void assertEquals(byte[] expected, byte[] actual, string message) {
            assertionCount++;
            if (expected.Length != actual.Length)
            {
                throw new Exception("Lengths do not match: " + expected.Length + " vs " + actual.Length);
            }
            for (int i = 0; i < expected.Length; ++i)
            {
                if (expected[i] != actual[i])
                {
                    throw new Exception("Expected <" + expected[i] + "> but got <" + actual[i] + "> at index " + i);
                }
            }
        }

        private static void assertEquals(object expected, object actual, string message) {
            assertionCount++;
            if (expected != actual && (actual == null || !actual.Equals(expected)))
            {
                throw new Exception(message + ": expected " + expected + " but got " + actual);
            }
        }

        private static void checkURL(string url, string method, HttpStatusCode code, string message)
        {
            checkURL(url, method, code, message, null);
        }

        private static void checkURL(string url, string method, HttpStatusCode code, string message, string data)
        {
            if (data == null) data = "";

            WebRequest connection = WebRequest.Create( url );
            connection.Method = method;
            if ("PUT".Equals(method)) {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(data);

                connection.ContentLength = bytes.Length;
                connection.GetRequestStream().Write(bytes, 0, bytes.Length);
            }

            try
            {
                assertEquals(code, (connection.GetResponse() as HttpWebResponse).StatusCode, message);
            }
            catch (WebException ex)
            {
                string msg = ex.Response != null ? Utils.slurpInputStreamAsString(ex.Response.GetResponseStream()) : ex.Message;
                throw new WebException(msg, ex, ex.Status, ex.Response);
            }
            connection.GetResponse().Close();
        }

        private static void testUrlEscaping()
        {
            for (int i = 1; i < 256; i++)
            {
                // These are special cases that the below test can't properly check for
                if (i == '\\' || i == '#' || i == '%' || i == '+')
                    continue;
                String path = "/ab" + new String((char)i, 2) + "cd";
                String query = "?zyx=123";
                String escaped = Utils.urlEncode(path) + query;
                Uri uri = new Uri("http://nowhere" + path + query);
                assertEquals(escaped, uri.PathAndQuery, 
                    "Utils.urlEncode() did not match Uri processing");
            }
        }
    }
}
