using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver2019_08
{
  public static class Helpers
  {
    public static T MaxBy<T, R>(this IEnumerable<T> en, Func<T, R> evaluate) where R : IComparable<R>
    {
      return en.Select(t => new Tuple<T, R>(t, evaluate(t)))
          .Aggregate((max, next) => next.Item2.CompareTo(max.Item2) > 0 ? next : max).Item1;
    }

    public static T MinBy<T, R>(this IEnumerable<T> en, Func<T, R> evaluate) where R : IComparable<R>
    {
      return en.Select(t => new Tuple<T, R>(t, evaluate(t)))
          .Aggregate((max, next) => next.Item2.CompareTo(max.Item2) < 0 ? next : max).Item1;
    }
  }
}
