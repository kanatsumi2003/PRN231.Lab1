using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Petalaka.Account.Core.Utils
{
    public class LowerCaseJsonNamingPolicy : JsonNamingPolicy
    {
            public override string ConvertName(string name)
            {
                if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
                    return name;

                return char.ToLower(name[0]) + name.Substring(1);
            }
    }
}
