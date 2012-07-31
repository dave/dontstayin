using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.Json
{
  public interface IJsonLineInfo
  {
    bool HasLineInfo();

    int LineNumber { get; }
    int LinePosition { get; }
  }
}
