<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MusicTypeDropDownList.ascx.cs" Inherits="Spotted.Controls.MusicTypeDropDownList" %>
<select id="<%= SelectTagID %>" name="<%= Name %>"<%= OnChangeAttribute %>>
	<option value="1" <%= MusicDropDown(1) %>>All Music</option>
	<option value="42" <%= MusicDropDown(42) %>>Commercial</option>
	<option value="2" <%= MusicDropDown(2) %>>&nbsp;&nbsp;Commercial Pop</option>
	<option value="3" <%= MusicDropDown(3) %>>&nbsp;&nbsp;Commercial Dance</option>
	<option value="43" <%= MusicDropDown(43) %>>&nbsp;&nbsp;Club Classics</option>
	<option value="4" <%= MusicDropDown(4) %>>House</option>
	<option value="9" <%= MusicDropDown(9) %>>&nbsp;&nbsp;Funky House</option>
	<option value="56" <%= MusicDropDown(56) %>>&nbsp;&nbsp;Jackin House</option>
	<option value="55" <%= MusicDropDown(55) %>>&nbsp;&nbsp;Electro House</option>
	<option value="57" <%= MusicDropDown(57) %>>&nbsp;&nbsp;Dirty House</option>
	<option value="74" <%= MusicDropDown(74) %>>&nbsp;&nbsp;Fidgit House</option>
	<option value="58" <%= MusicDropDown(58) %>>&nbsp;&nbsp;Latin House</option>
	<option value="41" <%= MusicDropDown(41) %>>&nbsp;&nbsp;Soulful House</option>
	<option value="7" <%= MusicDropDown(7) %>>&nbsp;&nbsp;Deep House</option>
	<option value="5" <%= MusicDropDown(5) %>>&nbsp;&nbsp;US House</option>
	<option value="34" <%= MusicDropDown(34) %>>&nbsp;&nbsp;US Garage</option>
	<option value="6" <%= MusicDropDown(6) %>>&nbsp;&nbsp;Progressive House</option>
	<option value="8" <%= MusicDropDown(8) %>>&nbsp;&nbsp;Tech House</option>
	<option value="40" <%= MusicDropDown(40) %>>&nbsp;&nbsp;Tribal House</option>
	<option value="44" <%= MusicDropDown(44) %>>&nbsp;&nbsp;Old Skool House</option>
	<option value="54" <%= MusicDropDown(54) %>>&nbsp;&nbsp;Acid House</option>
	<option value="73" <%= MusicDropDown(73) %>>&nbsp;&nbsp;Bassline House</option>
	<option value="10" <%= MusicDropDown(10) %>>Hard Dance</option>
	<option value="11" <%= MusicDropDown(11) %>>&nbsp;&nbsp;Hard House</option>
	<option value="59" <%= MusicDropDown(59) %>>&nbsp;&nbsp;Hardstyle</option>
	<option value="60" <%= MusicDropDown(60) %>>&nbsp;&nbsp;Hard Trance</option>
	<option value="12" <%= MusicDropDown(12) %>>&nbsp;&nbsp;Trance</option>
	<option value="13" <%= MusicDropDown(13) %>>&nbsp;&nbsp;Psy-Trance</option>
	<option value="14" <%= MusicDropDown(14) %>>&nbsp;&nbsp;Hardcore</option>
	<option value="45" <%= MusicDropDown(45) %>>&nbsp;&nbsp;Old Skool Hardcore</option>
	<option value="15" <%= MusicDropDown(15) %>>Alternative Dance</option>
	<option value="16" <%= MusicDropDown(16) %>>&nbsp;&nbsp;Electro</option>
	<option value="17" <%= MusicDropDown(17) %>>&nbsp;&nbsp;Big Beat</option>
	<option value="18" <%= MusicDropDown(18) %>>&nbsp;&nbsp;Breaks</option>
	<option value="20" <%= MusicDropDown(20) %>>Techno</option>
	<option value="61" <%= MusicDropDown(61) %>>&nbsp;&nbsp;Minimal Techno</option>
	<option value="21" <%= MusicDropDown(21) %>>&nbsp;&nbsp;Detroit Techno</option>
	<option value="62" <%= MusicDropDown(62) %>>&nbsp;&nbsp;Funky Techno</option>
	<option value="22" <%= MusicDropDown(22) %>>&nbsp;&nbsp;Acid Techno</option>
	<option value="23" <%= MusicDropDown(23) %>>&nbsp;&nbsp;Electro Techno</option>
	<option value="24" <%= MusicDropDown(24) %>>Drum and Bass</option>
	<option value="70" <%= MusicDropDown(70) %>>&nbsp;&nbsp;Liquid Drum and Bass</option>
	<option value="25" <%= MusicDropDown(25) %>>&nbsp;&nbsp;Jazzy Drum and Bass</option>
	<option value="26" <%= MusicDropDown(26) %>>&nbsp;&nbsp;Jump Up Drum and Bass</option>
	<option value="27" <%= MusicDropDown(27) %>>&nbsp;&nbsp;Jungle</option>
	<option value="28" <%= MusicDropDown(28) %>>Urban</option>
	<option value="29" <%= MusicDropDown(29) %>>&nbsp;&nbsp;Hip Hop</option>
	<option value="30" <%= MusicDropDown(30) %>>&nbsp;&nbsp;R&amp;B</option>
	<option value="31" <%= MusicDropDown(31) %>>&nbsp;&nbsp;Dancehall / Bashment</option>
	<option value="32" <%= MusicDropDown(32) %>>&nbsp;&nbsp;Reggae</option>
	<option value="33" <%= MusicDropDown(33) %>>&nbsp;&nbsp;UK Garage</option>
	<option value="71" <%= MusicDropDown(71) %>>&nbsp;&nbsp;Dubstep</option>
	<option value="72" <%= MusicDropDown(72) %>>&nbsp;&nbsp;Reggaeton</option>
	<option value="65" <%= MusicDropDown(65) %>>Alternative Electronic</option>
	<option value="66" <%= MusicDropDown(66) %>>&nbsp;&nbsp;Industrial</option>
	<option value="67" <%= MusicDropDown(67) %>>&nbsp;&nbsp;Electronic Body Music</option>
	<option value="68" <%= MusicDropDown(68) %>>&nbsp;&nbsp;Futurepop</option>
	<option value="69" <%= MusicDropDown(69) %>>&nbsp;&nbsp;Powernoise</option>
	<option value="46" <%= MusicDropDown(46) %>>Retro</option>
	<option value="47" <%= MusicDropDown(47) %>>&nbsp;&nbsp;Funk</option>
	<option value="48" <%= MusicDropDown(48) %>>&nbsp;&nbsp;Disco</option>
	<option value="49" <%= MusicDropDown(49) %>>&nbsp;&nbsp;Jazz-Funk</option>
	<option value="50" <%= MusicDropDown(50) %>>&nbsp;&nbsp;Soul</option>
	<option value="51" <%= MusicDropDown(51) %>>&nbsp;&nbsp;Jazz</option>
	<option value="52" <%= MusicDropDown(52) %>>&nbsp;&nbsp;Rare Groove</option>
	<option value="35" <%= MusicDropDown(35) %>>&nbsp;&nbsp;Chillout / Leftfield</option>
	<option value="36" <%= MusicDropDown(36) %>>Rock</option>
	<option value="37" <%= MusicDropDown(37) %>>&nbsp;&nbsp;Indie</option>
	<option value="38" <%= MusicDropDown(38) %>>&nbsp;&nbsp;Rock</option>
	<option value="39" <%= MusicDropDown(39) %>>&nbsp;&nbsp;Metal</option>
	<option value="63" <%= MusicDropDown(63) %>>&nbsp;&nbsp;Punk</option>
	<option value="64" <%= MusicDropDown(64) %>>&nbsp;&nbsp;Acoustic</option>
</select>
