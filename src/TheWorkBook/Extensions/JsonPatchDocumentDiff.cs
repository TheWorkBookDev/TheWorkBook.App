using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Linq;

namespace TheWorkBook.Extensions;

//Based on this: https://blog.abodit.com/posts/2014-05-json-patch-c-implementation/
public static class JsonPatchDocumentDiff
{
    public static JsonPatchDocument CalculatePatch(object left, object right)
    {
        var document = new JsonPatchDocument();

        var operations = CalculatePatch(JToken.FromObject(left), JToken.FromObject(right));
        document.Operations.AddRange(operations);

        return document;
    }

    private static IEnumerable<Operation> CalculatePatch(JToken left, JToken right, string path = "")
    {
        if (left.Type != right.Type)
        {
            yield return Replace(path, "", right);
            yield break;
        }

        if (left.Type == JTokenType.Array)
        {
            if (JToken.DeepEquals(left, right))
                yield break;

            using (var leftIterator = left.Children().GetEnumerator())
            using (var rightIterator = right.Children().GetEnumerator())
            {
                var index = 0;
                var leftCount = left.Children().Count();
                var minCount = Math.Min(leftCount, right.Children().Count());

                while (index < minCount)
                {
                    leftIterator.MoveNext();
                    rightIterator.MoveNext();

                    foreach (var patch in CalculatePatch(leftIterator.Current, rightIterator.Current, Extend(path, (index++).ToString())))
                        yield return patch;
                }

                while (leftIterator.MoveNext())
                    yield return Remove(Extend(path, index.ToString()), "");

                index = leftCount;
                while (leftIterator.MoveNext())
                    yield return Remove(Extend(path, (--index).ToString()), "");
            }

            yield break;
        }

        if (left.Type == JTokenType.Object)
        {
            var leftProps = ((IDictionary<string, JToken>)left).OrderBy(p => p.Key);
            var rightProps = ((IDictionary<string, JToken>)right).OrderBy(p => p.Key);

            foreach (var removed in leftProps.Except(rightProps, KeyComparer.Instance))
                yield return Remove(path, removed.Key);

            foreach (var added in rightProps.Except(leftProps, KeyComparer.Instance))
                yield return Add(path, added.Key, added.Value);

            var matchedKeys = leftProps.Select(x => x.Key).Intersect(rightProps.Select(y => y.Key));
            var zipped = matchedKeys.Select(k => new { key = k, left = left[k], right = right[k] });

            foreach (var match in zipped)
                foreach (var patch in CalculatePatch(match.left, match.right, Extend(path, match.key)))
                    yield return patch;

            yield break;
        }

        if (left.ToString() == right.ToString())
            yield break;

        yield return Replace(path, "", right);
    }

    private static string Extend(string path, string extension)
        => NormalizePathSegment(path) + "/" + NormalizePathSegment(extension);

    private static string NormalizePathSegment(string segment)
    {
        bool IsNumeric() => int.TryParse(segment, out int _);

        string LowerCaseFirst()
        {
            char[] segmentArray = segment.ToCharArray();
            segmentArray[0] = char.ToLower(segmentArray[0]);
            return new string(segmentArray);
        }

        if (string.IsNullOrEmpty(segment) || IsNumeric())
            return segment;

        return LowerCaseFirst();
    }

    private static Operation CreateOperationFrom(string op, string path, string key, JToken value)
    {
        if (string.IsNullOrEmpty(key))
            return new Operation { op = op, path = NormalizePathSegment(path), value = value };

        return new Operation { op = op, path = Extend(path, key), value = value };
    }

    private static Operation Add(string path, string key, JToken value)
        => CreateOperationFrom("add", path, key, value);

    private static Operation Remove(string path, string key)
        => CreateOperationFrom("remove", path, key, null);

    private static Operation Replace(string path, string key, JToken value)
        => CreateOperationFrom("replace", path, key, value);

    private sealed class KeyComparer : IEqualityComparer<KeyValuePair<string, JToken>>
    {
        public static readonly KeyComparer Instance = new KeyComparer();

        public bool Equals(KeyValuePair<string, JToken> x, KeyValuePair<string, JToken> y)
            => x.Key.Equals(y.Key);

        public int GetHashCode(KeyValuePair<string, JToken> obj)
            => obj.Key.GetHashCode();
    }
}
