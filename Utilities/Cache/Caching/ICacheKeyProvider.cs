using System;
using System.Collections.Generic;
using System.Text;

namespace Caching {
	public interface ICacheKeyProvider {
		string GetCacheKey();
	}
}
