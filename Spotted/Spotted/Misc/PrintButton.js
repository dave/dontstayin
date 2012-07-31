

//
// PrintButton by PhotoBox Limited - Copyright 2001-2004 All rights reserved
//
// $Id: makeprintbuttonscripts.pl,v 1.27 2004/12/06 11:38:51 chris Exp $
//
// This script generated: 2005-03-23 00:00:46
//
// s: 9297735

var host='http://printbutton.photobox.co.uk/printbutton.html?v_id=221004&product_id=display_popup';

var xoptions='';
var q="'";
var b=new Object;
var cnt=0;
var cblist=new Object;
var newwin=1;

window.onload = traverse_images;

function traverse_images()
{
	imgs = document.getElementsByTagName("img");
	buy_photos=new Array();
	for (var i=0;i<imgs.length;i++) {
		img = imgs[i];
		if (img.getAttribute("buy") == "yes") {
			var imgsrc = img.getAttribute("src");
			var imgname = imgsrc.replace(/.+\/(.+\.jpe?g)$/i,'$1');
			var url = '&buy='+escape(imgname);
			b[cnt]=new Object;
			b[cnt].url=url;
			b[cnt].img=img;
			var buyLink = document.createElement('a');
			buyLink.setAttribute('href', 'javascript:buy_photo(' + cnt + ')');
			buyLink.innerHTML = "buy this photo";
			img.parentNode.insertBefore(buyLink, img.nextSibling);
			img.parentNode.insertBefore(document.createElement('br'), img.nextSibling);
			//img.parentNode.appendChild(document.createElement('br'));
			//img.parentNode.appendChild(buyLink);
			cnt++;
		}
	}
}

function version()
{
	var a='PrintButton version 1.2\n';
	a+='Subversion: $Id: makeprintbuttonscripts.pl,v 1.27 2004/12/06 11:38:51 chris Exp $\n';
	a+='Script generated: 2005-03-23 00:00:46';
	alert(a);
}

function pbset()
{
	for(var i=0;i<arguments.length;i++)
	{
		if(arguments[i].indexOf('=')>0)xoptions+='&'+arguments[i].replace(/^vendor_([^=]+)=(.*)$/,'vendor_$1_global=$2');
	}
}

function buy(img)
{
        var url= '&buy='+escape(img);
	var caption='';
	for(var i=1;i<arguments.length;i++)
	{
		//alert(arguments[i]);
		if(arguments[i].indexOf('=')<0){ caption=arguments[i];
		} else {
			values=arguments[i].match(/^([^=]+)=(.+)/);
			if (values) {
				value=values[2];
				url+='&'+arguments[i].replace(/^([^=]+)=(.+)/,'$1_'+img+'='+escape(value));
			}
		}
	}
        if(!caption)caption='Buy this photo';
        if(caption.substring(0,1)=='/' || caption.substring(0,4)=='http')caption='<img src="' + caption +'" border=0>';

	b[cnt]=new Object;
	b[cnt].url=url;
	b[cnt].img=img;

	if(caption=='checkbox')
	{
		var rnd=Math.round(Math.random()*100000);
		document.write('<input type="checkbox" name="xc'+ rnd + '" value="1" onClick="ck(' + q + cnt + q + ',this)">');
		cblist[cnt]=0;
//		alert('<input type="checkbox" name="xc_'+ rnd + '" value="0" onClick="ck(' + q + cnt + q + ')">');
	}
        else document.write('<a id="'+ cnt +'" href="javascript:void buy_photo(' + cnt + ')">' + caption + '</a>');
	cnt++;
}

function ck(which,cbref)
{
	cblist[which]=cbref.checked;
}

function buy_photo(which)
{
	var url=b[which].url + xoptions;
	var burl='';
	for(var i=0;i<document.images.length;i++)
	{
		if(document.images[i].src.indexOf(b[which].img)!=-1)
		burl='&burl_' + b[which].img + '=' + escape(document.images[i].src);
	}
	url=host + url + burl;
	window.open(url,'Printbutton','left=50,top=50,width=350,height=450,scrollbars,resizable');
}

function viewbasket(caption,plan)
{
        if(!caption)caption='View shopping basket';
        if(caption.substring(0,1)=='/' || caption.substring(0,4)=='http')caption='<img src="' + caption +'" border=0>';
        document.write('<a href="' + host + '&view_basket=1&plan='+plan+'" target="pbprintbutton">' + caption + '</a>');
}

function viewbasketwithitems(caption,plan)
{
        if(!caption)caption='View shopping basket';
        if(caption.substring(0,1)=='/' || caption.substring(0,4)=='http')caption='<img src="' + caption +'" border=0>';
        document.write('<a href="' + host + '&view_basket=1&plan='+plan+'" target="pbprintbutton">' + caption);
	document.write('<script language="javascript" src="http://testprintbutton.photobox.co.uk/printbutton/basketitems.js?text=1"></script></a>');
}

function basketitems(withtext)
{
	if(withtext)
	{
		document.write('<script language="javascript" src="http://testprintbutton.photobox.co.uk/printbutton/basketitems.js?fulltext=1"></script></a>');
	}
	else
	{
		document.write('<script language="javascript" src="http://testprintbutton.photobox.co.uk/printbutton/basketitems.js"></script></a>');
	}
}

function checkout(caption,plan)
{
        if(!caption)caption='Checkout';
        if(caption.substring(0,1)=='/' || caption.substring(0,4)=='http')caption='<img src="' + caption +'" border=0>';
        document.write('<a href="' + host + '&checkout=1&plan=' + plan + '" target="pbprintbutton">' + caption + '</a>');
}

function addtobasket(caption)
{
        if(!caption)caption='Add selected photos to your basket';
        if(caption.substring(0,1)=='/' || caption.substring(0,4)=='http')caption='<img src="' + caption +'" border=0>';
        document.write('<a href="javascript:void add_to_basket()">' + caption + '</a>');
}

function add_to_basket()
{
	var url='';
	var burl='';
	var found=false;
	for (var i in cblist)if(cblist[i]==1)
	{
		found=true;
		url+=b[i].url;
		for(var j=0;j<document.images.length;j++)
		{
			if(document.images[j].src.indexOf(b[i].img)!=-1)
			burl+='&burl_' + b[i].img + '=' + document.images[j].src;
		}
	}
	url=host + url + xoptions + burl;
	if(found)window.open(url,'Printbutton','left=50,top=50,width=350,height=450,scrollbars,resizable');
	else alert('You have not selected any photos');
}

function barepricelist(plan) { pricelist(plan,'1'); }

function pricelist(plan,notable)
{
	var showtable=0;
	if(!notable)showtable=1;
	document.write('<script language="JavaScript" src="http://printbutton.photobox.co.uk/pricelist.js?vendor=221004&plan='+plan+'&showtable='+showtable+'"></script>');
}

function giftstudio(img)
{
        var url= '&giftstudio=1&buy='+img;
	var caption='';
	for(var i=1;i<arguments.length;i++)
	{
		if(arguments[i].indexOf('=')<0)caption=arguments[i];
		else url+='&'+arguments[i].replace(/^([^=]+)=/,'$1_'+img+'=');
	}
        if(!caption)caption='Make a gift';
        if(caption.substring(0,1)=='/' || caption.substring(0,4)=='http')caption='<img src="' + caption +'" border=0>';

	b[cnt]=new Object;
	b[cnt].url=url;
	b[cnt].img=img;

	if(caption=='checkbox')
	{
		var rnd=Math.round(Math.random()*100000);
		document.write('<input type="checkbox" name="xc'+ rnd + '" value="1" onClick="ck(' + q + cnt + q + ',this)">');
		cblist[cnt]=0;
	}
        else document.write('<a href="javascript:void gift_studio(' + cnt + ')">' + caption + '</a>');
	cnt++;
}

function gift_studio(which)
{
	var url=b[which].url;
	var burl='';
	for(var i=0;i<document.images.length;i++)
	{
		if(document.images[i].src.indexOf(b[which].img)!=-1)
		burl='&burl_' + b[which].img + '=' + document.images[i].src;
	}
	url=host + url + burl;
	if(newwin)window.open(url,'Printbutton');
        else location.href=url;
}


