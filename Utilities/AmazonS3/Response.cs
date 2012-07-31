// This software code is made available "AS IS" without warranties of any        
// kind.  You may copy, display, modify and redistribute the software            
// code either by itself or as incorporated into your code; provided that        
// you do not remove any proprietary notices.  Your use of this software         
// code is at your own risk and you waive any claim against Amazon               
// Digital Services, Inc. or its affiliates with respect to your use of          
// this software code. (c) 2006-2007 Amazon Digital Services, Inc. or its             
// affiliates.          



using System;
using System.Net;
using System.Text;
using System.Collections;

namespace AmazonS3
{
    public class Response : IDisposable
    {
        protected WebResponse response;

        public WebResponse Connection
        {
            get
            {
                return response;
            }
        }

        public HttpStatusCode Status
        {
            get
            {
                HttpWebResponse wr = response as HttpWebResponse;
                return wr.StatusCode;
            }
        }

        public string XAmzId
        {
            get
            {
                return response.Headers.Get( "x-amz-id-2" );
            }
        }

        public string XAmzRequestId
        {
            get
            {
                return response.Headers.Get("x-amz-request-id");
            }
        }

        public Response(WebResponse response)
        {
            this.response = response;
        }

        public String getResponseMessage()
        {
            string data = Utils.slurpInputStreamAsString( response.GetResponseStream() );
            response.GetResponseStream().Close();
            return data;
        }

        public byte[] getResponseBytes()
        {
            byte[] data = Utils.slurpInputStream(response.GetResponseStream());
            response.GetResponseStream().Close();
            return data;
        }

        ~Response()
        {
            Close();
        }

        public void Dispose() {
            Close();
        }

        public void Close() {
            if ( response != null ) {
                response.Close();
                response = null;
            }
        }
    }
}
