<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.MixmagVote.Home" %>
<h1>
	Mixmag Vote
</h1>
<p>
	<a href="http://www.mixmagfashion.com/">Mixmag Fashion</a>
</p>

<!--
<h1>BE CAREFULL!!!</h1>
<p>
	This test site will post real message to your facebook wall. Probably best to delete them straight away.
</p>

<h1>Example competition entry links</h1>
<p>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/alfredo-2.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/alfredo-2.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/bhunita-palmer-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/bhunita-palmer-1.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/carlos-3.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/carlos-3.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/chris.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/chris.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/claire-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/claire-1.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/frankie.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/frankie.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/ivan.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/ivan.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/michael-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/michael-1.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/nicolas-2.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/nicolas-2.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/rachel-2.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/rachel-2.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/rachel-waggett-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/rachel-waggett-1.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/robert-2.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/robert-2.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/sarah-3.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/sarah-3.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/sidney-3.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/sidney-3.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/simon-burgess.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/simon-burgess.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/trace-3.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/trace-3.jpg" width="100" border="0" /></a>
	<a href="/entry?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/vicky-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/vicky-1.jpg" width="100" border="0" /></a>
</p>

<h1>Example voting links</h1>
<p>
	Example vote link (from micro-site):<br />
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/alfredo-2.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/alfredo-2.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/bhunita-palmer-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/bhunita-palmer-1.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/carlos-3.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/carlos-3.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/chris.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/chris.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/claire-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/claire-1.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/frankie.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/frankie.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/ivan.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/ivan.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/michael-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/michael-1.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/nicolas-2.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/nicolas-2.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/rachel-2.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/rachel-2.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/rachel-waggett-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/rachel-waggett-1.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/robert-2.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/robert-2.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/sarah-3.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/sarah-3.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/sidney-3.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/sidney-3.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/simon-burgess.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/simon-burgess.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/trace-3.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/trace-3.jpg" width="100" border="0" /></a>
	<a href="/vote?k=2&url=http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/vicky-1.jpg"><img src="http://www.mixmagfashion.com/armaniexchange/wp-content/gallery/test/vicky-1.jpg" width="100" border="0" /></a>
</p>
-->

<!-- Welcome to DontStayIn (this string is needed to pass the build tests) -->
