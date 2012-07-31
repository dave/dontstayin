  
   function NoConfirmClose()
   {
      win = top;
      win.opener = top;
      win.close ();
   }

