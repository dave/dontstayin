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
using System.Xml;

namespace AmazonS3
{
    public class LocationResponse : Response
    {
        private string location;

        /// <summary>
        /// Return the reported Location.  
        /// If null, then response was not a valid location response.
        /// If Location == "", then location is not specified.
        /// </summary>
        public string Location
        {
            get { return location; }
        }

        public LocationResponse(WebResponse response)
            : base(response)
        {
            try
            {
                XmlTextReader r = new XmlTextReader(response.GetResponseStream());
                while (r.Read() && !r.IsStartElement())
                    ;
                this.location = r.ReadElementString("LocationConstraint");
                if (this.location == null)
                    this.location = "";
            }
            catch (XmlException)
            {
            }
        }


    }
}
