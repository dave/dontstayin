using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Bobs
{
	

	#region Q - Query condition - reprasents a single condition to go into a where clause - e.g. [myCol >= 44.6] or [usrName LIKE '%David%']
	/// <summary>
	/// Q - Query condition - reprasents a single condition to go into a where clause - e.g. [myCol >= 44.6] or [usrName LIKE '%David%']
	/// </summary>
	public class Q : Caching.ICacheKeyProvider
	{
		public Column Column { get; set; }
		public Column Column2 { get; set; }

		public bool DoesNotUseSqlParameters { get; set; }
		public QueryOperator OpEnum { get; set; }
		public object Data { get; set; }
		int RenderTrueFalse = 0;
		private string LiteralSql { get; set; }
		#region constructors
		/// <summary>
		/// Query condition
		/// </summary>
		public Q(object ColumnEnum, QueryOperator OpEnum, object Data)
		{
			SetColumnProperties(ColumnEnum, null);
			this.OpEnum = OpEnum;
			this.Data = Data;

		}
		/// <summary>
		/// Used for any custom Qs
		/// </summary>
		/// <param name="literalSql">Use in the same way as a string.format e.g. "{0} = {1}" will generate Sql "columnEnum0 = columnEnum1" </param>
		/// <param name="columnEnum0">Can be null</param>
		/// <param name="columnEnum1">Can be null</param>
		public Q(string literalSql, object columnEnum0, object columnEnum1)
		{
			SetColumnProperties(columnEnum0, columnEnum1);
			this.LiteralSql = literalSql;
		}

		private void SetColumnProperties(object columnEnum1, object columnEnum2)
		{
			if (columnEnum1 != null)
			{
				if (columnEnum1 is Column)
					this.Column = (Column)columnEnum1;
				else
					this.Column = new Column(columnEnum1);
			}
			if (columnEnum2 != null)
			{
				if (columnEnum2 is Column)
					this.Column2 = (Column)columnEnum2;
				else
					this.Column2 = new Column(columnEnum2);
			}
		}
		/// <summary>
		///	Use this construction wher you want something in the format "columnEnum1 IN ('1','2','3') 
		/// </summary>
		public Q(object columnEnum1, string[] inputList) 
		{
			string[] list = new string[inputList.Length];
			inputList.CopyTo(list, 0);
			Array.Sort(list);
			DoesNotUseSqlParameters = true;
			SetColumnProperties(columnEnum1, null);
			if (list.Length == 0)
			{
				LiteralSql = "1 = 0";
			}
			else
			{
				List<string> replacedList = new List<string>(list).ConvertAll(s => s.Replace("'", "''").Replace("{", "{{").Replace("}", "}}"));
				LiteralSql = "{0} IN ('" + String.Join("','", replacedList.ToArray()) + "')";
			}
		}

		/// <summary>
		///	Use this construction wher you want something in the format "columnEnum1 IN (1,2,3)"
		/// </summary>
		public Q(object columnEnum1, int[] inputList)
		{
			int[] list = new int[inputList.Length];
			inputList.CopyTo(list, 0);
			Array.Sort(list);
			DoesNotUseSqlParameters = true;
			SetColumnProperties(columnEnum1, null);
			if (list.Length == 0)
			{
				LiteralSql = "1 = 0";
			}
			else
			{
				string[] intsAsStrings = (new List<int>(list)).ConvertAll(i => i.ToString()).ToArray();
				LiteralSql = "{0} IN (" + String.Join(",", intsAsStrings) + ")";
			}
		}
		


		/// <summary>
		/// Use this constructor when the condition compairs two columns with an equals operator.
		/// </summary>
		public Q(object columnEnum1, object columnEnum2, bool columnComparison)
		{
			
			SetColumnProperties(columnEnum1, columnEnum2);
			this.OpEnum = QueryOperator.EqualTo;
			this.DoesNotUseSqlParameters = true;
		}
		/// <summary>
		/// Use this constructor when the condition compairs two columns.
		/// </summary>
		public Q(object columnEnum1, QueryOperator opEnum, object columnEnum2, bool columnComparison)
		{
			SetColumnProperties(columnEnum1, columnEnum2);

			this.OpEnum = opEnum;
			this.DoesNotUseSqlParameters = true;
		}
		/// <summary>
		/// Query condition with default QueryOperator of EqualTo
		/// </summary>
		/// <param name="ColumnEnum"></param>
		/// <param name="Data"></param>
		public Q(object columnEnum, object Data) : this(columnEnum, QueryOperator.EqualTo, Data) { }
		public Q(bool RenderTrueFalse)
		{
			if (RenderTrueFalse)
				this.RenderTrueFalse = 1;
			else
				this.RenderTrueFalse = 2;
		}
		public Q() { }
		
		
		#endregion

		#region ToString methods (2 overloads)
		public virtual void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			if (RenderTrueFalse > 0)
			{
				sb.Append(" ( ");
				sb.Append(RenderTrueFalse == 1 ? "1=1" : "1=0");
				sb.Append(" ) ");
				return;
			}
			if (LiteralSql != null)
			{
				sb.Append(" ( ");
				sb.Append(String.Format(LiteralSql, Column == null ? "" : Column.InternalSqlName, Column2 == null ? "" : Column2.InternalSqlName));
				sb.Append(" ) ");
				return;
			}
			string op = "";
			string postPart = "";
			#region get the operator sql text
			switch (OpEnum)
			{
				case QueryOperator.EqualTo:					op = "=";	break;
				case QueryOperator.NotEqualTo:				op = "<>";	break;
				case QueryOperator.GreaterThan:				op = ">";	break;
				case QueryOperator.GreaterThanOrEqualTo:	op = ">=";	break;
				case QueryOperator.LessThan:				op = "<";	break;
				case QueryOperator.LessThanOrEqualTo:		op = "<=";	break;
				case QueryOperator.TextContains:			Data = "%" + (string)Data + "%";	op = "LIKE";	break;
				case QueryOperator.TextEndsWith:			Data = "%" + (string)Data;			op = "LIKE";	break;
				case QueryOperator.TextStartsWith:			Data = (string)Data + "%";			op = "LIKE";	break;
				case QueryOperator.TextContainsAnyWord:		break;
				case QueryOperator.BitwiseEnumContains:
					string paramName = GetUniqueParamName(paramHash, Column.ColumnName);
					op = "& @" + paramName + " = ";
					SqlParameter p = new SqlParameter("@" + paramName, Data);
					paramHash.Add(paramName, p);
					break;
				case QueryOperator.BitwiseEnumDoesntContain:
					string paramName2 = GetUniqueParamName(paramHash, Column.ColumnName);
					op = "& @" + paramName2 + " <> ";
					SqlParameter p2 = new SqlParameter("@" + paramName2, Data);
					paramHash.Add(paramName2, p2);
					break;
				case QueryOperator.BitwiseAndEqualsZero:	op = "&";	postPart = " = 0";		break;
				default:		break;
			}
			#endregion

			if (DoesNotUseSqlParameters)
			{
				sb.Append(String.Format("({0} {1} {2} {3})", Column.InternalSqlName, op, Column2.InternalSqlName, postPart, " ) "));
			}
			else
			{
				if (OpEnum.Equals(QueryOperator.IsNull) || OpEnum.Equals(QueryOperator.IsNotNull))
				{
					sb.Append(" ");
					if (OpEnum.Equals(QueryOperator.IsNotNull))
						sb.Append("NOT ");
					sb.Append(Column.InternalSqlName).Append(" IS NULL ");
				}
				else if (OpEnum.Equals(QueryOperator.TextContainsAnyWord))
				{
					//SqlColumn col = Column.SqlColumnFromCache;
					List<string> stringArrayRaw = new List<string>(((string)Data).Split(' '));
					List<string> stringArray = MakeCombi(stringArrayRaw);
					string paramName = "";
					sb.Append(" ( 1=0 ");
					for (int i = 0; i < stringArray.Count; i++)
					{
						sb.Append(" OR ");
						sb.Append(Column.InternalSqlName);
						sb.Append(" LIKE @");
						sb.Append(GetUniqueParamName(paramHash, Column.ColumnName));
						sb.Append(" ");
						SqlParameter p = new SqlParameter("@" + paramName, "%" + stringArray[i] + "%");

						sb.Append(postPart);
						paramHash.Add(paramName, p);
					}
					sb.Append(" ) ");
				}
				else
				{
					string paramName = GetUniqueParamName(paramHash, Column.ColumnName);

					sb.Append(" ");
					sb.Append(Column.InternalSqlName);
					sb.Append(" ");
					sb.Append(op);
					if (Data == null)
					{
						sb.Append(" NULL ");
					}
					else
					{
						sb.Append(" @");
						sb.Append(paramName);
						sb.Append(" ");
						sb.Append(postPart);
						SqlParameter p = new SqlParameter("@" + paramName, Data);
						paramHash.Add(paramName, p);
					}

				}
			}
		}
		public virtual void BuildString(StringBuilder sb)
		{
			if (!DoesNotUseSqlParameters)
				throw new Exception("Must specify ref Dictionary<string, SqlParameter> paramHash for SQLParameters if condition is not a ColumnComparison");
			Dictionary<string, SqlParameter> h = new Dictionary<string, SqlParameter>();
			string s = "";
			this.BuildString(sb, ref h, ref s);
		}
		public override string ToString()
		{
			if (!DoesNotUseSqlParameters)
				throw new Exception("Must specify ref Dictionary<string, SqlParameter> paramHash for SQLParameters if condition is not a ColumnComparison");
			Dictionary<string, SqlParameter> h = new Dictionary<string, SqlParameter>();
			string s = "";
			return this.ToString(ref h, ref s);
		}
		public virtual string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			StringBuilder sb = new StringBuilder();
			this.BuildString(sb, ref paramHash, ref rankContribution);
			return sb.ToString();
		}
		public virtual string ToString(ref Dictionary<string, SqlParameter> paramHash)
		{
			StringBuilder sb = new StringBuilder();
			string rankContribution = "";
			this.BuildString(sb, ref paramHash, ref rankContribution);
			return sb.ToString();
		}
		#region copied from giles

		public static List<string> MakeCombi(List<string> stringArray)
		{
			List<string> common = new List<string>(CommonWords);
			List<string> stringArrayWithoutCommonWords = new List<string>();
			for (int i = 0; i < stringArray.Count; i++)
			{
				string myString = stringArray[i].ToString().ToLower().Trim();
				if (myString.Length > 1 & !common.Contains(myString))
				{
					stringArrayWithoutCommonWords.Add(stringArray[i].ToString().Trim());
				}
			}

			List<string> secondStringArray = new List<string>();
			for (int i = 0; i < stringArrayWithoutCommonWords.Count; i++)
			{
				for (int j = stringArrayWithoutCommonWords.Count; j > i; j--)
				{
					secondStringArray.Add("%" + AddStrings(stringArrayWithoutCommonWords, i, j) + "%");
				}
			}
			return secondStringArray;
		}

		internal static string AddStrings(List<string> stringArray, int start, int finish)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = start; i < finish; i++)
			{
				if (i != start)
					sb.Append("%");
				sb.Append(stringArray[i]);
			}
			return sb.ToString();
		}

		public static string[] CommonWords
		{
			get
			{
				string words = "a,about,after,ago,all,almost,along,also,an,and,answer,any,anybody,anywhere,are,aren't,around,as,ask,at,bad,be,been,before,being,best,better,between,big,but,by,can,can't,come,could,couldn't,day,did,didn't,do,does,don't,down,each,either,else,even,ever,every,everybody,everyone,far,for,found,from,get,go,going,gone,good,got,had,has,have,haven't,having,her,here,hers,him,his,home,how,if,in,into,is,isn't,it,its,know,large,like,little,many,me,most,must,my,near,never,new,news,no,none,not,nothing,of,off,often,old,on,once,only,or,other,our,ours,out,over,page,please,question,rather,recent,she,should,small,so,some,something,sometime,somewhere,than,that,the,their,theirs,them,then,there,these,they,this,those,though,through,thus,time,times,to,too,under,until,up,upon,use,users,version,very,via,was,web,were,what,when,where,which,who,whom,whose,why,wide,will,with,within,without,world,worse,worst,would,www,yes,yet,you,your,yours";
				return words.Split(',');
			}
		}

		#endregion

		public static string GetUniqueParamName(Dictionary<string, SqlParameter> paramHash, string paramName)
		{
			string initialParamName = paramName;
			int intToAdd = 0;
			if (paramHash.ContainsKey(paramName))
			{
				while (paramHash.ContainsKey(paramName))
				{
					paramName = initialParamName + intToAdd.ToString();
					intToAdd++;
				}
			}
			return paramName;
		}

		#endregion

		#region ICacheKeyProvider Members

		public virtual string GetCacheKey()
		{
			StringBuilder sb = new StringBuilder();
			if (DoesNotUseSqlParameters)
			{
				BuildString(sb);
			}
			else
			{
				Dictionary<string, SqlParameter> paramHash = new Dictionary<string, SqlParameter>();
				string rankContribution = "";
				BuildString(sb, ref paramHash, ref rankContribution);
				foreach (SqlParameter param in paramHash.Values)
				{
					sb.Append(param.ParameterName);
					sb.Append(param.SqlDbType.ToString());
					sb.Append(param.Value);
				}
			}
			return sb.ToString();
		}

		#endregion

		public virtual IEnumerable<Column> Columns()
		{
			if (Column != null) yield return Column;
			if (Column2 != null) yield return Column2;
		}
	}
	#endregion
	

}
