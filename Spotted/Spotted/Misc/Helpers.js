
var F =
{
	d: function()
	{
		var tmp = {};
		for (var i = 0; i < arguments.length; i = i + 2)
		{
			tmp[arguments[i]] = arguments[i + 1];
		}
		return tmp;
	}
}
