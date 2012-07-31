<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Test.ascx.cs" Inherits="Spotted.Blank.Test" %>

<div id="fb-root"></div>
<script>
	window.fbAsyncInit = function () {
		FB.init({ appId: 'bfa8eee21e5571480f66888debf50534',
			status: true, 
			cookie: true,
			xfbml: true
		});
	};
	(function () {
		var e = document.createElement('script'); e.async = true;
		e.src = document.location.protocol +
      '//connect.facebook.net/en_US/all.js';
		document.getElementById('fb-root').appendChild(e);
	} ());
</script>

	<p>
		<div onclick="test();return false;" style="width:100px; padding:10px; background-color:#00ff00;">test</div>
	</p>
	<p>
		<span id="OutputLabel"></span>
	</p>
	<script>
		function test() {
			//alert("test");
			FB.getLoginStatus(function (response) {
				alert("test3");
				if (response.session) {
					alert("true");
				} else {
					alert("false");
				}
			});
			//alert("test 2");
		}
	</script>
