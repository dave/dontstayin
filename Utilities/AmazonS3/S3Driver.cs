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
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using AmazonS3;

namespace S3Sample
{
    class S3Driver
    {
        private static readonly string awsAccessKeyId = "<INSERT YOUR AWS ACCESS KEY ID HERE>";
        private static readonly string awsSecretAccessKey = "<INSERT YOUR AWS SECRET ACCESS KEY HERE>";

        // Convert the bucket name to lowercase for vanity domains.
        // the bucket must be lower case since DNS is case-insensitive.
        private static readonly string bucketName = awsAccessKeyId.ToLower() + "-test-bucket";
        private static readonly string keyName = "test-key";

        static int Main(string[] args)
        {
            try {
                if ( awsAccessKeyId.StartsWith( "<INSERT" ) )
                {
                    System.Console.WriteLine( "Please examine S3Driver.cs and update it with your credentials" );
                    return 1;
                }

                AWSAuthConnection conn = 
                        new AWSAuthConnection( awsAccessKeyId, awsSecretAccessKey );
                QueryStringAuthGenerator generator =
                        new QueryStringAuthGenerator( awsAccessKeyId, awsSecretAccessKey );

                // Check if the bucket exists.  The high availability engineering of 
                // Amazon S3 is focused on get, put, list, and delete operations. 
                // Because bucket operations work against a centralized, global
                // resource space, it is not appropriate to make bucket create or
                // delete calls on the high availability code path of your application.
                // It is better to create or delete buckets in a separate initialization
                // or setup routine that you run less often.
                if (conn.checkBucketExists(bucketName))
                {
                    System.Console.WriteLine("----- bucket already exists! -----");
                }
                else
                {
                    System.Console.WriteLine( "----- creating bucket -----" );
                    // to create an EU located bucket change the Location param like this:
                    //  using ( Response response = conn.createBucket( bucketName, Location.EU, null ) )
                    using (Response response = conn.createBucket(bucketName, Location.EU, null))
                    {
                        System.Console.WriteLine(response.getResponseMessage() );
                    }
                }

                System.Console.WriteLine("----- bucket location -----");
                using (LocationResponse locationResponse = conn.getBucketLocation(bucketName))
                {
                    if (locationResponse.Location == null)
                        System.Console.WriteLine("Location: <error>");
                    else if (locationResponse.Location.Length == 0)
                        System.Console.WriteLine("Location: <default>");
                    else
                        System.Console.WriteLine("Location: '{0}'", locationResponse.Location);
                }

                System.Console.WriteLine("----- listing bucket -----");
                using ( ListBucketResponse listBucketResponse = conn.listBucket( bucketName, null, null, 0, null ) ) {
                    dumpBucketListing( listBucketResponse );
                }

                System.Console.WriteLine( "----- putting object -----" );
                S3Object obj = new S3Object( "This is a test", null );
                SortedList headers = new SortedList();
                headers.Add( "Content-Type", "text/plain" );
                using ( Response response = conn.put( bucketName, keyName, obj, headers ) ) {
                    System.Console.WriteLine( response.getResponseMessage() );
                }

                System.Console.WriteLine( "----- listing bucket -----" );
                using ( ListBucketResponse listBucketResponse = conn.listBucket( bucketName, null, null, 0, null ) ) {
                    dumpBucketListing( listBucketResponse );
                }

                System.Console.WriteLine( "----- query string auth example -----" );
                generator.ExpiresIn = 60 * 1000;

                System.Console.WriteLine( "Try this url in your web browser (it will only work for 60 seconds)\n" );
                string url = generator.get( bucketName, keyName, null );
                System.Console.WriteLine( url );
                System.Console.Write( "\npress enter >" );
                System.Console.ReadLine();

                System.Console.WriteLine( "\nNow try just the url without the query string arguments.  It should fail.\n" );
                System.Console.WriteLine( generator.makeBaseURL(bucketName, keyName) );
                System.Console.Write( "\npress enter >" );
                System.Console.ReadLine();

                System.Console.WriteLine( "----- putting object with metadata and public read acl -----" );
                SortedList metadata = new SortedList();
                metadata.Add( "blah", "foo" );
                obj = new S3Object( "this is a publicly readable test", metadata );

                headers = new SortedList();
                headers.Add( "x-amz-acl", "public-read" );
                headers.Add( "Content-Type", "text/plain" );
                using ( Response response = conn.put( bucketName, keyName + "-public", obj, headers ) ) {
                    System.Console.WriteLine( response.getResponseMessage() );
                }

                System.Console.WriteLine( "----- anonymous read test -----" );
                System.Console.WriteLine( "\nYou should be able to try this in your browser\n" );
                string publicURL = generator.get(bucketName, keyName + "-public", null);
                System.Console.WriteLine(publicURL);
                System.Console.Write( "\npress enter >" );
                System.Console.ReadLine();

                System.Console.WriteLine("----- path style url example -----");
                System.Console.WriteLine("\nNon-location-constrained buckets can also be specified as part of the url path.  (This was the original url style supported by S3.)");
                System.Console.WriteLine("\nTry this url out in your browser (it will only be valid for 60 seconds)\n");
                generator.CallingFormat = CallingFormat.PATH;
                // could also have been done like this:
                //  generator = new QueryStringAuthGenerator(awsAccessKeyId, awsSecretAccessKey, true, Utils.DEFAULT_HOST, CallingFormat.getPathCallingFormat());
                generator.ExpiresIn = 60 * 1000;
                System.Console.WriteLine(generator.get(bucketName, keyName, null));
                System.Console.Write("\npress enter> ");
                System.Console.ReadLine();


                System.Console.WriteLine( "----- getting object's acl -----" );
                using ( GetResponse response = conn.getACL( bucketName, keyName, null ) ) {
                    System.Console.WriteLine( response.Object.Data );
                }

                System.Console.WriteLine( "----- deleting objects -----" );
                using ( Response response = conn.delete( bucketName, keyName, null ) ) {
                    System.Console.WriteLine( response.getResponseMessage() );
                }
                using ( Response response = conn.delete( bucketName, keyName + "-public", null ) ) {
                    System.Console.WriteLine( response.getResponseMessage() );
                }

                System.Console.WriteLine( "----- listing bucket -----" );
                using ( ListBucketResponse listBucketResponse = conn.listBucket( bucketName, null, null, 0, null ) ) {
                    dumpBucketListing( listBucketResponse );
                }

                System.Console.WriteLine( "----- listing all my buckets -----" );
                using ( ListAllMyBucketsResponse listBucketResponse = conn.listAllMyBuckets( null ) ) {
                    dumpAllMyBucketListing( listBucketResponse );
                }

                System.Console.WriteLine( "----- deleting bucket -----" );
                using ( Response response = conn.deleteBucket( bucketName, null ) ) {
                    System.Console.WriteLine( response.getResponseMessage() );
                }
                return 0;
            } catch ( WebException e ) {
                System.Console.WriteLine( e.Status );
                System.Console.WriteLine( e.Message );
                System.Console.WriteLine( e.StackTrace );
                System.Console.ReadLine();
                return 1;
            } catch ( Exception e ) {
                System.Console.WriteLine( e.Message );
                System.Console.WriteLine( e.StackTrace );
                System.Console.ReadLine();
                return 1;
            }
        }

        private static String extractErrorCode(String errormsg)
        {
            try
            {
                XmlTextReader r = new XmlTextReader(new StringReader(errormsg));
                // read to start element
                while (r.Read() && !r.IsStartElement())
                    ;
                // looking for <Error>....
                if (r.IsStartElement("Error") && 
                    r.Read())
                {
                    // looking for <Code>....</Code>
                    return r.ReadElementString("Code");
                }
            }
            catch (XmlException)
            {
            }
            return null;
        }

        private static void dumpBucketListing(ListBucketResponse list)
        {
            foreach (ListEntry entry in list.Entries)
            {
                Owner o = entry.Owner;
                if (o == null)
                {
                    o = new Owner("", "");
                }
                System.Console.WriteLine( entry.Key.PadRight( 20 ) + 
                                          entry.ETag.PadRight( 20 ) +
                                          entry.LastModified.ToString().PadRight( 20 ) +
                                          o.Id.PadRight( 10 ) +
                                          o.DisplayName.PadRight( 20 ) +
                                          entry.Size.ToString().PadRight( 11 ) +
                                          entry.StorageClass.PadRight( 10 ) );
            }
        }

        private static void dumpAllMyBucketListing(ListAllMyBucketsResponse list)
        {
            foreach (Bucket entry in list.Buckets)
            {
                System.Console.WriteLine( entry.Name.PadRight(20) +
                                          entry.CreationDate.ToString().PadRight(20) );
            }
        }
    }
}
